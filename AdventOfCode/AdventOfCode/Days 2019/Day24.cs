using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Days_2019
{
    public class Day24 : IPuzzle
    {
        public bool Active => true;

        public string RunOne()
        {
            List<string> inputs = System.IO.File.ReadAllLines(@"..\..\Data\2019\input24.txt").ToList();

            int size = 5;

            string[,] grid = new string[size, size];

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    grid[i, j] = inputs[j][i].ToString();
                }
            }

            HashSet<string> saved = new HashSet<string>();
            string gridString = TransformGrid(grid, size);

            saved.Add(gridString);
            //PrintGrid(grid, size);

            while (true)
            {
                grid = UpdateGrid(grid, size);

                gridString = TransformGrid(grid, size);

                if (saved.Contains(gridString))
                {
                    int sum = 0;
                    for (int i = 0; i < size; i++)
                    {
                        for (int j = 0; j < size; j++)
                        {
                            if (grid[j, i] == "#")
                            {
                                sum += (int)Math.Pow(2, i * size + j);
                            }
                        }
                    }
                    return sum.ToString();
                }
                saved.Add(gridString);
                //PrintGrid(grid, size);
            }

            return "";
        }

        private string TransformGrid(string[,] grid, int size)
        {
            string s = "";
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    s += grid[i, j];
                }
            }
            return s;
        }

        private string[,] UpdateGrid(string[,] grid, int size)
        {
            string[,] tmp = new string[size, size];
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    tmp[i, j] = ".";
                    int bugs = CountBugs(grid, i, j, size);

                    if (grid[i, j] == ".")
                    {
                        if (bugs == 1 || bugs == 2)
                            tmp[i, j] = "#";
                    }
                    else
                    {
                        if (bugs != 1)
                        {
                            tmp[i, j] = ".";
                        }
                        else
                            tmp[i, j] = "#";
                    }
                }
            }
            return tmp;
        }

        private int CountBugs(string[,] grid, int i, int j, int size)
        {
            int bugs = 0;

            if (i + 1 < size && grid[i + 1, j] == "#")
                bugs++;
            if (i - 1 >= 0 && grid[i - 1, j] == "#")
                bugs++;
            if (j + 1 < size && grid[i, j + 1] == "#")
                bugs++;
            if (j - 1 >= 0 && grid[i, j - 1] == "#")
                bugs++;
            return bugs;
        }

        private void PrintGrid(string[,] grid, int size)
        {
            Console.WriteLine();
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    Console.Write(grid[j, i]);
                }
                Console.WriteLine();
            }
        }

        Dictionary<int, string[,]> levels = new Dictionary<int, string[,]>();

        public string RunTwo()
        {
            List<string> inputs = System.IO.File.ReadAllLines(@"..\..\Data\2019\input24.txt").ToList();

            int size = 5;

            string[,] grid = new string[size, size];

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    grid[i, j] = inputs[j][i].ToString();
                }
            }

            levels.Add(0, grid);

            for (int i = 0; i < 200; i++)
            {
                levels = PropagateBugs(levels, size);

                int innerMostLevel = levels.Keys.Min();
                int outerMostLevel = levels.Keys.Max();

                var innerMost = levels[innerMostLevel];
                var outerMost = levels[outerMostLevel];

                int buggs = 0;
                for (int j = 0; j < size; j++)
                {
                    for (int k = 0; k < size; k++)
                    {
                        if (innerMost[k, j] == "#")
                            buggs++;
                    }
                }
                if (buggs == 0)
                    levels.Remove(innerMostLevel);

                buggs = 0;
                for (int j = 0; j < size; j++)
                {
                    for (int k = 0; k < size; k++)
                    {
                        if (outerMost[k, j] == "#")
                            buggs++;
                    }
                }
                if (buggs == 0)
                    levels.Remove(outerMostLevel);
            }
            int mostInnerLevel = levels.Keys.Min();
            int mostOuterLevel = levels.Keys.Max();


            for (int j = mostOuterLevel; j >= mostInnerLevel; j--)
            {
                //Console.Write("Level: " + j * -1);
                //PrintGrid(levels[j], size);
                //Console.WriteLine();
            }

            int bugs = 0;

            foreach (var level in levels)
            {
                for (int i = 0; i < size; i++)
                {
                    for (int j = 0; j < size; j++)
                    {
                        if (level.Value[j, i] == "#")
                        {
                            bugs++;
                        }
                    }
                }
            }

            return bugs.ToString();
        }

        private Dictionary<int, string[,]> PropagateBugs(Dictionary<int, string[,]> levels, int size)
        {
            //-1 = deepest. The outer of -1 = the first level 0.
            int mostInnerLevel = levels.Keys.Min() - 1;
            int mostOuterLevel = levels.Keys.Max() + 1;

            levels.Add(mostInnerLevel, new string[size,size]);
            levels.Add(mostOuterLevel, new string[size,size]);

            Dictionary<int, string[,]> newLevels = new Dictionary<int, string[,]>();

            for (int k = mostInnerLevel; k <= mostOuterLevel; k++)
            {
                string[,] newGrid = new string[size, size];

                for (int i = 0; i < size; i++)
                {
                    for (int j = 0; j < size; j++)
                    {
                        if (i == 2 && j == 2)
                        {
                            newGrid[i, j] = "?";
                            continue;
                        }
                        newGrid[i, j] = ".";
                        if (levels[k][i, j] == null)
                            levels[k][i, j] = ".";

                        string[,] innerLevel = null;
                        if (levels.ContainsKey(k - 1))
                            innerLevel = levels[k - 1];

                        string[,] outerLevel = null;
                        if (levels.ContainsKey(k + 1))
                            outerLevel = levels[k + 1];

                        int bugs = CountBugs(levels[k], i, j, size, innerLevel, outerLevel);
                        if (levels[k][i, j] == ".")
                        {
                            if (bugs == 1 || bugs == 2)
                                newGrid[i, j] = "#";
                        }
                        else
                        {
                            if (bugs != 1)
                            {
                                newGrid[i, j] = ".";
                            }
                            else
                                newGrid[i, j] = "#";
                        }
                    }
                }
                newLevels.Add(k, newGrid);
            }

            return newLevels;
        }

        private int CountBugs(string[,] currentLevel, int i, int j, int size, string[,] innerLevel, string[,] outerLevel)
        {
            int bugs = 0;

            List<Tuple<int, int>> list = new List<Tuple<int, int>>
            {
                new Tuple<int, int>(i - 1, j),
                new Tuple<int, int>(i + 1, j),
                new Tuple<int, int>(i, j - 1),
                new Tuple<int, int>(i, j + 1)
            };

            for(int k = 0; k < list.Count; k++)
            {
                if(list[k].Item1 == 2 && list[k].Item2 == 2)
                {
                    //Discover middle
                    if(innerLevel != null)
                    {
                        if(i == 2 && j == 1)
                        {
                            if (innerLevel[0, 0] == "#")
                                bugs++;
                            if (innerLevel[1, 0] == "#")
                                bugs++;
                            if (innerLevel[2, 0] == "#")
                                bugs++;
                            if (innerLevel[3, 0] == "#")
                                bugs++;
                            if (innerLevel[4, 0] == "#")
                                bugs++;
                        }
                        if (i == 1 && j == 2)
                        {
                            if (innerLevel[0, 0] == "#")
                                bugs++;
                            if (innerLevel[0, 1] == "#")
                                bugs++;
                            if (innerLevel[0, 2] == "#")
                                bugs++;
                            if (innerLevel[0, 3] == "#")
                                bugs++;
                            if (innerLevel[0, 4] == "#")
                                bugs++;
                        }
                        if (i == 2 && j == 3)
                        {
                            if (innerLevel[0, 4] == "#")
                                bugs++;
                            if (innerLevel[1, 4] == "#")
                                bugs++;
                            if (innerLevel[2, 4] == "#")
                                bugs++;
                            if (innerLevel[3, 4] == "#")
                                bugs++;
                            if (innerLevel[4, 4] == "#")
                                bugs++;
                        }
                        if (i == 3 && j == 2)
                        {
                            if (innerLevel[4, 0] == "#")
                                bugs++;
                            if (innerLevel[4, 1] == "#")
                                bugs++;
                            if (innerLevel[4, 2] == "#")
                                bugs++;
                            if (innerLevel[4, 3] == "#")
                                bugs++;
                            if (innerLevel[4, 4] == "#")
                                bugs++;
                        }
                    }
                }
                else if(list[k].Item1 < 0)
                {
                    //Check outer left
                    if (outerLevel != null)
                    {
                        if (outerLevel[1, 2] == "#")
                            bugs++;
                    }
                }
                else if(list[k].Item1 >= size)
                {
                    //Check outer right
                    if(outerLevel != null)
                    {
                        if (outerLevel[3, 2] == "#")
                            bugs++;
                    }
                }
                else if(list[k].Item2 < 0)
                {
                    //Check outer upper
                    if(outerLevel != null)
                    {
                        if (outerLevel[2, 1] == "#")
                            bugs++;
                    }
                }
                else if(list[k].Item2 >= size)
                {
                    //Check outer under
                    if(outerLevel != null)
                    {
                        if (outerLevel[2, 3] == "#")
                            bugs++;
                    }
                }
                else
                {
                    if (currentLevel[list[k].Item1, list[k].Item2] == "#")
                        bugs++;
                }
            }

            return bugs;
        }
    }
}
