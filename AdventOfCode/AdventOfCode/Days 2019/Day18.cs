using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Days_2019
{
    public class Day18 : IPuzzle
    {
        public bool Active => false;

        public string RunOne()
        {
            List<string> inputs = System.IO.File.ReadAllLines(@"..\..\Data\2019\input18test1.txt").ToList();

            int width = inputs[0].Length;
            int height = inputs.Count;

            Tuple<int, int> start;

            string[,] grid = new string[width, height];

            for(int i = 0; i < width; i++)
            {
                for(int j = 0; j < height; j++)
                {
                    grid[i, j] = inputs[j][i].ToString();
                    if (grid[i, j] == "@")
                        start = new Tuple<int, int>(i, j);
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
