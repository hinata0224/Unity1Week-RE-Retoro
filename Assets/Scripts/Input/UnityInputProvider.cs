using System;
using UniRx;
using UnityEngine;

namespace InputProvider
{
    public class UnityInputProvider : SingletonMonoBehaviour<UnityInputProvider>, IInputProvider
    {
        private ReactiveProperty<int> _onMoveObservable = new ReactiveProperty<int>(0);
        private Subject<Unit> _onRotateObservable = new Subject<Unit>();
        private Subject<Unit> _onDownObservable = new Subject<Unit>();

        public IReadOnlyReactiveProperty<int> OnMoveObservable => _onMoveObservable;
        public IObservable<Unit> OnRotateObservable => _onRotateObservable;
        public IObservable<Unit> OnDownObservable => _onDownObservable;

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
                _onMoveObservable.Value = -1;
            }
            else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                _onMoveObservable.Value = 1;
            }
            else
            {
                _onMoveObservable.Value = 0;
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
        }
    }
}