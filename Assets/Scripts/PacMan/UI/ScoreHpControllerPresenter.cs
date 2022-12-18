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


        private void Start()
        {
            ItemController.GetItemCount()
                .Subscribe(x =>
                {
                    controller.SetScore(x);
                }).AddTo(this);

            PlayerController.GetHP()
                .Subscribe(x =>
                {
                    controller.SetHp(x);
                }).AddTo(this);
        }

    }
}