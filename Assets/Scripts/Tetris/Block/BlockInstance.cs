using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using Other_System;
//using UnityEngine.AddressableAssets;
//using UnityEngine.ResourceManagement.AsyncOperations;
using Tetris_Input;
using Tetris_UI;

namespace Tetris_Block
{
    public class BlockInstance : MonoBehaviour
    {
        [SerializeField, Header("生成インターバル")]
        private float interval = 1f;

        [SerializeField, Header("CSVデータのAddress")]
        private string csvAddress;

        [SerializeField, Header("Minoの親オブジェクト")]
        private Transform grid;

        [SerializeField, Header("生成場所")]
        private Transform createPos;

        [SerializeField, Header("MinoのPrefab")]
        private List<GameObject> minos;

        private List<GameObject> createNums = new(4);
        [SerializeField, Header("次のブロックの表示場所")]
        private List<Transform> nextMinoPos = new(4);

        private bool initflga = false;

        private Subject<int> instanceFlag = new();

        private Subject<Unit> gameOver = new();

        private TimerModel timer = new();
        private BlockData data;

        void Start()
        {

            timer.GetEndTimer()
                .Subscribe(x => MinoInstance())
                .AddTo(this);
            Init();
        }

        private void MinoInstance()
        {
            DeleteMino();
        }

        private void AddCreate(int count)
        {
            //float max = (float)importer.GetString(1).Count;
            //string address = importer.GetString(1)[(int)UnityEngine.Random.Range(1, max)];

            GameObject mino = minos[UnityEngine.Random.Range(0, minos.Count)];
            GameObject obj = Instantiate(mino, createPos);

            createNums.Add(obj);
            if (!initflga)
            {
                instanceFlag.OnNext(count);
            }
            else
            {
                SetNextMino();
            }

            //var handle = Addressables.LoadAssetAsync<GameObject>(address);
            //handle.Completed += mino =>
            //{
            //    if (mino.Status == AsyncOperationStatus.Succeeded)
            //    {
            //        GameObject obj = Instantiate(mino.Result, createPos);
            //        createNums.Add(obj);
            //        if (!initflga)
            //        {
            //            instanceFlag.OnNext(count);
            //        }
            //        else
            //        {
            //            SetNextMino();
            //        }
            //    }
            //};
        }

        private void SetNextMino()
        {
            for (int i = 0; i < createNums.Count; i++)
            {
                if (i == 0) {
                    ScoreModel model = new();
                    int score = model.GetScorePoint();
                    float blockInterval = 1f;

                    switch (score)
                    {
                        case 500:
                            blockInterval = 0.8f;
                            break;
                        case 1000:
                            blockInterval = 0.5f;
                            break;
                        case 1500:
                            blockInterval = 0.3f;
                            break;
                    }

                    data = createNums[i].GetComponent<BlockData>();
                    TetrisInputController.SetBlockData(data);
                    data.Init(grid, blockInterval);

                    data.GetNextCreate()
                    .Subscribe(x =>
                    {
                        timer.StartTimer(interval);
                        data.DestroyObj();
                    }).AddTo(createNums[i]);

                    data.GetGameOver()
                    .Subscribe(x =>
                    {
                        gameOver.OnNext(Unit.Default);
                    }).AddTo(createNums[i]);
                }
                createNums[i].transform.position = nextMinoPos[i].position;
            }
        }


        private void DeleteMino()
        {
            createNums.RemoveAt(0);
            AddCreate(5);
        }

        private void Init()
        {
            int nowcount = 0;
            instanceFlag.TakeWhile(x => x < nextMinoPos.Count)
                .Subscribe(x =>
                {
                    nowcount++;
                    AddCreate(nowcount);
                }, 
                _ =>
                {
                    //Debug.Log("zikoku");
                    //initflga = true;
                    //SetNextMino();
                },
                () =>
                {
                    initflga = true;
                    SetNextMino();
                }).AddTo(this);
            instanceFlag.OnNext(nowcount);
        }

        public void StartInstance()
        {
            timer.StartTimer(interval);
        }

        public void EndInstance()
        {
            timer.EndTimer();
        }

        public IObservable<Unit> GetGameOver()
        {
            return gameOver;
        }
    }
}