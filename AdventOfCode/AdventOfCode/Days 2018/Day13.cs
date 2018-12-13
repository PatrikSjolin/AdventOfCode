using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Days_2018
{
    public class Day13 : IPuzzle
    {
        public class Cart
        {
            public Point Direction { get; set; }

            public Point Position { get; set; }

            public Point LastTurn { get; set; }

            public bool Crashed { get; set; }
        }



        public string RunOne()
        {

            List<string> inputLines = System.IO.File.ReadAllLines(@"..\..\Data\2018\input13.txt").ToList();

            int width = inputLines[0].Length;
            int height = inputLines.Count;

            string[,] map = new string[inputLines[0].Length, inputLines.Count];
            List<Cart> carts = new List<Cart>();
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    string s = inputLines[j][i].ToString();
                    Cart c = null;
                    if (s == "^")
                    {
                        map[i, j] = "|";
                        carts.Add(c = new Cart { Direction = new Point(0, -1) });
                    }
                    else if (s == "<")
                    {
                        map[i, j] = "-";
                        carts.Add(c = new Cart { Direction = new Point(-1, 0) });
                    }
                    else if (s == ">")
                    {
                        map[i, j] = "-";
                        carts.Add(c = new Cart { Direction = new Point(1, 0) });
                    }
                    else if (s == "v")
                    {
                        map[i, j] = "|";
                        carts.Add(c = new Cart { Direction = new Point(0, 1) });
                    }
                    else
                    {
                        map[i, j] = s;
                    }
                    if (c != null)
                    {
                        c.Position = new Point(i, j);
                        c.LastTurn = new Point(1, 0);
                        c.Crashed = false;
                    }
                }
            }

            while (true)
            {
                carts = carts.OrderBy(x => x.Position.X).OrderBy(x => x.Position.Y).ToList();
                foreach (var c in carts)
                {
                    int newX = c.Position.X + c.Direction.X;
                    int newY = c.Position.Y + c.Direction.Y;

                    string nextMove = map[newX, newY];

                    Cart testCrash = carts.FirstOrDefault(x => x.Position.X == newX && x.Position.Y == newY);

                    c.Position.X = newX;
                    c.Position.Y = newY;

                    if (testCrash != null)
                    {
                        return newX + "," + newY;
                    }
                    if (nextMove == "+")
                    {
                        if (c.LastTurn.X == 1)
                        {
                            if (c.Direction.Y == 1)
                            {
                                c.Direction.Y = 0;
                                c.Direction.X = 1;
                            }
                            else if (c.Direction.Y == -1)
                            {
                                c.Direction.Y = 0;
                                c.Direction.X = -1;
                            }
                            else if (c.Direction.X == 1)
                            {
                                c.Direction.X = 0;
                                c.Direction.Y = -1;
                            }
                            else if (c.Direction.X == -1)
                            {
                                c.Direction.X = 0;
                                c.Direction.Y = 1;
                            }
                            c.LastTurn.X = -1;
                            c.LastTurn.Y = 0;
                        }
                        else if (c.LastTurn.X == -1)
                        {
                            c.LastTurn.X = 0;
                            c.LastTurn.Y = -1;
                        }
                        else
                        {
                            if (c.Direction.Y == 1)
                            {
                                c.Direction.Y = 0;
                                c.Direction.X = -1;
                            }
                            else if (c.Direction.Y == -1)
                            {
                                c.Direction.Y = 0;
                                c.Direction.X = 1;
                            }
                            else if (c.Direction.X == 1)
                            {
                                c.Direction.X = 0;
                                c.Direction.Y = 1;
                            }
                            else if (c.Direction.X == -1)
                            {
                                c.Direction.X = 0;
                                c.Direction.Y = -1;
                            }
                            c.LastTurn.X = 1;
                            c.LastTurn.Y = 0;
                        }
                    }
                    if (nextMove == "\\")
                    {
                        if (c.Direction.X == 1)
                        {
                            c.Direction.X = 0;
                            c.Direction.Y = 1;
                        }
                        else if (c.Direction.X == -1)
                        {
                            c.Direction.X = 0;
                            c.Direction.Y = -1;
                        }
                        else if (c.Direction.Y == 1)
                        {
                            c.Direction.X = 1;
                            c.Direction.Y = 0;
                        }
                        else if (c.Direction.Y == -1)
                        {
                            c.Direction.X = -1;
                            c.Direction.Y = 0;
                        }
                    }
                    if (nextMove == "/")
                    {
                        if (c.Direction.X == 1)
                        {
                            c.Direction.X = 0;
                            c.Direction.Y = -1;
                        }
                        else if (c.Direction.X == -1)
                        {
                            c.Direction.X = 0;
                            c.Direction.Y = 1;
                        }
                        else if (c.Direction.Y == 1)
                        {
                            c.Direction.X = -1;
                            c.Direction.Y = 0;
                        }
                        else if (c.Direction.Y == -1)
                        {
                            c.Direction.X = 1;
                            c.Direction.Y = 0;
                        }
                    }

                }
            }
        }

        private void PaintMap(string[,] map, int width, int height, List<Cart> carts)
        {
            Console.WriteLine();
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    Cart c = carts.FirstOrDefault(x => x.Position.X == j && x.Position.Y == i);
                    if (c != null)
                    {
                        string symbol = "^";
                        if (c.Direction.X == 1)
                        {
                            symbol = ">";
                        }
                        if (c.Direction.X == -1)
                        {
                            symbol = "<";
                        }
                        if (c.Direction.Y == 1)
                        {
                            symbol = "v";
                        }
                        Console.Write(symbol);
                    }
                    else
                    {
                        Console.Write(map[j, i]);
                    }
                }
                Console.WriteLine();
            }
        }

        public string RunTwo()
        {
            List<string> inputLines = System.IO.File.ReadAllLines(@"..\..\Data\2018\input13.txt").ToList();

            int width = inputLines[0].Length;
            int height = inputLines.Count;

            string[,] map = new string[inputLines[0].Length, inputLines.Count];
            List<Cart> carts = new List<Cart>();
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    string s = inputLines[j][i].ToString();
                    Cart c = null;
                    if (s == "^")
                    {
                        map[i, j] = "|";
                        carts.Add(c = new Cart { Direction = new Point(0, -1) });
                    }
                    else if (s == "<")
                    {
                        map[i, j] = "-";
                        carts.Add(c = new Cart { Direction = new Point(-1, 0) });
                    }
                    else if (s == ">")
                    {
                        map[i, j] = "-";
                        carts.Add(c = new Cart { Direction = new Point(1, 0) });
                    }
                    else if (s == "v")
                    {
                        map[i, j] = "|";
                        carts.Add(c = new Cart { Direction = new Point(0, 1) });
                    }
                    else
                    {
                        map[i, j] = s;
                    }
                    if (c != null)
                    {
                        c.Position = new Point(i, j);
                        c.LastTurn = new Point(1, 0);
                        c.Crashed = false;
                    }
                }
            }

            while (carts.Count(x => !x.Crashed) != 1)
            {
                carts = carts.Where(x => !x.Crashed).OrderBy(x => x.Position.X).OrderBy(x => x.Position.Y).ToList();
                foreach (var c in carts)
                {
                    if (!c.Crashed)
                    {
                        int newX = c.Position.X + c.Direction.X;
                        int newY = c.Position.Y + c.Direction.Y;

                        string nextMove = map[newX, newY];

                        Cart testCrash = carts.FirstOrDefault(x => x.Position.X == newX && x.Position.Y == newY && !x.Crashed);

                        c.Position.X = newX;
                        c.Position.Y = newY;

                        if (testCrash != null)
                        {
                            testCrash.Crashed = true;
                            c.Crashed = true;
                        }
                        if (nextMove == "+")
                        {
                            if (c.LastTurn.X == 1)
                            {
                                if (c.Direction.Y == 1)
                                {
                                    c.Direction.Y = 0;
                                    c.Direction.X = 1;
                                }
                                else if (c.Direction.Y == -1)
                                {
                                    c.Direction.Y = 0;
                                    c.Direction.X = -1;
                                }
                                else if (c.Direction.X == 1)
                                {
                                    c.Direction.X = 0;
                                    c.Direction.Y = -1;
                                }
                                else if (c.Direction.X == -1)
                                {
                                    c.Direction.X = 0;
                                    c.Direction.Y = 1;
                                }
                                c.LastTurn.X = -1;
                                c.LastTurn.Y = 0;
                            }
                            else if (c.LastTurn.X == -1)
                            {
                                c.LastTurn.X = 0;
                                c.LastTurn.Y = -1;
                            }
                            else
                            {
                                if (c.Direction.Y == 1)
                                {
                                    c.Direction.Y = 0;
                                    c.Direction.X = -1;
                                }
                                else if (c.Direction.Y == -1)
                                {
                                    c.Direction.Y = 0;
                                    c.Direction.X = 1;
                                }
                                else if (c.Direction.X == 1)
                                {
                                    c.Direction.X = 0;
                                    c.Direction.Y = 1;
                                }
                                else if (c.Direction.X == -1)
                                {
                                    c.Direction.X = 0;
                                    c.Direction.Y = -1;
                                }
                                c.LastTurn.X = 1;
                                c.LastTurn.Y = 0;
                            }
                        }
                        if (nextMove == "\\")
                        {
                            if (c.Direction.X == 1)
                            {
                                c.Direction.X = 0;
                                c.Direction.Y = 1;
                            }
                            else if (c.Direction.X == -1)
                            {
                                c.Direction.X = 0;
                                c.Direction.Y = -1;
                            }
                            else if (c.Direction.Y == 1)
                            {
                                c.Direction.X = 1;
                                c.Direction.Y = 0;
                            }
                            else if (c.Direction.Y == -1)
                            {
                                c.Direction.X = -1;
                                c.Direction.Y = 0;
                            }
                        }
                        if (nextMove == "/")
                        {
                            if (c.Direction.X == 1)
                            {
                                c.Direction.X = 0;
                                c.Direction.Y = -1;
                            }
                            else if (c.Direction.X == -1)
                            {
                                c.Direction.X = 0;
                                c.Direction.Y = 1;
                            }
                            else if (c.Direction.Y == 1)
                            {
                                c.Direction.X = -1;
                                c.Direction.Y = 0;
                            }
                            else if (c.Direction.Y == -1)
                            {
                                c.Direction.X = 1;
                                c.Direction.Y = 0;
                            }
                        }
                    }
                }
            }
            return carts[0].Position.X + "," + carts[0].Position.Y;
        }
    }
}
