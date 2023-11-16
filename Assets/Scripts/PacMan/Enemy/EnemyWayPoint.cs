using System.Collections.Generic;
using UnityEngine;

namespace PackMan_Enemy
{
    public class EnemyWayPoint : MonoBehaviour
    {
        private static List<Transform> waypoints = new(29);

        private static List<int> callnum = new(4);

        private void Awake()
        {
            Init();
        }

        private void Init()
        {
            foreach (Transform child in gameObject.transform)
            {
                waypoints.Add(child);
            }
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
