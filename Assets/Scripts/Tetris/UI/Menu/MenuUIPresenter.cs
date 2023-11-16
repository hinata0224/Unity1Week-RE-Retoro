using UnityEngine;
using UniRx;

namespace Tetris_UI
{
    public class MenuUIPresenter : MonoBehaviour
    {
        [SerializeField]
        private MenuUIController controller;

        private MenuUIModel model = new();
        void Start()
        {
            controller.Initializ();

            controller.IsOpenMenu
                .Where(x => x)
                .Subscribe(x => model.StopTime())
                .AddTo(this);

            controller.IsOpenMenu
                .Where(x => !x)
                .Subscribe(x => model.StartTime())
                .AddTo(this);
        }
    }
}