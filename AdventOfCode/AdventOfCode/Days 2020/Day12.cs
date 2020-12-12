using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Days_2020
{
    public class Day12 : IPuzzle
    {
        public bool Active => true;

        private List<string> inputs;

        public string RunOne()
        {
            inputs = System.IO.File.ReadAllLines(@"..\..\Data\2020\input12.txt").ToList();

            List<Point> directions = new List<Point> { new Point(1, 0), new Point(0, -1), new Point(-1, 0), new Point(0, 1) };
            int currentDirection = 0;
            Point position = new Point(0, 0);

            for(int i = 0; i < inputs.Count; i++)
            {
                string action = inputs[i][0].ToString();
                int steps = int.Parse(inputs[i].Substring(1, inputs[i].Length - 1));

                if(action == "N")
                {
                    position.Y += steps;
                }
                if (action == "E")
                {
                    position.X += steps;
                }
                if (action == "S")
                {
                    position.Y -= steps;
                }
                if (action == "W")
                {
                    position.X -= steps;
                }
                if (action == "F")
                {
                    position.X += directions[currentDirection].X * steps;
                    position.Y += directions[currentDirection].Y * steps;
                }
                if (action == "R")
                {
                    int turns = steps / 90;
                    currentDirection += turns;
                    currentDirection = currentDirection % 4;
                }
                if (action == "L")
                {
                    int turns = steps / 90;
                    currentDirection -= turns;
                    currentDirection = ((currentDirection % 4) + 4) % 4;
                }
            }
            return (Math.Abs(position.X) + Math.Abs(position.Y)).ToString();
        }

        public string RunTwo()
        {
            Point shipPosition = new Point(0, 0);
            Point waypointPosition = new Point(10, 1);

            for (int i = 0; i < inputs.Count; i++)
            {
                string action = inputs[i][0].ToString();
                int steps = int.Parse(inputs[i].Substring(1, inputs[i].Length - 1));

                if (action == "N")
                {
                    waypointPosition.Y += steps;
                }
                if (action == "E")
                {
                    waypointPosition.X += steps;
                }
                if (action == "S")
                {
                    waypointPosition.Y -= steps;
                }
                if (action == "W")
                {
                    waypointPosition.X -= steps;
                }
                if (action == "F")
                {
                    shipPosition.X += waypointPosition.X * steps;
                    shipPosition.Y += waypointPosition.Y * steps;
                }
                if (action == "R")
                {
                    int turns = steps / 90;
                    for (int j = 0; j < turns; j++)
                    {
                        int oldX = waypointPosition.X;
                        waypointPosition.X = waypointPosition.Y;
                        waypointPosition.Y = oldX * -1;
                    }
                }
                if (action == "L")
                {
                    int turns = steps / 90;
                    for (int j = 0; j < turns; j++)
                    {
                        int oldX = waypointPosition.X;
                        waypointPosition.X = waypointPosition.Y * -1;
                        waypointPosition.Y = oldX;
                    }
                }
            }
            return (Math.Abs(shipPosition.X) + Math.Abs(shipPosition.Y)).ToString();
        }
    }
}
