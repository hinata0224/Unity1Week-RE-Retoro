using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace PackMan_Enemy
{
    public class EnemyWayPoint : MonoBehaviour
    {
        private static List<Transform> waypoints;

        private static List<int> callnum = new(4);
        public static Subject<Unit> IsInitEnd = new Subject<Unit>();

        private void Awake()
        {
            Init();
        }

        private void Init()
        {
            waypoints = new(29);
            foreach (Transform child in gameObject.transform)
            {
                waypoints.Add(child);
            }
            IsInitEnd.OnNext(Unit.Default);
        }

        public static Transform InitCallNumber()
        {
            int call = Random.Range(0, waypoints.Count);
            if (callnum.Count != 0)
            {
                while (true)
                {
                    for (int i = 0; i < callnum.Count; i++)
                    {
                        if (callnum[i] == call)
                        {
                            continue;
                        }
                    }
                    break;
                }
            }
            callnum.Add(call);
            return waypoints[call];
        }

        public static Transform GetWayPoint(Transform point)
        {
            for (int i = 0; i < waypoints.Count; i++)
            {
                if (waypoints[i] == point)
                {
                    callnum.RemoveAt(i);
                }
            }
            int call = Random.Range(0, waypoints.Count);
            if (callnum.Count != 0)
            {
                while (true)
                {
                    for (int i = 0; i < callnum.Count; i++)
                    {
                        if (callnum[i] == call)
                        {
                            continue;
                        }
                    }
                    break;
                }
            }
            callnum.Add(call);
            return waypoints[callnum[call]];
        }
    }
}
