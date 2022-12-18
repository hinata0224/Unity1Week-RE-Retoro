using UnityEngine;
using UniRx;
using System;

namespace PackMan_Player
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerController : MonoBehaviour
    {
        [Header("スピード調整")]
        [SerializeField] private float spead = 5;

        [SerializeField]
        private int _hp = 3;

        private static ReactiveProperty<int> hp = new();

        [SerializeField]
        private Transform initpos;

        private Vector3 move;

        private Rigidbody rb;

        private static Subject<Unit> gameOver = new();

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
            hp.Value = _hp;
        }

        private void Start()
        {
            PacManPlayerInput.GetInput()
                .Subscribe(x =>
                {
                    DirextionSet(x);
                }).AddTo(this);
        }
        void Update()
        {
            PlayerMove();
        }

        private void DirextionSet(Vector2 vec)
        {
            rb.velocity = new(0, 0, 0);
            Vector3 pos;
            pos = transform.position;
            if (vec.x != 0)
            {
                if (Mathf.Sign(vec.x) == 1)
                {
                    pos.x += 1;
                }
                else
                {
                    pos.x -= 10;
                }
            }
            else if(vec.y != 0)
            {
                if (Mathf.Sign(vec.y) == 1)
                {
                    pos.z += 1;
                }
                else
                {
                    pos.z += -10;
                }
            }
            transform.LookAt(pos);
        }

        private void PlayerMove()
        {
            move = new(0, 0, spead);
            move = transform.TransformDirection(move);
            rb.velocity = move;
        }

        public void Dead()
        {
            _hp--;
            hp.Value = _hp;
            if(_hp <= 0)
            {

            }
            transform.position = initpos.position;
        }

        public static IObservable<int> GetHP()
        {
            return hp;
        }

        public static IObservable<Unit> GetGameOver()
        {
            return gameOver;
        }
    }
}