using UnityEngine;
using UniRx;
using System;

namespace PackMan_Item
{
    public class ItemController : MonoBehaviour
    {
        private ReactiveProperty<int> _rpItemCount = new(0);
        public IReadOnlyReactiveProperty<int> RPItemCount => _rpItemCount;

        private Subject<Unit> gameClear = new();
        public IObservable<Unit> IsGameCler => gameClear;

        private void Awake()
        {
            Init();
        }

        private void Init()
        {
            foreach (Transform child in gameObject.transform)
            {
                _rpItemCount.Value++;
            }
        }

        public void GetItem()
        {
            _rpItemCount.Value--;
            if (_rpItemCount.Value == 0)
            {
                gameClear.OnNext(Unit.Default);
            }
        }
    }
}
