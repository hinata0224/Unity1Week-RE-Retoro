using UnityEngine;
using TMPro;
using UniRx;
using UnityEngine.UI;
using SceneController;

namespace Tetris_UI
{
    public class ResultView : MonoBehaviour
    {
        [SerializeField] private GameObject rezult;
        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private Button _titleButton;
        [SerializeField] private Button _moreButton;

        private ScoreModel model = new();

        private static Subject<Unit> gameOver = new();
        private SceneMangerController _sceneManger;

        private void Awake()
        {
            _sceneManger = SceneMangerController.Instance;
        }

        private void Start()
        {
            gameOver.First()
                .Subscribe(x => GameOver())
                .AddTo(this);

            _titleButton.OnButtonObservable()
                .Subscribe(_ => _sceneManger.GoTitle())
                .AddTo(gameObject);

            _moreButton.OnButtonObservable()
                .Subscribe(_ => _sceneManger.GoTetrisStart())
                .AddTo(gameObject);
        }

        public void GameOver()
        {
            rezult.SetActive(true);
            scoreText.text = "最終スコア：" + model.GetScorePoint();
            Time.timeScale = 0;
        }

        public static void GameOverStart()
        {
            gameOver.OnNext(Unit.Default);
        }
    }
}
