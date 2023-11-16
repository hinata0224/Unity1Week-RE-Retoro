using UnityEngine;
using UniRx;
using System;
using InputProvider;

namespace PackMan_Player
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerController : MonoBehaviour
    {
        [Header("スピード調整")]
        [SerializeField] private float spead = 5;

        [SerializeField]
        private int _hp = 3;

        private ReactiveProperty<int> _rpHp = new();
        public IReadOnlyReactiveProperty<int> RPHp => _rpHp;

        [SerializeField]
        private Transform initpos;

        private Vector3 move;

        private Rigidbody rb;
        private IInputProvider inputProvider;

        private static Subject<Unit> gameOver = new();
        public static IObservable<Unit> IsGameOver => gameOver;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
            inputProvider = UnityInputProvider.Instance;
            _rpHp.Value = _hp;
        }

        private void Start()
        {
            inputProvider.OnMoveObservable
                .Subscribe(x =>
                {
                    DirextionSet(x);
                }).AddTo(gameObject);
        }
        void Update()
        {
            PlayerMove();
        }

        /// <summary>
        /// 移動方向に向く
        /// </summary>
        /// <param name="vec"></param>
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
            else if (vec.y != 0)
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

        /// <summary>
        /// 移動処理
        /// </summary>
        private void PlayerMove()
        {
            move = new(0, 0, spead);
            move = transform.TransformDirection(move);
            rb.velocity = move;
        }

        /// <summary>
        /// ダメージ処理
        /// </summary>
        public void HitDamage()
        {
            _hp--;
            _rpHp.Value = _hp;
            if (_hp <= 0)
            {
                gameOver.OnNext(Unit.Default);
            }
            transform.position = initpos.position;
        }
    }
}
