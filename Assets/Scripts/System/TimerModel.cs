using UniRx;
using System;
using System.Diagnostics;
using System.Collections.Generic;
using UnityEngine;


namespace Other_System
{
    public class TimerModel
    {
        private Subject<Unit> endTimer = new Subject<Unit>();

        private IDisposable clock;

        public void StartTimer(float count)
        {
            clock = Observable.Timer(TimeSpan.FromSeconds(count))
                .Subscribe(x => endTimer.OnNext(Unit.Default));
        }

        public void StopTimer()
        {
            clock.Dispose();
        }

        public void EndTimer()
        {
            clock.Dispose();
            endTimer.Dispose();
        }

        public IObservable<Unit> GetEndTimer()
        {
            return endTimer;
        }
    }
}