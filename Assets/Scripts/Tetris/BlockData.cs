using System.Collections.Generic;
using UnityEngine;
using Other_System;
using UniRx;
using UnityEngine.InputSystem;

namespace Tetris_Block
{
    public class BlockData : MonoBehaviour
    {
        [SerializeField, Header("置いた時の待機時間")]
        private int locateTime = 1;

        private int width;

        private float x_value;
        private float y_value;

        private Transform grid;

        [SerializeField, Header("子オブジェクトのブロック")]
        private List<GameObject> blocks;

        private bool move = true;

        private Subject<bool> moveInput = new();

        private BlockModel model = new();
        private TimerModel timer = new();

        void Start()
        {
            timer.StartTimer(locateTime);
            timer.GetEndTimer()
                .Subscribe(x => DownBlock())
                .AddTo(this);

            moveInput.Where(x => x)
                .Subscribe(x =>
                {
                    MoveBlock();
                }).AddTo(this);

            moveInput.Where(x => x && Mathf.Sign(y_value) == 1)
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
        }

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
            move = false;
            timer.StopTimer();
            transform.Rotate(0, 0, 90);
            moveInput.OnNext(false);
        }

        private void DownBlock()
        {
            timer.StopTimer();
            transform.position -= new Vector3(0, 1, 0);
            if (!ValidMovement())
            {
                transform.position += new Vector3(0, 1, 0);
                timer.EndTimer();
                SetPosition();
            }
            else
            {
                timer.StartTimer(locateTime);
            }
        }


        private bool ValidMovement()
        {
            foreach (Transform children in transform)
            {
                int roundX = Mathf.RoundToInt(children.transform.position.x);
                int roundY = Mathf.RoundToInt(children.transform.position.y);

                if (roundX < 0 || roundX >= width || roundY < 0)
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

        public void Init(Transform gridObj)
        {
            grid = gridObj;
            width = model.GetWidth();
        }

        public void InputValue(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                if (context.ReadValue<Vector2>().x != 0)
                {
                    x_value = context.ReadValue<Vector2>().x;
                }
                else
                {
                    x_value = 0;
                }
                if (context.ReadValue<Vector2>().y > 0)
                {
                    y_value = context.ReadValue<Vector2>().y;
                }
                else
                {
                    y_value = 0;
                }
                moveInput.OnNext(move);
            }
        }

        public void SetPosition()
        {
            timer = new();
            timer.StartTimer(locateTime);
            timer.GetEndTimer()
                .Where(x => x)
                .Subscribe(x =>
                {
                    for (int i = 0; i < blocks.Count; i++)
                    {
                        model.SetGrid(blocks[i].transform);
                        blocks[i].transform.parent = grid;
                    }

                    DestroyObj();
                }).AddTo(this);
        }

        private void DestroyObj()
        {
            timer.StopTimer();
            Destroy(this.gameObject);
        }
    }
}