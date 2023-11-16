using UnityEngine;
using UniRx;
using PackMan_Player;
using PackMan_Item;

namespace PackMan_UI
{
    public class ResultController : MonoBehaviour
    {
        [SerializeField, Header("Result���")]
        private GameObject gameOverResult;

        [SerializeField, Header("gameClear���")]
        private GameObject gameClearResult;

        [SerializeField, Header("�|�[�Y���")]
        private GameObject poos;

        [SerializeField, Header("�|�[�Y�{�^��")]
        private GameObject poseButton;


        void Start()
        {
            gameOverResult.SetActive(false);
            gameClearResult.SetActive(false);
            poos.SetActive(false);

            PlayerController.IsGameOver
                .Subscribe(x =>
                {
                    gameOverResult.SetActive(true);
                }).AddTo(this);

            ItemController.GetGameClear()
                .Subscribe(x =>
                {
                    gameClearResult.SetActive(true);
                }).AddTo(this);
        }

        public void DisplayPoos()
        {
            Time.timeScale = 0;
            poos.SetActive(true);
            poseButton.SetActive(false);
        }

        public void ClosePoos()
        {
            Time.timeScale = 1;
            poos.SetActive(false);
            poseButton.SetActive(true);
        }
    }
}
