using UnityEngine;
using UniRx;

namespace Tetris_UI {
    public class ScoreModelPresenter : MonoBehaviour
    {
        [SerializeField, Header("�Q�ƃX�N���v�g")]
        private ScoreModelController controller;

        private ScoreModel model = new();
        void Start()
        {
            controller.SetText(model.GetScorePoint());
            model.GetScore()
                .Subscribe(x => controller.SetText(x))
                .AddTo(this);
        }
    }
}