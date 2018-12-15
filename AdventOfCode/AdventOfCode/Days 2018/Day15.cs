﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Days_2018
{
    public enum UnitType
    {
        Elf,
        Goblin
    };

    public class Unit
    {
        public UnitType Type { get; set; }
        public int HitPoints { get; set; }
        public int AttackPower { get; set; }
        public Point Position { get; set; }
    }

    public class Day15 : IPuzzle
    {
        public bool Active { get => true; }

        List<string> inputLines = System.IO.File.ReadAllLines(@"..\..\Data\2018\input15test.txt").ToList();

        public void PaintMap(string[,] map, int width, int height, List<Unit> units)
        {
            Console.WriteLine();

            for (int i = 0; i < height; i++)
            {
                for(int j = 0; j < width; j++)
                {
                    Unit u = units.FirstOrDefault(x => x.Position.X == j && x.Position.Y == i);
                    if(u != null)
                    {
                        if (u.Type == UnitType.Elf)
                            Console.Write("E");
                        else
                            Console.Write("G");
                    }
                    else
                        Console.Write(map[j, i]);
                }
                Console.WriteLine();
            }
        }

        public void UpdateUnit(List<Unit> units, Unit unit, UnitType attackType, string[,] map, int width, int height)
        {
            var targets = units.Where(x => x.Type == attackType).ToList();
            if (targets.Count() == 0)
                return;

            List<Unit> attackable = targets.Where(x =>
            (x.Position.X == unit.Position.X + 1 && x.Position.Y == unit.Position.Y) ||
            (x.Position.X == unit.Position.X - 1 && x.Position.Y == unit.Position.Y) ||
            (x.Position.X == unit.Position.X && x.Position.Y == unit.Position.Y + 1) ||
            (x.Position.X == unit.Position.X && x.Position.Y == unit.Position.Y - 1)).ToList();

            if (attackable.Count > 0)
            {
                if (attackable.Count > 1)
                {
                    attackable = attackable.OrderBy(x => x.HitPoints).ToList();

                    if (attackable[0].HitPoints != attackable[1].HitPoints)
                    {
                        Unit u = attackable[0];
                        u.HitPoints -= unit.AttackPower;
                        if (u.HitPoints <= 0)
                            units.RemoveAll(x => x.Position.X == u.Position.X && x.Position.Y == u.Position.Y);
                    }
                    else
                    {
                        //More than one with same hitpoints
                        attackable = attackable.OrderBy(x => x.Position.X).OrderBy(x => x.Position.Y).ToList();
                        Unit u = attackable[0];
                        u.HitPoints -= unit.AttackPower;
                        if (u.HitPoints <= 0)
                            units.RemoveAll(x => x.Position.X == u.Position.X && x.Position.Y == u.Position.Y);
                    }
                }
                else
                {
                    //One unit to attack
                    Unit u = attackable[0];
                    u.HitPoints -= unit.AttackPower;
                    if (u.HitPoints <= 0)
                        units.RemoveAll(x => x.Position.X == u.Position.X && x.Position.Y == u.Position.Y);
                }
            }
            else
            {
                List<Point> possibleDestinations = GetPossibleDestinations(targets, units, map);
                possibleDestinations = possibleDestinations.OrderBy(x => x.Y).OrderBy(x => x.X).ToList();
                //PaintPossible(map, units, possibleDestinations, width, height);

                bool[,] realMap = ConstructMap(map, units, width, height);
                realMap[unit.Position.X, unit.Position.Y] = true;


                int shortest = int.MaxValue;
                int[,] shortestPath = null;

                Dictionary<int, int[,]> pathValues = new Dictionary<int, int[,]>();

                foreach (var p in possibleDestinations)
                {
                    List<Point> unvisitedNodes = new List<Point>();
                    int[,] paths = new int[width, height];

                    for (int i = 0; i < width; i++)
                    {
                        for (int j = 0; j < height; j++)
                        {
                            if (realMap[i, j] == true)
                            {
                                paths[i, j] = int.MaxValue;
                                unvisitedNodes.Add(new Point(i, j));
                            }
                        }
                    }

                    paths[p.X, p.Y] = 0;
                    FindShortestPath(realMap, unvisitedNodes, paths, p.X, p.Y);

                    int distance = paths[unit.Position.X, unit.Position.Y];

                    if (distance > 0 && distance != int.MaxValue)
                    {
                        if(distance < shortest)
                        {
                            shortest = distance;
                            shortestPath = paths;
                        }
                    }

                }
                if (shortestPath != null)
                {
                    int up = shortestPath[unit.Position.X, unit.Position.Y - 1];
                    if (up == 0)
                        up = int.MaxValue;
                    int down = shortestPath[unit.Position.X, unit.Position.Y + 1];
                    if (down == 0)
                        down = int.MaxValue;
                    int left = shortestPath[unit.Position.X - 1, unit.Position.Y];
                    if (left == 0)
                        left = int.MaxValue;
                    int right = shortestPath[unit.Position.X + 1, unit.Position.Y];
                    if (right == 0)
                        right = int.MaxValue;

                    List<int> points = new List<int> { up, down, left, right };

                    int smallest = points.Min();

                    if (smallest == up)
                    {
                        unit.Position.Y--;
                    }
                    else if (smallest == left)
                    {
                        unit.Position.X--;
                    }
                    else if (smallest == right)
                    {
                        unit.Position.X++;
                    }
                    else if (smallest == down)
                    {
                        unit.Position.Y++;
                    }
                }
            }
        }

        private List<Point> GetNeighbours(bool[,] map, int x, int y)
        {
            List<Point> neighbours = new List<Point>();

            if (y + 1 < map.GetLength(1) && map[x, y + 1] == true)
            {
                neighbours.Add(new Point(x, y + 1));
            }
            if (x + 1 < map.GetLength(0) && map[x + 1, y] == true)
            {
                neighbours.Add(new Point(x + 1, y));
            }
            if (y - 1 >= 0 && map[x, y - 1] == true)
            {
                neighbours.Add(new Point(x, y - 1));
            }
            if (x - 1 >= 0 && map[x - 1, y] == true)
            {
                neighbours.Add(new Point(x - 1, y));
            }

            return neighbours;
        }

        private void FindShortestPath(bool[,] map, List<Point> unvisitedNodes, int[,] paths, int x, int y)
        {
            List<Point> neighbours = GetNeighbours(map, x, y);

            unvisitedNodes.Remove(new Point(x, y));

            foreach (var n in neighbours)
            {
                int nx = n.X;
                int ny = n.Y;

                if (paths[x, y] + 1 < paths[nx, ny])
                {
                    paths[nx, ny] = paths[x, y] + 1;
                }
            }

            foreach (var n in neighbours)
            {
                int nx = n.X;
                int ny = n.Y;

                if (unvisitedNodes.Contains(new Point(nx, ny)))
                {
                    FindShortestPath(map, unvisitedNodes, paths, nx, ny);
                }
            }
        }

        private bool[,] ConstructMap(string[,] map, List<Unit> units, int width, int height)
        {
            bool[,] realMap = new bool[width, height];
            for (int i = 0; i < width; i++)
            {
                for(int j = 0; j < height; j++)
                {
                    Unit u = units.FirstOrDefault(x => x.Position.X == i && x.Position.Y == j);
                    if (u != null)
                        realMap[i, j] = false;
                    else if (map[i, j] == "#")
                        realMap[i, j] = false;
                    else
                        realMap[i, j] = true;
                }
            }

            return realMap;
        }

        private void PaintPossible(string[,] map, List<Unit> units, List<Point> possibleDestinations, int width, int height)
        {
            Console.WriteLine();

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    Unit u = units.FirstOrDefault(x => x.Position.X == j && x.Position.Y == i);
                    Point p = possibleDestinations.FirstOrDefault(x => x.X == j && x.Y == i);
                    if (u != null)
                    {
                        if (u.Type == UnitType.Elf)
                            Console.Write("E");
                        else
                            Console.Write("G");
                    }
                    else if(p != null)
                    {
                        Console.Write("?");
                    }
                    else
                        Console.Write(map[j, i]);
                }
                Console.WriteLine();
            }
        }

        public string RunOne()
        {
            int width = inputLines[0].Length;
            int height = inputLines.Count;

            string[,] map = new string[inputLines[0].Length, inputLines.Count];

            List<Unit> units = new List<Unit>();

            for(int i = 0; i < height; i++)
            {
                for(int j = 0; j < width; j++)
                {
                    string content = inputLines[i][j].ToString();

                    if(content == "G")
                    {
                        units.Add(new Unit { HitPoints = 200, AttackPower = 3, Position = new Point(j, i), Type = UnitType.Goblin });
                        map[j, i] = ".";
                    }
                    else if(content == "E")
                    {
                        units.Add(new Unit { HitPoints = 200, AttackPower = 3, Position = new Point(j, i), Type = UnitType.Elf });
                        map[j, i] = ".";
                    }
                    else
                    {
                        map[j, i] = inputLines[i][j].ToString();
                    }
                }
            }
            PaintMap(map, width, height, units);

            int rounds = 0;

            while (units.Count(x => x.Type == UnitType.Elf) > 0 && units.Count(x => x.Type == UnitType.Goblin) > 0)
            {
                units = units.OrderBy(x => x.Position.X).OrderBy(x => x.Position.Y).ToList();

                foreach (var unit in units.ToList())
                {
                    if(unit.Type == UnitType.Elf)
                    {
                        UpdateUnit(units, unit, UnitType.Goblin, map, width, height);
                    }
                    else
                    {
                        UpdateUnit(units, unit, UnitType.Elf, map, width, height);
                    }
                }
                rounds++;
                //PaintMap(map, width, height, units);
                //Console.ReadKey();
            }

            int sum = units.Where(x => x.HitPoints >= 0).Sum(x => x.HitPoints);

            return (rounds * sum) + "";
        }

        private List<Point> GetPossibleDestinations(List<Unit> targets, List<Unit> allUnits, string[,] map)
        {
            List<Point> points = new List<Point>();

            foreach(var t in targets)
            {
                if(map[t.Position.X + 1, t.Position.Y] == ".")
                {
                    if(allUnits.Count(x => x.Position.X == t.Position.X + 1 && x.Position.Y == t.Position.Y) > 0)
                    {
                        //Blocked by other unit
                    }
                    else
                    {
                        points.Add(new Point(t.Position.X + 1, t.Position.Y));
                    }
                }
                if (map[t.Position.X - 1, t.Position.Y] == ".")
                {
                    if (allUnits.Count(x => x.Position.X == t.Position.X - 1 && x.Position.Y == t.Position.Y) > 0)
                    {
                        //Blocked by other unit
                    }
                    else
                    {
                        points.Add(new Point(t.Position.X - 1, t.Position.Y));
                    }
                }
                if (map[t.Position.X, t.Position.Y + 1] == ".")
                {
                    if (allUnits.Count(x => x.Position.X == t.Position.X && x.Position.Y == t.Position.Y + 1) > 0)
                    {
                        //Blocked by other unit
                    }
                    else
                    {
                        points.Add(new Point(t.Position.X, t.Position.Y + 1));
                    }
                }
                if (map[t.Position.X, t.Position.Y - 1] == ".")
                {
                    if (allUnits.Count(x => x.Position.X == t.Position.X && x.Position.Y == t.Position.Y - 1) > 0)
                    {
                        //Blocked by other unit
                    }
                    else
                    {
                        points.Add(new Point(t.Position.X, t.Position.Y - 1));
                    }
                }
            }

            return points;
        }

        public string RunTwo()
        {
         
            return "";
        }
    }
}
