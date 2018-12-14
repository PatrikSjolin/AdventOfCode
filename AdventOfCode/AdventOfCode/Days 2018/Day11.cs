using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Days_2018
{
    public class Day11 : IPuzzle
    {
        int GetGridPowerLevel(int x, int y)
        {
            int rackId = x + 10;
            int powerLevel = rackId * y;
            powerLevel += puzzleInput;
            powerLevel = powerLevel * rackId;
            if (powerLevel < 100)
                return -5;
            else
            {
                int hundredDigit = (powerLevel / 100) % 10;
                return hundredDigit - 5;
            }
        }

        int puzzleInput = 5468;
        int size = 300;

        public string RunOne()
        {
            int size = 300;
            int[,] grid = new int[size, size];

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    grid[i, j] = GetGridPowerLevel(i, j);
                }
            }

            int maxRegion = int.MinValue;

            Point p = new Point(0, 0);

            for (int i = 0; i < size-(3+1); i++)
            {
                for(int j = 0; j < size-(3+1); j++)
                {
                    int sum = 0;

                    for(int k = i; k < i+3; k++)
                    {
                        for(int l = j; l < j+3; l++)
                        {
                            sum += grid[k, l];
                        }
                    }

                    if(sum > maxRegion)
                    {
                        maxRegion = sum;
                        p.X = i;
                        p.Y = j;
                    }
                }
            }

            return p.X + "," + p.Y;
        }

        public string RunTwo()
        {
            int[,] grid = new int[size, size];

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    grid[i, j] = GetGridPowerLevel(i, j);
                }
            }

            int maxRegion = int.MinValue;
            int optSize = 0;
            Point p = new Point(0, 0);
            Parallel.For(1, size, a =>
            {
                for (int i = 0; i < size - (a + 1); i++)
                {
                    for (int j = 0; j < size - (a + 1); j++)
                    {
                        int sum = 0;

                        for (int k = i; k < i + a; k++)
                        {
                            for (int l = j; l < j + a; l++)
                            {
                                sum += grid[k, l];
                            }
                        }

                        if (sum > maxRegion)
                        {
                            maxRegion = sum;
                            p.X = i;
                            p.Y = j;
                            optSize = a;
                        }
                    }
                }
                //break;
            });

            return p.X + "," + p.Y + "," + optSize;
        }
    }
}
