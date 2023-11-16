using UnityEngine;
using UniRx;
using Tetris_Block;

namespace Tetris_UI
{
    public class ResultViewPresenter : MonoBehaviour
    {
        [SerializeField]
        private BlockManager _block;

        [SerializeField]
        private ResultView _view;

        void Start()
        {
            _block.IsGameOver
                .Subscribe(x =>
                {
                    _view.GameOver();
                })
                .AddTo(gameObject);
        }
    }
}