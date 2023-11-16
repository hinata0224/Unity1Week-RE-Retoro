using UnityEngine;
using UniRx;
using PackMan_Item;
using PackMan_Player;

namespace PackMan_UI
{
    public class ScoreHpPresenter : MonoBehaviour
    {
        [SerializeField]
        private ScoreHPView view;

        private PlayerController _playerController;
        private ItemController _itemController;

        private void Awake()
        {
            _playerController = GameObject.FindGameObjectWithTag(TagName.Player).GetComponent<PlayerController>();
            _itemController = GameObject.FindGameObjectWithTag(TagName.ItemController).GetComponent<ItemController>();
        }


        private void Start()
        {
            _itemController.RPItemCount
                .Subscribe(x =>
                {
                    view.SetScore(x);
                }).AddTo(this);

            _playerController.RPHp
                .Subscribe(x =>
                {
                    view.SetHp(x);
                }).AddTo(this);
        }

    }
}
