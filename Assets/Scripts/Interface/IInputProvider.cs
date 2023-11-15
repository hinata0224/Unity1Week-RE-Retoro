using UniRx;
using System;

namespace InputProvider
{
    public interface IInputProvider
    {
        /// <summary>
        /// 移動ボタンの入力イベント
        /// 左右を想定
        /// </summary>
        public IReadOnlyReactiveProperty<int> OnMoveObservable { get; }
        /// <summary>
        /// 回転ボタンの入力イベント
        /// </summary>
        public IObservable<Unit> OnRotateObservable { get; }
        /// <summary>
        /// 移動ボタンの入力イベント
        /// 下に落とすボタンを想定
        /// </summary>
        public IObservable<Unit> OnDownObservable { get; }
    }
}
