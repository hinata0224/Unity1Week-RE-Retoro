using System.Collections.Generic;
using UnityEngine;
using System;
using UniRx;
using Other_Script;

namespace Tetris_Block
{
    public class BlockManager : MonoBehaviour
    {
        private float _downInterval = 1f;
        private float _nextInterval = 1f;
        private float _holedInterval = 0.5f;
        [SerializeField]
        private List<Transform> _createPoss;
        private Transform _grid;
        // 本来はAddressableでやりたいがwebGLBuildのため参照
        [SerializeField]
        private List<GameObject> _blocks;
        private List<GameObject> _nextBlock = new(4);
        private bool _instanceCheck = true;
        private Subject<Unit> _isGameOver = new();
        private BlockController _blockdata;
        public IObservable<Unit> IsGameOver => _isGameOver;

        private CompositeDisposable _disposable = new CompositeDisposable();

        void Start()
        {
            Init();
        }

        //Blockを生成
        private void BlockInstance()
        {
            if (_instanceCheck)
            {
                GameObject block = _blocks[UnityEngine.Random.Range(0, _blocks.Count)];
                GameObject obj = Instantiate(block);

                _nextBlock.Add(obj);
            }
        }

        //Blockの位置を変更
        private void SetBlockPosition()
        {
            _disposable.Clear();
            for (int i = 0; i < _nextBlock.Count; i++)
            {
                if (i == 0)
                {
                    _blockdata = _nextBlock[i].GetComponent<BlockController>();

                    _blockdata.GetGameOverFlag()
                        .Subscribe(_ =>
                        {
                            _instanceCheck = false;
                            _isGameOver.OnNext(Unit.Default);
                        })
                        .AddTo(_disposable);
                    _blockdata.GetNextCreate()
                        .First()
                        .Subscribe(_ =>
                        {
                            DeleteBlock();
                            TimerModel timer = new();
                            timer.GetEndTimer()
                                .Where(_ => _instanceCheck)
                                .First()
                                .Subscribe(_ =>
                                {
                                    BlockInstance();
                                    SetBlockPosition();
                                    timer.EndTimer();
                                });
                            timer.SetTimer(_nextInterval);
                        });
                }
                _nextBlock[i].transform.position = _createPoss[i].position;
            }
            _blockdata.Init(_downInterval, _holedInterval, _grid);
        }

        private void DeleteBlock()
        {
            Destroy(_blockdata.gameObject);
            _nextBlock.RemoveAt(0);
        }

        private void Init()
        {
            for (int i = 0; i < _createPoss.Count; i++)
            {
                BlockInstance();
            }

            SetBlockPosition();
        }
    }
}
