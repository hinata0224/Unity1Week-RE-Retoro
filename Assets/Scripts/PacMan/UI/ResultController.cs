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

        private PlayerController _playerController;
        private ItemController _itemController;

        private void Awake()
        {
            _playerController = GameObject.FindGameObjectWithTag(TagName.Player).GetComponent<PlayerController>();
            _itemController = GameObject.FindGameObjectWithTag(TagName.ItemController).GetComponent<ItemController>();
        }

        void Start()
        {
            gameOverResult.SetActive(false);
            gameClearResult.SetActive(false);
            poos.SetActive(false);

            _playerController.IsGameOver
                .Subscribe(x =>
                {
                    gameOverResult.SetActive(true);
                }).AddTo(this);

            _itemController.IsGameCler
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
