using UnityEngine;
using UniRx;

namespace Tetris_UI
{
    public class ScoreModelPresenter : MonoBehaviour
    {
        [SerializeField]
        private ScoreModelController controller;

        private ScoreModel model = new();
        void Start()
        {
            ScoreModel.ResetScore();

            controller.SetText(model.GetScorePoint());

            model.Score
                .Subscribe(x => controller.SetText(x))
                .AddTo(this);
        }
    }
}