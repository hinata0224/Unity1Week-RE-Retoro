using UnityEngine;
using TMPro;
using UniRx;
using System;

namespace Tetris_UI
{
    public class ResultController : MonoBehaviour
    {
        [SerializeField]
        private GameObject rezult;

        [SerializeField]
        private TextMeshProUGUI scoreText;

        private ScoreModel model = new();

        private static Subject<Unit> gameOver = new();

        private void Start()
        {
            gameOver.First()
                .Subscribe(x => GameOver())
                .AddTo(this);
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