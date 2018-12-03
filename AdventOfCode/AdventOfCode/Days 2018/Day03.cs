using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Days_2018
{
    public class Day03 : IPuzzle
    {
        int[,] grid = new int[1000, 1000];

        public string RunOne()
        {
            List<string> input = System.IO.File.ReadAllLines(@"..\..\Data\2018\input03.txt").ToList();

            foreach (var line in input)
            {
                List<string> split = line.Split(' ').ToList();
                string coord = split[2];
                string size = split[3];

                List<string> coords = coord.Split(',').ToList();
                int c1 = int.Parse(coords[0]);
                int c2 = int.Parse(coords[1].Split(':')[0]);

                List<string> sizes = size.Split('x').ToList();

                int s1 = int.Parse(sizes[0]);
                int s2 = int.Parse(sizes[1]);
                for (int i = c1; i < c1 + s1; i++)
                {
                    for (int j = c2; j < c2 + s2; j++)
                    {
                        grid[i, j]++;
                    }
                }
            }
            int count = 0;
            for (int i = 0; i < 1000; i++)
            {
                for (int j = 0; j < 1000; j++)
                {
                    if (grid[i, j] > 1)
                        count++;
                }
            }

            return count.ToString();
        }

        public string RunTwo()
        {
            List<string> input = System.IO.File.ReadAllLines(@"..\..\Data\2018\input03.txt").ToList();

            foreach (var line in input)
            {
                List<string> split = line.Split(' ').ToList();
                string coord = split[2];
                string size = split[3];

                List<string> coords = coord.Split(',').ToList();
                int c1 = int.Parse(coords[0]);
                int c2 = int.Parse(coords[1].Split(':')[0]);

                List<string> sizes = size.Split('x').ToList();

                int s1 = int.Parse(sizes[0]);
                int s2 = int.Parse(sizes[1]);
                bool found = true;
                for (int i = c1; i < c1 + s1; i++)
                {
                    for (int j = c2; j < c2 + s2; j++)
                    {
                        if (grid[i, j] != 1)
                            found = false;
                    }
                }

                if (found)
                {
                    return line.Split('#')[1];
                }
            }

            return "";
        }
    }
}
