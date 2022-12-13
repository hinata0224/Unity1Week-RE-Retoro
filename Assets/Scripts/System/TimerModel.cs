using UniRx;
using System;
using System.Diagnostics;
using System.Collections.Generic;
using UnityEngine;


namespace Other_System
{
    public class TimerModel
    {
        private Subject<bool> endTimer = new Subject<bool>();

        private IDisposable clock;

        public void StartTimer(int count)
        {
            clock = Observable.Timer(TimeSpan.FromSeconds(count))
                .Subscribe(x => endTimer.OnNext(true));
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

        public IObservable<bool> GetEndTimer()
        {
            return endTimer;
        }
    }
}