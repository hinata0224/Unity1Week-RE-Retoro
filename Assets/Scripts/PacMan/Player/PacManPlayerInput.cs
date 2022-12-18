using UnityEngine;
using UniRx;
using System;
using UnityEngine.InputSystem;

namespace PackMan_Player
{
    public class PacManPlayerInput : MonoBehaviour
    {

        private static Subject<Vector2> getInput = new();
        public void InputValue(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                Vector2 vec = new Vector2();
                if (context.ReadValue<Vector2>().x != 0)
                {
                    vec.x = context.ReadValue<Vector2>().x;
                }
                else
                {
                    vec.x = 0;
                }
                if (context.ReadValue<Vector2>().y != 0)
                {
                    vec.y = context.ReadValue<Vector2>().y;
                }
                else
                {
                    vec.y = 0;
                }
                getInput.OnNext(vec);
            }
        }

        public static IObservable<Vector2> GetInput()
        {
            return getInput;
        }
    }
}