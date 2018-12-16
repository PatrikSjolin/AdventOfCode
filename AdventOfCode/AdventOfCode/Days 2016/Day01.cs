using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Days_2016
{
    public class Day01 : IPuzzle
    {
        public bool Active => true;

        public List<string> ReadInput(string path)
        {
            string text = System.IO.File.ReadAllText(path);

            List<string> inputList = text.Split(',').ToList();
            for (int i = 0; i < inputList.Count; i++)
            {
                inputList[i] = inputList[i].TrimStart();
            }

            return inputList;
        }

        static List<Tuple<int, int>> GetVisitedPoints(List<string> inputList)
        {
            List<Tuple<int, int>> pointsVisited = new List<Tuple<int, int>>();

            int currentX = 0;
            int currentY = 0;

            pointsVisited.Add(new Tuple<int, int>(currentX, currentY));

            //north, east, south, west
            Tuple<int, int>[] directions = { new Tuple<int, int>(0, 1), new Tuple<int, int>(1, 0), new Tuple<int, int>(0, -1), new Tuple<int, int>(-1, 0) };

            //north
            int currentDirection = 400;

            List<Tuple<int, int>> visitedTwice = new List<Tuple<int, int>>();

            foreach (string s in inputList)
            {
                int length = int.Parse(s.Substring(1));

                if (s.StartsWith("R"))
                {
                    currentDirection = currentDirection + 1;
                }
                else
                {
                    currentDirection = currentDirection - 1;
                }

                for (int i = 0; i < length; i++)
                {
                    currentX += directions[currentDirection % 4].Item1;
                    currentY += directions[currentDirection % 4].Item2;

                    pointsVisited.Add(new Tuple<int, int>(currentX, currentY));
                }
            }

            return pointsVisited;
        }

        static int CountBlocksAway(Tuple<int, int> coordinate)
        {
            return Math.Abs(coordinate.Item1) + Math.Abs(coordinate.Item2);
        }

        static Tuple<int, int> GetFirstDuplicate(List<Tuple<int, int>> inputList)
        {
            List<Tuple<int, int>> visitedTwice = new List<Tuple<int, int>>();

            foreach (var point in inputList)
            {
                if (visitedTwice.Contains(point))
                {
                    return point;
                }
                visitedTwice.Add(point);
            }

            return null;
        }

        public void GoOne()
        {
        }

        public void GoTwo()
        {
        }

        public string RunOne()
        {
            List<string> inputList = ReadInput(@"..\..\Data\2016\input01.txt").ToList();
            List<Tuple<int, int>> pointsVisited = GetVisitedPoints(inputList);
            int distance = CountBlocksAway(pointsVisited.Last());
            return distance.ToString();
        }

        public string RunTwo()
        {
            List<string> inputList = ReadInput(@"..\..\Data\2016\input01.txt").ToList();
            List<Tuple<int, int>> pointsVisited = GetVisitedPoints(inputList);
            Tuple<int, int> firstVisitedPoint = GetFirstDuplicate(pointsVisited);
            int distance = CountBlocksAway(firstVisitedPoint);
            return distance.ToString(); ;
        }
    }
}
