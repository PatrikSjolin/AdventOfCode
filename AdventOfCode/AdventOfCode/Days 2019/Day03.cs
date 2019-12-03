using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Days_2019
{
    public class Day03 : IPuzzle
    {
        public bool Active => true;

        public string RunOne()
        {
            List<string> startInput = System.IO.File.ReadAllLines(@"..\..\Data\2019\input03.txt").ToList();
            List<string> wire1 = startInput[0].Split(',').ToList();
            List<string> wire2 = startInput[1].Split(',').ToList();

            HashSet<Point> points = new HashSet<Point>();
            
            int x = 0;
            int y = 0;

            foreach(var v in wire1)
            {
                string direction = v.First().ToString();
                int moves = int.Parse(v.Substring(1, v.Length - 1));

                if(direction == "R")
                {
                    for(int i = 0; i < moves; i++)
                    {
                        x++;
                        points.Add(new Point(x, y));
                    }
                }
                if (direction == "U")
                {
                    for (int i = 0; i < moves; i++)
                    {
                        y++;
                        points.Add(new Point(x, y));
                    }
                }
                if (direction == "L")
                {
                    for (int i = 0; i < moves; i++)
                    {
                        x--;
                        points.Add(new Point(x, y));
                    }
                }
                if (direction == "D")
                {
                    for (int i = 0; i < moves; i++)
                    {
                        y--;
                        points.Add(new Point(x, y));
                    }
                }
            }

            x = 0;
            y = 0;

            int distance = int.MaxValue;

            foreach (var v in wire2)
            {
                string direction = v.First().ToString();
                int moves = int.Parse(v.Substring(1, v.Length - 1));

                if (direction == "R")
                {
                    for (int i = 0; i < moves; i++)
                    {
                        x++;
                        if(points.Contains(new Point(x, y)))
                        {
                            int manhattan = Math.Abs(x) + Math.Abs(y);
                            if (manhattan < distance)
                                distance = manhattan;
                        }
                    }
                }
                if (direction == "U")
                {
                    for (int i = 0; i < moves; i++)
                    {
                        y++;
                        if (points.Contains(new Point(x, y)))
                        {
                            int manhattan = Math.Abs(x) + Math.Abs(y);
                            if (manhattan < distance)
                                distance = manhattan;
                        }
                    }
                }
                if (direction == "L")
                {
                    for (int i = 0; i < moves; i++)
                    {
                        x--;
                        if (points.Contains(new Point(x, y)))
                        {
                            int manhattan = Math.Abs(x) + Math.Abs(y);
                            if (manhattan < distance)
                                distance = manhattan;
                        }
                    }
                }
                if (direction == "D")
                {
                    for (int i = 0; i < moves; i++)
                    {
                        y--;
                        if (points.Contains(new Point(x, y)))
                        {
                            int manhattan = Math.Abs(x) + Math.Abs(y);
                            if (manhattan < distance)
                                distance = manhattan;
                        }
                    }
                }


            }

            return distance.ToString();
        }

        public string RunTwo()
        {
            List<string> startInput = System.IO.File.ReadAllLines(@"..\..\Data\2019\input03.txt").ToList();
            List<string> wire1 = startInput[0].Split(',').ToList();
            List<string> wire2 = startInput[1].Split(',').ToList();

            HashSet<Point> points = new HashSet<Point>();
            Dictionary<Point, int> pointSteps = new Dictionary<Point, int>();

            int x = 0;
            int y = 0;
            int steps = 0;

            foreach (var v in wire1)
            {
                string direction = v.First().ToString();
                int moves = int.Parse(v.Substring(1, v.Length - 1));

                if (direction == "R")
                {
                    for (int i = 0; i < moves; i++)
                    {
                        steps++;
                        x++;
                        Point p;
                        points.Add(p = new Point(x, y));
                        if(!pointSteps.ContainsKey(p))
                            pointSteps.Add(p, steps);
                    }
                }
                if (direction == "U")
                {
                    for (int i = 0; i < moves; i++)
                    {
                        steps++;
                        y++;
                        Point p;
                        points.Add(p = new Point(x, y));
                        if (!pointSteps.ContainsKey(p))
                            pointSteps.Add(p, steps);
                    }
                }
                if (direction == "L")
                {
                    for (int i = 0; i < moves; i++)
                    {
                        steps++;
                        x--;
                        Point p;
                        points.Add(p = new Point(x, y));
                        if (!pointSteps.ContainsKey(p))
                            pointSteps.Add(p, steps);
                    }
                }
                if (direction == "D")
                {
                    for (int i = 0; i < moves; i++)
                    {
                        steps++;
                        y--;
                        Point p;
                        points.Add(p = new Point(x, y));
                        if (!pointSteps.ContainsKey(p))
                            pointSteps.Add(p, steps);
                    }
                }
            }

            x = 0;
            y = 0;
            steps = 0;

            int distance = int.MaxValue;

            foreach (var v in wire2)
            {
                string direction = v.First().ToString();
                int moves = int.Parse(v.Substring(1, v.Length - 1));

                if (direction == "R")
                {
                    for (int i = 0; i < moves; i++)
                    {
                        steps++;
                        x++;
                        if (points.Contains(new Point(x, y)))
                        {
                            int manhattan = steps + pointSteps[new Point(x, y)];
                            if (manhattan < distance)
                                distance = manhattan;
                        }
                    }
                }
                if (direction == "U")
                {
                    for (int i = 0; i < moves; i++)
                    {
                        steps++;
                        y++;
                        if (points.Contains(new Point(x, y)))
                        {
                            int manhattan = steps + pointSteps[new Point(x, y)];
                            if (manhattan < distance)
                                distance = manhattan;
                        }
                    }
                }
                if (direction == "L")
                {
                    for (int i = 0; i < moves; i++)
                    {
                        steps++;
                        x--;
                        if (points.Contains(new Point(x, y)))
                        {
                            int manhattan = steps + pointSteps[new Point(x, y)];
                            if (manhattan < distance)
                                distance = manhattan;
                        }
                    }
                }
                if (direction == "D")
                {
                    for (int i = 0; i < moves; i++)
                    {
                        steps++;
                        y--;
                        if (points.Contains(new Point(x, y)))
                        {
                            int manhattan = steps + pointSteps[new Point(x, y)];
                            if (manhattan < distance)
                                distance = manhattan;
                        }
                    }
                }


            }

            return distance.ToString();
        }
    }
}
