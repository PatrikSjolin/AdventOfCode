using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Days_2018
{
    public class Day17 : IPuzzle
    {
        public bool Active => true;

        List<string> inputLines = System.IO.File.ReadAllLines(@"..\..\Data\2018\input17.txt").ToList();
        string[,] map;
        bool paint = false;
        int smallestY = int.MaxValue;
        int biggestY = int.MinValue;
        int biggestX = int.MinValue;


        public string RunOne()
        {
            if(paint)
                Console.SetWindowSize(319, 83);
            int smallestX = int.MaxValue;

            foreach (var line in inputLines)
            {
                List<string> split = line.Split(',').ToList();

                int y = int.Parse(split[0].Split('=')[1]);

                string xCord = split[1].Substring(3, split[1].Length - 3);
                xCord = xCord.Replace("..", ",");

                int x1 = int.Parse(xCord.Split(',')[0]);
                int x2 = int.Parse(xCord.Split(',')[1]);

                if (line.StartsWith("x"))
                {
                    if (x2 > biggestY)
                        biggestY = x2;
                    if (x1 < smallestY)
                        smallestY = x1;
                    if (y > biggestX)
                        biggestX = y;

                }
                if (line.StartsWith("y"))
                {
                    if (x2 > biggestX)
                        biggestX = x2;
                    if (x1 < smallestX)
                        smallestX = x1;
                    if (y > biggestY)
                        biggestY = y;
                }
            }

            biggestY++;
            biggestX++;
            smallestX--;
            biggestX = biggestX - smallestX;
            map = new string[biggestX, biggestY];

            for (int i = 0; i < biggestX; i++)
            {
                for (int j = 0; j < biggestY; j++)
                {
                    map[i, j] = ".";
                }
            }

            map[500 - smallestX, 0] = "+";

            foreach (var line in inputLines)
            {
                List<string> split = line.Split(',').ToList();

                int y = int.Parse(split[0].Split('=')[1]);

                string xCord = split[1].Substring(3, split[1].Length - 3);
                xCord = xCord.Replace("..", ",");

                int x1 = int.Parse(xCord.Split(',')[0]);
                int x2 = int.Parse(xCord.Split(',')[1]);

                if (line.StartsWith("x"))
                {
                    for (int i = x1; i <= x2; i++)
                    {
                        map[y - smallestX, i] = "#";
                    }
                }
                if (line.StartsWith("y"))
                {
                    for (int i = x1; i <= x2; i++)
                    {
                        map[i - smallestX, y] = "#";
                    }
                }
            }

            if (paint)
            {
                PaintMap();
            }

            map[500 - smallestX, 1] = "|";
            int waterCount = 0;
            int runningWaterCount = 0;
            
            while (true)
            {
                int newWater = CountWater(map, biggestX, biggestY);
                int newRunningWaterCount = CountRunningWater(map, biggestX, biggestY);
                if (newWater > 5 && newWater == waterCount && newRunningWaterCount == runningWaterCount)
                {
                    if (paint)
                    {
                        PaintMap();
                    }

                    return (runningWaterCount + waterCount).ToString();
                }

                waterCount = newWater;
                runningWaterCount = newRunningWaterCount;
                SpreadWater(map, biggestX, biggestY);

                if (paint)
                {
                    PaintMap();
                }
            }
        }

        private void PaintMap()
        {
            Console.WriteLine();
            for (int i = 0; i < biggestY; i++)
            {
                for (int j = 0; j < biggestX; j++)
                {
                    Console.Write(map[j, i]);
                }
                Console.WriteLine();
            }
            Console.ReadKey();
            Console.WriteLine();
        }

        private int CountWater(string[,] map, int sizeX, int sizeY)
        {
            int waterCount = 0;
            for (int i = smallestY; i < sizeY; i++)
            {
                for (int j = 0; j < sizeX; j++)
                {
                    if (map[j, i] == "~")
                        waterCount++;
                }
            }
            return waterCount;
        }

        private int CountRunningWater(string[,] map, int sizeX, int sizeY)
        {
            int waterCount = 0;
            for (int i = smallestY; i < sizeY; i++)
            {
                for (int j = 0; j < sizeX; j++)
                {
                    if (map[j, i] == "|")
                        waterCount++;
                }
            }
            return waterCount;
        }

        private void SpreadWater(string[,] map, int sizeX, int sizeY)
        {
            for (int i = 0; i < sizeY; i++)
            {
                for (int j = 0; j < sizeX; j++)
                {
                    string current = map[j, i];

                    string down = "X";
                    if (i + 1 < sizeY)
                        down = map[j, i + 1];

                    string left = "X";
                    if (j - 1 >= 0)
                        left = map[j - 1, i];

                    string right = "X";
                    if (j + 1 < sizeX)
                        right = map[j + 1, i];

                    if (current == "|")
                    {
                        if (down == "#" || down == "~")
                        {
                            if (down == "~" && IsOverFlowing(map, j, i + 1))
                                continue;
                            for (int k = j - 1; map[k, i] != "#"; k--)
                            {
                                if (map[k, i + 1] == "." || map[k, i + 1] == "|")
                                {
                                    map[k, i] = "|";
                                    break;
                                }
                                map[k, i] = "~";
                            }
                            for (int k = j + 1; map[k, i] != "#"; k++)
                            {
                                if (map[k, i + 1] == "." || map[k, i + 1] == "|")
                                {
                                    map[k, i] = "|";
                                    break;
                                }
                                map[k, i] = "~";
                            }
                            map[j, i] = "~";
                        }
                        else if (down == ".")
                        {
                            map[j, i + 1] = "|";
                        }
                    }
                }
            }
        }

        private bool IsOverFlowing(string[,] map, int x, int y)
        {
            bool overFlowing = false;

            for (int i = x; ; i++)
            {
                if (map[i, y] == "#")
                    break;
                if (map[i, y] == "|")
                    return true;
            }

            for (int i = x; ; i--)
            {
                if (map[i, y] == "#")
                    break;
                if (map[i, y] == "|")
                    return true;
            }

            return overFlowing;
        }

        public string RunTwo()
        {
            for (int i = 0; i < biggestX; i++)
            {
                for (int j = 0; j < biggestY; j++)
                {
                    if (map[i, j] == "~" && IsOverFlowing(map, i, j))
                        map[i, j] = "|";
                }
            }

            return CountWater(map, biggestX, biggestY).ToString();
        }
    }
}
