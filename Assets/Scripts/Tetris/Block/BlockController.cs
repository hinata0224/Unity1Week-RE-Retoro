using UnityEngine;
using UniRx;
using Other_Script;
using FiledData;
using System;
using System.Collections.Generic;
using InputProvider;

namespace Tetris_Block
{
    public class BlockController : MonoBehaviour
    {
        private float downInterval;

        private float holedInterval;

        private bool endBlock = false;

        private Transform grid;

        [SerializeField]
        private Vector3 rotatePos;

        private ReactiveProperty<float> x_value = new(0);
        private ReactiveProperty<float> y_value = new(0);

        private Subject<Unit> nextCreate = new();
        private Subject<Unit> gameOverFlag = new();

        private CompositeDisposable disposables = new();

        private TimerModel timer = new();

        private FieldData model = new();

        private IInputProvider _inputProvider;

        /// <summary>
        /// 初期化
        /// </summary>
        /// <param name="down"></param>
        /// <param name="holed"></param>
        /// <param name="_grid"></param>
        public void Init(float down, float holed, Transform _grid)
        {
            _inputProvider = UnityInputProvider.Instance;

            downInterval = down;
            holedInterval = holed;
            grid = _grid;

            _inputProvider.OnMoveObservable
                .Where(x => x != 0)
                .Subscribe(x => BlockMove(x))
                .AddTo(gameObject);

            _inputProvider.OnRotateObservable
                .Subscribe(x => RotateBlock())
                .AddTo(gameObject);

            _inputProvider.OnDownObservable
                .Subscribe(x => DownBlock())
                .AddTo(gameObject);

            timer.GetEndTimer()
                .Subscribe(_ => DownBlock())
                .AddTo(gameObject);

            timer.SetTimer(downInterval);

            // 生成時に動けなければgameover
            if (!IsMoveMent())
            {
                gameOverFlag.OnNext(Unit.Default);
            }

        }

        /// <summary>
        /// ブロックの移動
        /// </summary>
        /// <param name="x"></param>
        private void BlockMove(float x)
        {
            timer.RestertTimer();
            if (Mathf.Sign(x) == 1)
            {
                transform.position += new Vector3(1, 0, 0);
                if (!IsMoveMent())
                {
                    transform.position += new Vector3(-1, 0, 0);
                }
            }
            else
            {
                transform.position += new Vector3(-1, 0, 0);
                if (!IsMoveMent())
                {
                    transform.position += new Vector3(1, 0, 0);
                }
            }
            x_value.Value = 0;
        }

        /// <summary>
        /// ブロックの回転
        /// </summary>
        /// <param name="y"></param>
        private void RotateBlock()
        {
            timer.RestertTimer();
            transform.RotateAround(transform.TransformPoint(rotatePos), new Vector3(0, 0, 90), 90);
            if (!IsMoveMent())
            {
                transform.RotateAround(transform.TransformPoint(rotatePos), new Vector3(0, 0, 90), -90);
            }
        }

        /// <summary>
        /// ブロックを落とす
        /// </summary>
        private void DownBlock()
        {
            transform.position += new Vector3(0, -1, 0);
            if (!IsMoveMent())
            {
                endBlock = true;
                transform.position += new Vector3(0, 1, 0);
                SetPosition();
            }
            else
            {
                endBlock = false;
            }
            timer.RestertTimer();
        }

        /// <summary>
        /// 左右の上限判定
        /// </summary>
        /// <returns></returns>
        private bool IsMoveMent()
        {
            foreach (Transform children in transform)
            {
                int roundX = Mathf.RoundToInt(children.transform.position.x);
                int roundY = Mathf.RoundToInt(children.transform.position.y);

                if (roundX < 0 || roundX >= model.GetWidht() || roundY < 0)
                {
                    return false;
                }

                if (!model.CheckGrid(roundX, roundY))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// 入力値の更新
        /// </summary>
        /// <param name="vec"></param>
        public void InputValue(Vector2 vec)
        {
            x_value.Value = vec.x;
            y_value.Value = vec.y;
        }

        /// <summary>
        /// ポジションを更新
        /// </summary>
        private void SetPosition()
        {
            timer.EndTimer();
            int count = transform.childCount;
            for (int i = count - 1; i >= 0; i--)
            {
                model.SetGrid(transform.GetChild(i));
                transform.GetChild(i).parent = grid;
            }

            //��񂻂���Ă��邩�̊m�F�Ƃ�����Ă��郉�C���̍�����ۊ�
            List<int> lineCount = new(5);
            for (int i = model.GetHeight() - 1; i >= 0; i--)        //�ォ��m�F����
            {
                if (model.CheckLine(i))
                {
                    lineCount.Add(i);
                }
            }
            //�u���b�N�̍폜�ƍ폜���������i������
            if (lineCount.Count > 0)
            {
                List<GameObject> tempObj = model.DeLeteLine(lineCount);
                foreach (GameObject obj in tempObj)
                {
                    Destroy(obj);
                }
                model.RowDown(lineCount);
            }

            nextCreate.OnNext(Unit.Default);

        }

        public IObservable<Unit> GetNextCreate()
        {
            return nextCreate.AddTo(disposables);
        }

        public IObservable<Unit> GetGameOverFlag()
        {
            return gameOverFlag.AddTo(disposables);
        }

        private void OnDestroy()
        {
            disposables.Dispose();
        }
    }
}