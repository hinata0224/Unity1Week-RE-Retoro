using UnityEngine;
using UniRx;
using Tetris_Block;

namespace Tetris_UI
{
    public class ResultControllerPresenter : MonoBehaviour
    {
        [SerializeField]
        private BlockManager block;

        [SerializeField]
        private ResultController controller;

        void Start()
        {
            block.IsGameOver
                .Subscribe(x =>
                {
                    controller.GameOver();
                })
                .AddTo(block);
        }
    }
}