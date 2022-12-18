using UnityEngine;
using UniRx;
using System;

namespace PackMan_Item
{
    public class ItemController : MonoBehaviour
    {
        private static ReactiveProperty<int> itemCount = new(0);

        private static Subject<Unit> gameClear = new();

        private void Awake()
        {
            Init();
        }

        private void Init()
        {
            foreach(Transform child in gameObject.transform)
            {
                itemCount.Value++;
            }
        }

        public static void GetItem()
        {
            itemCount.Value--;
            if(itemCount.Value == 0)
            {
                gameClear.OnNext(Unit.Default);
            }
        }

        public static IObservable<int> GetItemCount()
        {
            return itemCount;
        }

        public static IObservable<Unit> GetGameClear()
        {
            return gameClear;
        }
    }
}