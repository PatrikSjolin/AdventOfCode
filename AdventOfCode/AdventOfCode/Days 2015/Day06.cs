using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Days_2015
{
    public class Day06 : IPuzzle
    {
        public string RunOne()
        {
            List<string> inputLines = System.IO.File.ReadAllLines(@"..\..\Data\2015\input06.txt").ToList();

            bool[,] grid = new bool[1000, 1000];

            foreach(var s in inputLines)
            {
                if (s.StartsWith("toggle"))
                {
                    List<string> split = s.Split(' ').ToList();

                    string c1 = split[1];
                    int x1 = int.Parse(c1.Split(',')[0]);
                    int y1 = int.Parse(c1.Split(',')[1]);
                    string c2 = split[3];
                    int x2 = int.Parse(c2.Split(',')[0]);
                    int y2 = int.Parse(c2.Split(',')[1]);

                    for(int i = x1; i <= x2; i++)
                    {
                        for(int j = y1; j <= y2; j++)
                        {
                            grid[i, j] = !grid[i, j];
                        }
                    }

                }
                if(s.StartsWith("turn on"))
                {
                    List<string> split = s.Split(' ').ToList();

                    string c1 = split[2];
                    int x1 = int.Parse(c1.Split(',')[0]);
                    int y1 = int.Parse(c1.Split(',')[1]);
                    string c2 = split[4];
                    int x2 = int.Parse(c2.Split(',')[0]);
                    int y2 = int.Parse(c2.Split(',')[1]);

                    for (int i = x1; i <= x2; i++)
                    {
                        for (int j = y1; j <= y2; j++)
                        {
                            grid[i, j] = true;
                        }
                    }
                }
                if (s.StartsWith("turn off"))
                {
                    List<string> split = s.Split(' ').ToList();

                    string c1 = split[2];
                    int x1 = int.Parse(c1.Split(',')[0]);
                    int y1 = int.Parse(c1.Split(',')[1]);
                    string c2 = split[4];
                    int x2 = int.Parse(c2.Split(',')[0]);
                    int y2 = int.Parse(c2.Split(',')[1]);

                    for (int i = x1; i <= x2; i++)
                    {
                        for (int j = y1; j <= y2; j++)
                        {
                            grid[i, j] = false;
                        }
                    }
                }
            }

            int count = 0;
            for(int i = 0; i < 1000; i++)
            {
                for(int j = 0; j < 1000; j++)
                {
                    if (grid[i, j])
                        count++;
                }
            }

            return count.ToString();
        }

        public string RunTwo()
        {
            List<string> inputLines = System.IO.File.ReadAllLines(@"..\..\Data\2015\input06.txt").ToList();

            int[,] grid = new int[1000, 1000];

            foreach (var s in inputLines)
            {
                if (s.StartsWith("toggle"))
                {
                    List<string> split = s.Split(' ').ToList();

                    string c1 = split[1];
                    int x1 = int.Parse(c1.Split(',')[0]);
                    int y1 = int.Parse(c1.Split(',')[1]);
                    string c2 = split[3];
                    int x2 = int.Parse(c2.Split(',')[0]);
                    int y2 = int.Parse(c2.Split(',')[1]);

                    for (int i = x1; i <= x2; i++)
                    {
                        for (int j = y1; j <= y2; j++)
                        {
                            grid[i, j] += 2;
                        }
                    }

                }
                if (s.StartsWith("turn on"))
                {
                    List<string> split = s.Split(' ').ToList();

                    string c1 = split[2];
                    int x1 = int.Parse(c1.Split(',')[0]);
                    int y1 = int.Parse(c1.Split(',')[1]);
                    string c2 = split[4];
                    int x2 = int.Parse(c2.Split(',')[0]);
                    int y2 = int.Parse(c2.Split(',')[1]);

                    for (int i = x1; i <= x2; i++)
                    {
                        for (int j = y1; j <= y2; j++)
                        {
                            grid[i, j]++;
                        }
                    }
                }
                if (s.StartsWith("turn off"))
                {
                    List<string> split = s.Split(' ').ToList();

                    string c1 = split[2];
                    int x1 = int.Parse(c1.Split(',')[0]);
                    int y1 = int.Parse(c1.Split(',')[1]);
                    string c2 = split[4];
                    int x2 = int.Parse(c2.Split(',')[0]);
                    int y2 = int.Parse(c2.Split(',')[1]);

                    for (int i = x1; i <= x2; i++)
                    {
                        for (int j = y1; j <= y2; j++)
                        {
                            if (grid[i, j] > 0)
                                grid[i, j]--;
                        }
                    }
                }
            }

            int count = 0;
            for (int i = 0; i < 1000; i++)
            {
                for (int j = 0; j < 1000; j++)
                {
                    count += grid[i, j];
                }
            }

            return count.ToString();
        }
    }
}
