using System.Collections.Generic;

namespace AdventOfCode.Days
{
    public class Day14 : IPuzzle
    {
        static HashSet<Point> usedNodes;
        Dictionary<char, string> hexCharacterToBinary = new Dictionary<char, string> {
                { '0', "0000" },
                { '1', "0001" },
                { '2', "0010" },
                { '3', "0011" },
                { '4', "0100" },
                { '5', "0101" },
                { '6', "0110" },
                { '7', "0111" },
                { '8', "1000" },
                { '9', "1001" },
                { 'a', "1010" },
                { 'b', "1011" },
                { 'c', "1100" },
                { 'd', "1101" },
                { 'e', "1110" },
                { 'f', "1111" }
            };

        int[,] grid;

        public string RunOne()
        {
            string input = "hxtvlmkl";

            int sum = 0;

            grid = new int[128, 128];

            for (int i = 0; i < 128; i++)
            {
                string hash = input + "-" + i;

                string knotHash = Day10.GetHashKnot(hash);

                for (int j = 0; j < knotHash.Length; j++)
                {
                    string binary = hexCharacterToBinary[knotHash[j]];

                    grid[i, j * 4] = (int)char.GetNumericValue(binary[0]);
                    grid[i, j * 4 + 1] = (int)char.GetNumericValue(binary[1]);
                    grid[i, j * 4 + 2] = (int)char.GetNumericValue(binary[2]);
                    grid[i, j * 4 + 3] = (int)char.GetNumericValue(binary[3]);

                    sum += (grid[i, j * 4] + grid[i, j * 4 + 1] + grid[i, j * 4 + 2] + grid[i, j * 4 + 3]);
                }
            }


            return sum.ToString();
        }

        public string RunTwo()
        {
            int regions = 0;
            usedNodes = new HashSet<Point>();

            for (int i = 0; i < 128; i++)
            {
                for (int j = 0; j < 128; j++)
                {
                    if (grid[i, j] == 1)
                    {
                        if (usedNodes.Contains(new Point(i, j)))
                        {
                            continue;
                        }
                        else
                        {
                            regions++;
                            usedNodes.Add(new Point(i, j));
                            GetAdjacentNodes(i, j, grid);
                        }
                    }
                    else
                    {
                        continue;
                    }
                }
            }

            return regions.ToString();
        }

        private void GetAdjacentNodes(int i, int j, int[,] grid)
        {
            Point test1 = new Point(i, j + 1);
            Point test2 = new Point(i + 1, j);
            Point test3 = new Point(i, j - 1);
            Point test4 = new Point(i - 1, j);

            List<Point> testNodes = new List<Point>();
            testNodes.Add(test1);
            testNodes.Add(test2);
            testNodes.Add(test3);
            testNodes.Add(test4);

            foreach (var p in new List<Point>(testNodes))
            {
                if (p.X < 0 || p.Y < 0 || p.X > 127 || p.Y > 127 || grid[p.X, p.Y] == 0 || usedNodes.Contains(p))
                    testNodes.Remove(p);
            }

            foreach (var p in testNodes)
            {
                usedNodes.Add(p);
            }

            foreach (var p in testNodes)
            {
                GetAdjacentNodes(p.X, p.Y, grid);
            }
        }
    }
}
