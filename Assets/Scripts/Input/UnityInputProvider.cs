using System;
using UniRx;
using UnityEngine;

namespace InputProvider
{
    public class UnityInputProvider : SingletonMonoBehaviour<UnityInputProvider>, IInputProvider
    {
        private ReactiveProperty<int> _onHorizontalObservable = new ReactiveProperty<int>(0);
        private Subject<Unit> _onRotateObservable = new Subject<Unit>();
        private Subject<Unit> _onDownObservable = new Subject<Unit>();
        private ReactiveProperty<Vector2> _onMoveObservable = new ReactiveProperty<Vector2>();

        public IReadOnlyReactiveProperty<int> OnHorizontalObservable => _onHorizontalObservable;
        public IObservable<Unit> OnRotateObservable => _onRotateObservable;
        public IObservable<Unit> OnDownObservable => _onDownObservable;
        public IReadOnlyReactiveProperty<Vector2> OnMoveObservable => _onMoveObservable;


        private void Update()
        {
            HandleMoveButton();
        }

        /// <summary>
        /// 移動系の入力を処理
        /// </summary>
        private void HandleMoveButton()
        {
            // 移動の入力
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                _onHorizontalObservable.Value = -1;
            }
            else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                _onHorizontalObservable.Value = 1;
            }
            else
            {
                _onHorizontalObservable.Value = 0;
            }

            // 回転の入力
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                _onRotateObservable.OnNext(Unit.Default);
            }

            // 下入力
            if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                _onDownObservable.OnNext(Unit.Default);
            }

            _onMoveObservable.Value = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        }
    }
}
