using System.Collections.Generic;
using UnityEngine;
using Other_System;
using UniRx;
using System;
using Tetris_UI;

namespace Tetris_Block
{
    public class BlockData : MonoBehaviour
    {
        private float locateTime = 1;

        private float x_value;
        private float y_value;

        private Transform grid;

        [SerializeField, Header("子オブジェクトのブロック")]
        private List<GameObject> blocks;

        private bool move = true;
        private bool endblock = false;

        private Subject<bool> moveInput = new();
        private Subject<Unit> nextCreate = new();
        private Subject<Unit> gameOver = new();

        private BlockModel model = new();
        private TimerModel timer = new();


        private void MoveBlock()
        {
            if (x_value != 0)
            {
                move = false;
                timer.StopTimer();
                if (Mathf.Sign(x_value) == 1)
                {
                    transform.position += new Vector3(1, 0, 0);
                    if (!ValidMovement())
                    {
                        transform.position -= new Vector3(1, 0, 0);
                    }
                }
                else
                {
                    transform.position -= new Vector3(1, 0, 0);
                    if (!ValidMovement())
                    {
                        transform.position += new Vector3(1, 0, 0);
                    }
                }
                moveInput.OnNext(false);
            }
        }

        private void RotateBlock()
        {
            if (y_value != 0)
            {
                move = false;
                timer.StopTimer();
                if (Mathf.Sign(y_value) == 1)
                {
                    transform.RotateAround(this.transform.position, new Vector3(0, 0, 90), 90);
                    if (!ValidMovement())
                    {
                        transform.RotateAround(this.transform.position, new Vector3(0, 0, 90), -90);
                    }
                    moveInput.OnNext(false);
                }
                else
                {
                    if (!endblock)
                    {
                        transform.position -= new Vector3(0, 1, 0);
                        if (!ValidMovement())
                        {
                            transform.position += new Vector3(0, 1, 0);
                            endblock = true;
                        }
                        else
                        {
                            endblock = false;
                        }
                        moveInput.OnNext(false);
                    }
                }
            }
        }

        private void DownBlock()
        {
            timer.StopTimer();
            transform.position -= new Vector3(0, 1, 0);
            if (!ValidMovement())
            {
                transform.position += new Vector3(0, 1, 0);
                endblock = true;
            }
            else
            {
                endblock = false;
            }
            timer.StartTimer(locateTime);
        }


        private bool ValidMovement()
        {
            foreach (Transform children in transform)
            {
                int roundX = Mathf.RoundToInt(children.transform.position.x);
                int roundY = Mathf.RoundToInt(children.transform.position.y);

                if (roundX < 0 || roundX >= model.GetWidth() || roundY < 0)
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

        public void Init(Transform gridObj, float interval)
        {
            grid = gridObj;
            locateTime= interval;
            timer.StartTimer(locateTime);
            timer.GetEndTimer()
                .Subscribe(x => DownBlock())
                .AddTo(this);

            timer.GetEndTimer()
                .Where(x => endblock)
                .Subscribe(x => SetPosition())
                .AddTo(this);

            moveInput.Where(x => x)
                .Subscribe(x =>
                {
                    MoveBlock();
                }).AddTo(this);

            moveInput.Where(x => x)
                .Subscribe(x =>
                {
                    RotateBlock();
                }).AddTo(this);

            moveInput.Where(x => !x)
                .Subscribe(x =>
                {
                    timer.StartTimer(locateTime);
                    move = true;
                }).AddTo(this);
            if (!ValidMovement())
            {
                gameOver.OnNext(Unit.Default);
            }
        }

        public void InputValue(Vector2 vec)
        {
            x_value = vec.x;
            y_value = vec.y;
            moveInput.OnNext(move);
        }

        public void SetPosition()
        {
            timer.EndTimer();
            List<int> scoreCount = new();
            for (int i = 0; i < blocks.Count; i++)
            {
                model.SetGrid(blocks[i].transform);
                blocks[i].transform.parent = grid;
            }

            for (int i = model.GetHeight() - 1; i >= 0; i--)
            {
                if (model.CheckLine(i))
                {
                    scoreCount.Add(i);
                }
            }

            if(scoreCount.Count > 0)
            {
                List<GameObject> tempObj = model.DeketeLine(scoreCount);
                
                foreach(GameObject temp in tempObj)
                {
                    Destroy(temp);
                }

                model.RowDown(scoreCount);

                for(int i = 0; i < scoreCount.Count; i++)
                {
                    ScoreModel.AddScore();
                }
            }

            nextCreate.OnNext(Unit.Default);
        }

        public void DestroyObj()
        {
            nextCreate.Dispose();
            Destroy(gameObject);
        }

        public IObservable<Unit> GetNextCreate()
        {
            return nextCreate;
        }

        public IObservable<Unit> GetGameOver()
        {
            return gameOver;
        }
    }
}