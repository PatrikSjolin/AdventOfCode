using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Days_2020
{
    public class Day03 : IPuzzle
    {
        public bool Active => true;

        public string RunOne()
        {
            List<string> list = System.IO.File.ReadAllLines(@"..\..\Data\2020\input03.txt").ToList();

            string[,] map = new string[list.Count, list[0].Length];
            for(int i = 0; i < list.Count; i++)
            {
                for(int j = 0; j < list[0].Length; j++)
                {
                    map[i, j] = list[i][j].ToString();
                }
            }

            int right = 3;
            int down = 1;

            int x = 0;
            int y = 0;

            int numTrees = 0;

            while(y < list.Count)
            {
                if(map[y % list.Count, x % list[0].Length] == "#")
                {
                    numTrees++;
                }
                y += down;
                x += right;
            }

            return numTrees.ToString();
        }

        public string RunTwo()
        {
            List<string> list = System.IO.File.ReadAllLines(@"..\..\Data\2020\input03.txt").ToList();

            List<KeyValuePair<int, int>> slopes = new List<KeyValuePair<int, int>>()
            {
                new KeyValuePair<int, int>(1, 1),
                new KeyValuePair<int, int>(3, 1),
                new KeyValuePair<int, int>(5, 1),
                new KeyValuePair<int, int>(7, 1),
                new KeyValuePair<int, int>(1, 2)
            };

            string[,] map = new string[list.Count, list[0].Length];
            for (int i = 0; i < list.Count; i++)
            {
                for (int j = 0; j < list[0].Length; j++)
                {
                    map[i, j] = list[i][j].ToString();
                }
            }

            int product = 1;

            foreach (var slope in slopes)
            {
                int right = slope.Key;
                int down = slope.Value;

                int x = 0;
                int y = 0;

                int numTrees = 0;

                while (y < list.Count)
                {
                    if (map[y % list.Count, x % list[0].Length] == "#")
                    {
                        numTrees++;
                    }
                    y += down;
                    x += right;
                }
                product *= numTrees;
            }
            return product.ToString();
        }
    }
}
