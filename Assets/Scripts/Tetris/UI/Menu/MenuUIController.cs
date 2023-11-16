using UnityEngine;
using UniRx;
using System;
using UnityEngine.UI;
using SceneController;

namespace Tetris_UI
{
    public class MenuUIController : MonoBehaviour
    {
        [SerializeField] private GameObject Menu;
        [SerializeField] private Button _openButton;
        [SerializeField] private Button _titleButton;
        [SerializeField] private Button _moreButton;

        private bool isOpneMenu = true;

        private Subject<bool> openMenu = new();
        public IObservable<bool> IsOpenMenu => openMenu;
        private SceneMangerController _sceneManger;

        private void Awake()
        {
            _sceneManger = SceneMangerController.Instance;
        }

        public void Initializ()
        {
            _openButton.OnButtonObservable()
                .Subscribe(_ => DisplayMenu())
                .AddTo(gameObject);

            _titleButton.OnButtonObservable()
                .Subscribe(_ => _sceneManger.GoTitle())
                .AddTo(gameObject);

            _moreButton.OnButtonObservable()
                .Subscribe(_ => _sceneManger.GoTetrisStart())
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
