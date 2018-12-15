using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Days_2018
{
    public class Day06 : IPuzzle
    {
        public bool Active { get => true; }

        List<Point> coordinates;
        int size = 0;

        public string RunOne()
        {
            List<string> input = System.IO.File.ReadAllLines(@"..\..\Data\2018\input06.txt").ToList();

            coordinates = new List<Point>();

            foreach (var s in input)
            {
                string ss = s.Trim();
                int x = int.Parse(ss.Split(',')[0]);
                int y = int.Parse(ss.Split(',')[1]);
                coordinates.Add(new Point(x, y));

                if (x > size)
                    size = x;
                if (y > size)
                    size = y;
            }

            int[,] grid = new int[size, size];

            for(int i = 0; i < size; i++)
            {
                for(int j = 0; j < size; j++)
                {
                    int index = GetDistance(i, j, coordinates);
                    grid[i, j] = index;
                }
            }

            int maxArea = 0;

            Dictionary<int, bool> realArea = new Dictionary<int, bool>();

            Dictionary<int, int> occ = new Dictionary<int, int>();

            for(int i = 0; i < size; i++)
            {
                for(int j = 0; j < size; j++)
                {
                    if (!occ.ContainsKey(grid[i, j]))
                        occ.Add(grid[i, j], 0);
                    occ[grid[i,  j]]++;

                    if(i == 0 || j == 0 || i == size - 1 || j == size - 1)
                    {
                        occ[grid[i, j]] = 0;
                    }
                }
            }

            foreach(var o in occ)
            {
                if (o.Value > maxArea)
                    maxArea = o.Value;
            }

            return maxArea.ToString();
        }

        private int GetDistance(int x, int y, List<Point> coordinates)
        {
            int closest = 9999999;
            int index = -1;

            bool real = true;

            for(int i = 0; i < coordinates.Count; i++)
            {
                int x1 = Math.Abs(coordinates[i].X - x);
                int y1 = Math.Abs(coordinates[i].Y - y);

                int d = x1 + y1;

                if (d <= closest)
                {
                    if (d == closest)
                        real = false;
                    else
                        real = true;
                    closest = d;
                    index = i;
                }
            }

            if (!real)
                return -1;

            return index;
        }

        private int GetSummedUpDistance(int x, int y, List<Point> coordinates)
        {
            int sum = 0;

            for (int i = 0; i < coordinates.Count; i++)
            {
                int x1 = Math.Abs(coordinates[i].X - x);
                int y1 = Math.Abs(coordinates[i].Y - y);

                int d = x1 + y1;

                sum += d;
            }

            return sum;
        }

        public string RunTwo()
        {
            int regionSize = 0;

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    int index = GetSummedUpDistance(i, j, coordinates);
                    if(index < 10000)
                    {
                        regionSize++;
                    }
                }
            }

            return regionSize.ToString();
        }
    }
}
