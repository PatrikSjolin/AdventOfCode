using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Days_2020
{
    public class Day12 : IPuzzle
    {
        public bool Active => true;

        private List<(char, int)> inputs;

        public string RunOne()
        {
            inputs = System.IO.File.ReadAllLines(@"..\..\Data\2020\input12.txt").Select(x => (x[0], int.Parse(x.Substring(1)))).ToList();

            List<Point> directions = new List<Point> { new Point(1, 0), new Point(0, -1), new Point(-1, 0), new Point(0, 1) };
            int currentDirection = 0;
            Point position = new Point(0, 0);

            for(int i = 0; i < inputs.Count; i++)
            {
                char action = inputs[i].Item1;
                int steps = inputs[i].Item2;

                if(action == 'N')
                {
                    position.Y += steps;
                }
                else if (action == 'E')
                {
                    position.X += steps;
                }
                else if (action == 'S')
                {
                    position.Y -= steps;
                }
                else if (action == 'W')
                {
                    position.X -= steps;
                }
                else if (action == 'F')
                {
                    position.X += directions[currentDirection].X * steps;
                    position.Y += directions[currentDirection].Y * steps;
                }
                else if (action == 'R')
                {
                    int turns = steps / 90;
                    currentDirection += turns;
                    currentDirection = currentDirection % 4;
                }
                else if (action == 'L')
                {
                    int turns = steps / 90;
                    currentDirection -= turns;
                    currentDirection = Utilities.ModuloWithNegativeNumbersForIndex(currentDirection, 4);
                }
            }
            return Utilities.GetManhattanDistance(position.X, position.Y).ToString();
        }

        public string RunTwo()
        {
            Point shipPosition = new Point(0, 0);
            Point waypointPosition = new Point(10, 1);

            for (int i = 0; i < inputs.Count; i++)
            {
                char action = inputs[i].Item1;
                int steps = inputs[i].Item2;

                if (action == 'N')
                {
                    waypointPosition.Y += steps;
                }
                else if (action == 'E')
                {
                    waypointPosition.X += steps;
                }
                else if (action == 'S')
                {
                    waypointPosition.Y -= steps;
                }
                else if (action == 'W')
                {
                    waypointPosition.X -= steps;
                }
                else if (action == 'F')
                {
                    shipPosition.X += waypointPosition.X * steps;
                    shipPosition.Y += waypointPosition.Y * steps;
                }
                else if (action == 'R')
                {
                    int turns = steps / 90;
                    waypointPosition.Rotate90DegreesCWAroundOrigin(turns);
                }
                else if (action == 'L')
                {
                    int turns = steps / 90;
                    waypointPosition.Rotate90DegreesCCWAroundOrigin(turns);
                }
            }
            return Utilities.GetManhattanDistance(shipPosition.X, shipPosition.Y).ToString();
        }
    }
}
