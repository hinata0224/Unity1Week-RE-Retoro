using UnityEngine;
using UniRx;
using PackMan_Item;
using PackMan_Player;

namespace PackMan_UI
{
    public class ScoreHpControllerPresenter : MonoBehaviour
    {
        [SerializeField]
        private ScoreHPController controller;

        private PlayerController playerController;

        private void Awake()
        {
            playerController = GameObject.FindWithTag(TagName.Player).GetComponent<PlayerController>();
        }


        private void Start()
        {
            ItemController.GetItemCount()
                .Subscribe(x =>
                {
                    controller.SetScore(x);
                }).AddTo(this);

            playerController.RPHp
                .Subscribe(x =>
                {
                    controller.SetHp(x);
                }).AddTo(this);
        }

    }
}
