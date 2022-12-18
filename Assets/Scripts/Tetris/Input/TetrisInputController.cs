using Tetris_Block;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Tetris_Input
{
    public class TetrisInputController : MonoBehaviour
    {
        private static BlockData block;

        public void InputValue(InputAction.CallbackContext context)
        {
            if (context.started && block != null)
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
                block.InputValue(vec);
            }
        }

        public static void SetBlockData(BlockData data)
        {
            block = data;
        }
    }
}