using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Days_2018
{
    public class Day10 : IPuzzle
    {
        int seconds = 1;
        public string RunOne()
        {
            List<string> inputLines = System.IO.File.ReadAllLines(@"..\..\Data\2018\input10.txt").ToList();
            List<KeyValuePair<Point, Point>> points = new List<KeyValuePair<Point, Point>>();

            foreach(var i in inputLines)
            {

                var split = i.Split('<');
                var position = split[1].Split('>')[0];
                position = position.Replace(" ", "");
                Point pos = new Point(int.Parse(position.Split(',')[0]), int.Parse(position.Split(',')[1]));

                var velo = split[2].Split('>')[0];
                velo = velo.Replace(" ", "");
                Point vel = new Point(int.Parse(velo.Split(',')[0]), int.Parse(velo.Split(',')[1]));

                points.Add(new KeyValuePair<Point, Point>(pos, vel));
            }

            int largestY = int.MinValue; ;
            int smallestY = int.MaxValue;
            int largestX = int.MinValue;
            int smallestX = int.MaxValue;

            int diff = int.MaxValue;

            for (seconds = 1; seconds < 1000000; seconds++)
            {

                largestY = int.MinValue; ;
                smallestY = int.MaxValue;
                largestX = int.MinValue;
                smallestX = int.MaxValue;

                foreach (var v in points)
                {
                    v.Key.X += v.Value.X;
                    v.Key.Y += v.Value.Y;
                }

                foreach(var v in points)
                {
                    if(v.Key.Y > largestY)
                    {
                        largestY = v.Key.Y;
                    }
                    if(v.Key.Y < smallestY)
                    {
                        smallestY = v.Key.Y;
                    }
                    if(v.Key.X > largestX)
                    {
                        largestX = v.Key.X;
                    }
                    if(v.Key.X < smallestX)
                    {
                        smallestX = v.Key.X;
                    }
                }

                //Run until size increases
                if (largestY - smallestY < diff)
                {
                    diff = largestY - smallestY;
                }
                else
                {
                    //Back one step
                    seconds--;
                    largestY = int.MinValue; ;
                    smallestY = int.MaxValue;
                    largestX = int.MinValue;
                    smallestX = int.MaxValue;

                    foreach (var v in points)
                    {
                        v.Key.X -= v.Value.X;
                        v.Key.Y -= v.Value.Y;
                    }
                    foreach (var v in points)
                    {
                        if (v.Key.Y > largestY)
                        {
                            largestY = v.Key.Y;
                        }
                        if (v.Key.Y < smallestY)
                        {
                            smallestY = v.Key.Y;
                        }
                        if (v.Key.X > largestX)
                        {
                            largestX = v.Key.X;
                        }
                        if (v.Key.X < smallestX)
                        {
                            smallestX = v.Key.X;
                        }
                    }
                    break;
                }
            }

            int size = largestX - smallestX+1;

            string[,] grid = new string[size, size];

            for(int i = 0; i < size; i++)
            {
                for(int j = 0; j < size; j++)
                {
                    grid[i, j] = ".";
                }
            }

            foreach(var v in points)
            {
                grid[v.Key.X - smallestX, v.Key.Y - smallestY] = "#";
            }
            //Console.WriteLine();

            for(int i = 0; i < size; i++)
            {
                for(int j = 0; j < size; j++)
                {
                    //Console.Write(grid[j, i]);
                }
                //Console.WriteLine();
            }

            return "ERCXLAJL";
        }

        public string RunTwo()
        {
            List<string> inputLines = System.IO.File.ReadAllLines(@"..\..\Data\2018\input10test.txt").ToList();
            return seconds.ToString();
        }
    }
}
