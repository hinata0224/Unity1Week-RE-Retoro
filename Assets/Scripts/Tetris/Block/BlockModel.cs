using System.Collections.Generic;
using UnityEngine;

namespace Tetris_Block
{
    public class BlockModel
    {
        private static int width = 10;
        private static int height = 15;

        private static Transform[,] grid = new Transform[width, height];

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

        public bool CheckLine(int height)
        {
            for(int i = 0; i < width; i++)
            {
                if (grid[i,height] == null)
                {
                    return false;
                }
            }

            return true;
        }

        public List<GameObject> DeketeLine(List<int> count)
        {
            List<GameObject> returnlist = new(20);
            for (int i = 0; i < count.Count; i++)
            {
                for(int j = 0; j < width; j++)
                {
                    returnlist.Add(grid[j, count[i]].gameObject);
                    grid[j, count[i]] = null;
                }
            }
            return returnlist;
        }

        public void RowDown(List<int> count)
        {
            for(int i = 0; i < count.Count; i++)
            {
                for (int h = count[i]; h < height; h++)
                {
                    for (int j = 0; j < width; j++)
                    {
                        if (grid[j, h] != null)
                        {
                            grid[j, h - 1] = grid[j, h];
                            grid[j, h] = null;
                            grid[j, h - 1].transform.position -= new Vector3(0, 1, 0);
                        }
                    }
                }
            }
        }

        public int GetWidth()
        {
            return width;
        }

        public int GetHeight()
        {
            return height;
        }
    }
}