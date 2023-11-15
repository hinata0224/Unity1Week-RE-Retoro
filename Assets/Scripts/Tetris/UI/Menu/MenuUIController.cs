using UnityEngine;
using UniRx;
using System;
using UnityEngine.UI;

namespace Tetris_UI
{
    public class MenuUIController : MonoBehaviour
    {
        [SerializeField]
        private GameObject Menu;
        [SerializeField]
        private Button _openButton;
        private bool isOpneMenu = true;

        private Subject<bool> openMenu = new();
        public IObservable<bool> IsOpenMenu => openMenu;

        public void Initializ()
        {
            _openButton.OnButtonObservable()
                .Subscribe(_ => DisplayMenu())
                .AddTo(gameObject);
        }

        private void DisplayMenu()
        {
            Menu.SetActive(isOpneMenu);
            openMenu.OnNext(isOpneMenu);
            isOpneMenu = !isOpneMenu;
        }
    }
}