using UnityEngine;
using UniRx;
using Tetris_Block;

namespace Tetris_UI
{
    public class ResultControllerPresenter : MonoBehaviour
    {
        [SerializeField, Header("�Q�ƃX�N���v�g")]
        private BlockManager block;

        [SerializeField]
        private ResultController controller;

        void Start()
        {
            block.IsGameOver
                .Subscribe(x =>
                {
                    Debug.Log("ssss");
                    controller.GameOver();
                })
                .AddTo(block);
        }
    }
}