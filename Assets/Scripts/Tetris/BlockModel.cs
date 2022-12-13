using UnityEngine;

namespace Tetris_Block
{
    public class BlockModel
    {
        private static int width = 10;
        private static int height = 20;

        private Transform[,] grid = new Transform[width, height];

        public void SetGrid(Transform block)
        {
            Vector3 pos = block.position;
            grid[(int)pos.x, (int)pos.y] = block;
        }

        public bool CheckGrid(int x_value,int y_value)
        {
            if (grid[x_value,y_value] != null)
            {
                return false;
            }
            return true;
        }

        public bool CheckLine(int height,int wight)
        {
            for(int i = 0; i < wight; i++)
            {
                if (grid[i,height] == null)
                {
                    return false;
                }
            }

            return true;
        }

        public int GetWidth()
        {
            return width;
        }

    }
}