using UnityEngine;
using TMPro;
using UniRx;
using System;

namespace Tetris_UI
{
    public class ResultController : MonoBehaviour
    {
        [SerializeField, Header("リザルトの親オブジェクト")]
        private GameObject rezult;

        [SerializeField, Header("リザルトスコア")]
        private TextMeshProUGUI scoreText;

        private ScoreModel model = new();

        private static Subject<Unit> gameOver = new();

        private void Awake()
        {
            rezult.SetActive(false);
        }

        private void Start()
        {
            gameOver.First()
                .Subscribe(x => GameOver())
                .AddTo(this);
        }

        public void GameOver()
        {
            rezult.SetActive(true);
            scoreText.text = "今回のスコアは" + model.GetScorePoint() + "です。";
            Time.timeScale = 0;
        }

        public static void GameOverStart()
        {
            gameOver.OnNext(Unit.Default);
        }
    }
}