using UnityEngine;
using UniRx;
using System;
using UnityEngine.AI;

namespace PackMan_Enemy
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class EnemyController : MonoBehaviour
    {
        [SerializeField]
        private float speed = 2f;

        private NavMeshAgent agent;

        private Subject<Unit> rootend = new();

        private void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
        }

        void Start()
        {
            Init();

            rootend
                .Subscribe(x => Circulation())
                .AddTo(gameObject);
        }

        public void UpdateLoop()
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                rootend.OnNext(Unit.Default);
            }
        }

        public void TargetPlayer(Vector3 player)
        {
            agent.SetDestination(player);
        }

        private void Circulation()
        {
            agent.SetDestination(EnemyWayPoint.InitCallNumber().position);
        }

        private void Init()
        {
            agent.speed = speed;
            agent.SetDestination(EnemyWayPoint.InitCallNumber().position);
        }
    }
}
