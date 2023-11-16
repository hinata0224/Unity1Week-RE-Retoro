using UnityEngine;
using UniRx;
using PackMan_Player;
using PackMan_Item;

namespace PackMan_UI
{
    public class ResultController : MonoBehaviour
    {
        // NOTE: WebGLBuildのためAddressableではなく参照
        [SerializeField]
        private GameObject _gameOverResult;
        // NOTE: WebGLBuildのためAddressableではなく参照
        [SerializeField]
        private GameObject _gameClearResult;

        private PlayerController _playerController;
        private ItemController _itemController;

        private void Awake()
        {
            _playerController = GameObject.FindGameObjectWithTag(TagName.Player).GetComponent<PlayerController>();
            _itemController = GameObject.FindGameObjectWithTag(TagName.ItemController).GetComponent<ItemController>();
        }

        void Start()
        {
            _gameOverResult.SetActive(false);
            _gameClearResult.SetActive(false);

            _playerController.IsGameOver
                .Subscribe(x =>
                {
                    _gameOverResult.SetActive(true);
                }).AddTo(gameObject);

            _itemController.IsGameCler
                .Subscribe(x =>
                {
                    _gameClearResult.SetActive(true);
                }).AddTo(gameObject);
        }
    }
}
