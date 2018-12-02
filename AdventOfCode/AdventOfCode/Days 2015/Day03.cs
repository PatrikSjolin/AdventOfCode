using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Days_2015
{
    public class Day03 : IPuzzle
    {
        public string RunOne()
        {
            List<string> inputLines = System.IO.File.ReadAllLines(@"..\..\Data\2015\input03.txt").ToList();
            List<Point> points = new List<Point>();

            Dictionary<Point, int> houses = new Dictionary<Point, int>();
            houses.Add(new Point(0, 0), 1);

            Point current = new Point(0, 0);
            //points.Add(current);
            foreach (char c in inputLines[0])
            {
                if(c == '<')
                {
                    current.X--;
                }
                if(c == '>')
                {
                    current.X++;
                }
                if(c == '^')
                {
                    current.Y++;
                }
                if(c == 'v')
                {
                    current.Y--;
                }
                int val = 0;
                if (houses.TryGetValue(current, out val))
                {
                    houses[current]++;
                }
                else
                {
                    houses.Add(new Point(current.X, current.Y), 1);
                }
            }
            return houses.Count.ToString();
        }

        public string RunTwo()
        {
            List<string> inputLines = System.IO.File.ReadAllLines(@"..\..\Data\2015\input03.txt").ToList();
            //List<string> inputLines = new List<string> { "^>v<" };
            List<Point> points = new List<Point>();

            Dictionary<Point, int> houses = new Dictionary<Point, int>();
            houses.Add(new Point(0, 0), 1);

            Point current = new Point(0, 0);
            //points.Add(current);
            for (int i = 0; i < inputLines[0].Length; i += 2)
            {
                if (inputLines[0][i] == '<')
                {
                    current.X--;
                }
                if (inputLines[0][i] == '>')
                {
                    current.X++;
                }
                if (inputLines[0][i] == '^')
                {
                    current.Y++;
                }
                if (inputLines[0][i] == 'v')
                {
                    current.Y--;
                }
                int val = 0;
                if (houses.TryGetValue(current, out val))
                {
                    houses[current]++;
                }
                else
                {
                    houses.Add(new Point(current.X, current.Y), 1);
                }
            }
            current = new Point(0, 0);
            for (int i = 1; i < inputLines[0].Length; i += 2)
            {
                if (inputLines[0][i] == '<')
                {
                    current.X--;
                }
                if (inputLines[0][i] == '>')
                {
                    current.X++;
                }
                if (inputLines[0][i] == '^')
                {
                    current.Y++;
                }
                if (inputLines[0][i] == 'v')
                {
                    current.Y--;
                }
                int val = 0;
                if (houses.TryGetValue(current, out val))
                {
                    houses[current]++;
                }
                else
                {
                    houses.Add(new Point(current.X, current.Y), 1);
                }
            }
            return houses.Count.ToString();
        }
    }
}
