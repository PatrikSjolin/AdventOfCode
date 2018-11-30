using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Days
{
    public class Day19 : IPuzzle
    {
        int steps;
        public string RunOne()
        {
            List<string> inputLines = System.IO.File.ReadAllLines(@"..\..\Data\2017\input19.txt").ToList();

            Tuple<int, int> direction = new Tuple<int, int>(0, 1);

            Tuple<int, int> north = new Tuple<int, int>(0, -1);
            Tuple<int, int> south = new Tuple<int, int>(0, 1);
            Tuple<int, int> west = new Tuple<int, int>(-1, 0);
            Tuple<int, int> east = new Tuple<int, int>(1, 0);

            int x = 0;
            int y = 0;

            string start = inputLines[0];

            int maxX = start.Length;
            int maxY = inputLines.Count;

            x = start.IndexOf('|');
            steps = 0;
            string key = "";

            while (true)
            {
                steps++;
                x += direction.Item1;
                y += direction.Item2;

                char c = inputLines[y][x];

                if (c == ' ')
                {
                    break;
                }
                if (c == '|' || c == '-')
                {
                    continue;
                }
                else if (c == '+')
                {
                    if (direction.Item1 == 0)
                    {
                        if (x + 1 < maxX && (inputLines[y][x + 1] == '-' || char.IsLetter(inputLines[y][x + 1])))
                        {
                            direction = east;
                        }
                        else
                        {
                            direction = west;
                        }
                    }
                    else
                    {
                        if (y + 1 < maxY && (inputLines[y + 1][x] == '|' || char.IsLetter(inputLines[y + 1][x])))
                        {
                            direction = south;
                        }
                        else
                        {
                            direction = north;
                        }
                    }
                }
                else
                {
                    key += c;
                }
            }

            return key;
        }

        public string RunTwo()
        {
            List<string> inputLines = System.IO.File.ReadAllLines(@"..\..\Data\2017\input19.txt").ToList();

            return steps.ToString();
        }
    }
}
