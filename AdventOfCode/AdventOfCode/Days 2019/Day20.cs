using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Days_2019
{
    public class Day20 : IPuzzle
    {
        public bool Active => false;

        public string RunOne()
        {
            List<string> inputs = System.IO.File.ReadAllLines(@"..\..\Data\2019\input20test1.txt").ToList();

            int xSize = inputs[0].Length;
            int ySize = inputs.Count;

            int xStart = 0;
            int yStart = 0;
            int xGoal = 0;
            int yGoal = 0;

            string[,] grid = new string[xSize, ySize];

            for(int i = 0; i < xSize; i++)
            {
                for(int j = 0; j < ySize; j++)
                {
                    grid[i, j] = inputs[j][i].ToString();

                    if(grid[i, j] == "A" && grid[i, j + 1] == "A")
                    {

                    }
                }
            }

            bool found = false;

            for (int i = 0; i < xSize; i++)
            {
                if (found)
                    break;
                for (int j = 0; j < ySize; j++)
                {
                    if (grid[i, j] == "A" && grid[i, j + 1] == "A")
                    {
                        xStart = i;
                        yStart = j + 2;
                    }
                    if (grid[i, j] == "Z" && grid[i, j + 1] == "Z")
                    {
                        xGoal = i;
                        yGoal = j - 1;
                        found = true;
                        break;
                    }
                }
            }



            return "";
        }

        public string RunTwo()
        {
            throw new NotImplementedException();
        }
    }
}
