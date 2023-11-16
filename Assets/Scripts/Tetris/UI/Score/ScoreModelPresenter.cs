using UnityEngine;
using UniRx;

namespace Tetris_UI
{
    public class ScoreModelPresenter : MonoBehaviour
    {
        [SerializeField]
        private ScoreModelView _view;

        private ScoreModel model = new();
        void Start()
        {
            ScoreModel.ResetScore();

            _view.SetText(model.GetScorePoint());

            model.Score
                .Subscribe(x => _view.SetText(x))
                .AddTo(this);
        }
    }
}