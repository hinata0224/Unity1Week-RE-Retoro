using UnityEngine;
using UniRx;

namespace Tetris_UI
{
    public class MenuUIPresenter : MonoBehaviour
    {
        [SerializeField, Header("�Q�ƃX�N���v�g")]
        private MenuUIController controller;

        private MenuUIModel model = new();
        void Start()
        {
            controller.GetOpenMenu()
                .Where(x => x)
                .Subscribe(x => model.StopTime())
                .AddTo(this);

            controller.GetOpenMenu()
                .Where(x => !x)
                .Subscribe(x => model.StartTime())
                .AddTo(this);
        }
    }
}