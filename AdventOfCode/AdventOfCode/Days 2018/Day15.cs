using System;
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

        bool paint = false;
        List<string> inputLines = System.IO.File.ReadAllLines(@"..\..\Data\2018\input15.txt").ToList();

        public void PaintMap(string[,] map, int width, int height, List<Unit> units)
        {
            Console.WriteLine();

            for (int i = 0; i < height; i++)
            {
                List<Unit> rowUnites = new List<Unit>();
                for (int j = 0; j < width; j++)
                {
                    Unit u = units.FirstOrDefault(x => x.Position.X == j && x.Position.Y == i);
                    if (u != null)
                    {
                        rowUnites.Add(u);
                        if (u.Type == UnitType.Elf)
                            Console.Write("E");
                        else
                            Console.Write("G");
                    }
                    else
                        Console.Write(map[j, i]);
                }
                Console.Write("  ");
                foreach (var u in rowUnites)
                {
                    if (u.Type == UnitType.Elf)
                        Console.Write("E({0}), ", u.HitPoints);
                    else
                        Console.Write("G({0}), ", u.HitPoints);
                }
                Console.WriteLine();
            }
        }

        public void UpdateUnit(List<Unit> units, Unit unit, UnitType attackType, string[,] map, int width, int height)
        {
            var targets = units.Where(x => x.Type == attackType && x.HitPoints > 0).ToList();
            if (targets.Count() == 0)
                return;

            List<Unit> attackable = targets.Where(x =>
            (x.Position.X == unit.Position.X + 1 && x.Position.Y == unit.Position.Y) ||
            (x.Position.X == unit.Position.X - 1 && x.Position.Y == unit.Position.Y) ||
            (x.Position.X == unit.Position.X && x.Position.Y == unit.Position.Y + 1) ||
            (x.Position.X == unit.Position.X && x.Position.Y == unit.Position.Y - 1)).ToList();

            if (attackable.Count > 0)
            {
                Attack(unit, units, attackable);
            }
            else
            {
                List<Point> possibleDestinations = GetPossibleDestinations(targets, units, map);
                possibleDestinations = possibleDestinations.OrderBy(x => x.Y).ThenBy(x => x.X).ToList();
                if (paint)
                    PaintPossible(map, units, possibleDestinations, width, height);

                bool[,] realMap = ConstructMap(map, units, width, height);
                realMap[unit.Position.X, unit.Position.Y] = true;


                int shortest = int.MaxValue;
                int[,] shortestPath = null;

                Dictionary<int, int[,]> pathValues = new Dictionary<int, int[,]>();

                int shortestX = 0;
                int shortestY = 0;

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
                                unvisitedNodes.Add(new Point(i, j));
                            }
                            paths[i, j] = int.MaxValue;
                        }
                    }

                    paths[p.X, p.Y] = 0;
                    Utilities.FindShortestPath(realMap, unvisitedNodes, paths, p.X, p.Y);

                    int distance = paths[unit.Position.X, unit.Position.Y];

                    if (distance > 0 && distance != int.MaxValue)
                    {
                        if (distance < shortest)
                        {
                            shortest = distance;
                            shortestPath = paths;
                            shortestX = p.X;
                            shortestY = p.Y;
                        }
                    }
                }
                if (paint)
                    Console.WriteLine();

                if (shortestPath != null)
                {
                    if (paint)
                        PaintShortestPath(shortestPath, width, height);

                    int up = shortestPath[unit.Position.X, unit.Position.Y - 1];
                    if (up == 0 && !(unit.Position.X == shortestX && (unit.Position.Y - 1) == shortestY))
                        up = int.MaxValue;
                    int down = shortestPath[unit.Position.X, unit.Position.Y + 1];
                    if (down == 0 && !(unit.Position.X == shortestX && (unit.Position.Y + 1) == shortestY))
                        down = int.MaxValue;
                    int left = shortestPath[unit.Position.X - 1, unit.Position.Y];
                    if (left == 0 && !((unit.Position.X - 1) == shortestX && unit.Position.Y == shortestY))
                        left = int.MaxValue;
                    int right = shortestPath[unit.Position.X + 1, unit.Position.Y];
                    if (right == 0 && !((unit.Position.X + 1) == shortestX && unit.Position.Y == shortestY))
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

                    var targets2 = units.Where(x => x.Type == attackType && x.HitPoints > 0).ToList();
                    if (targets2.Count() == 0)
                        return;

                    List<Unit> attackable2 = targets.Where(x =>
                    (x.Position.X == unit.Position.X + 1 && x.Position.Y == unit.Position.Y) ||
                    (x.Position.X == unit.Position.X - 1 && x.Position.Y == unit.Position.Y) ||
                    (x.Position.X == unit.Position.X && x.Position.Y == unit.Position.Y + 1) ||
                    (x.Position.X == unit.Position.X && x.Position.Y == unit.Position.Y - 1)).ToList();

                    if (attackable2.Count > 0)
                    {
                        Attack(unit, units, attackable2);
                    }
                }
            }
        }

        private void Attack(Unit unit, List<Unit> units, List<Unit> attackable)
        {
            if (attackable.Count > 1)
            {
                attackable = attackable.OrderBy(x => x.HitPoints).ToList();

                if (attackable[0].HitPoints != attackable[1].HitPoints)
                {
                    Unit u = attackable[0];
                    if (unit.HitPoints > 0)
                        u.HitPoints -= unit.AttackPower;
                    if (u.HitPoints <= 0)
                        units.RemoveAll(x => x.Position.X == u.Position.X && x.Position.Y == u.Position.Y);
                }
                else
                {
                    //More than one with same hitpoints
                    attackable = attackable.OrderBy(x => x.Position.Y).ThenBy(x => x.Position.X).ToList();
                    Unit u = attackable[0];
                    if (unit.HitPoints > 0)
                        u.HitPoints -= unit.AttackPower;
                    if (u.HitPoints <= 0)
                        units.RemoveAll(x => x.Position.X == u.Position.X && x.Position.Y == u.Position.Y);
                }
            }
            else
            {
                //One unit to attack
                Unit u = attackable[0];
                if (unit.HitPoints > 0)
                    u.HitPoints -= unit.AttackPower;
                if (u.HitPoints <= 0)
                    units.RemoveAll(x => x.Position.X == u.Position.X && x.Position.Y == u.Position.Y);
            }
        }

        private void PaintShortestPath(int[,] shortestPath, int width, int height)
        {
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    if (shortestPath[j, i] == int.MaxValue)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("##");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else
                    {
                        if (shortestPath[j, i] < 10)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write("0");
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                        Console.Write(shortestPath[j, i]);
                    }
                }
                Console.WriteLine();
            }
        }

        private bool[,] ConstructMap(string[,] map, List<Unit> units, int width, int height)
        {
            bool[,] realMap = new bool[width, height];
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
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
                    else if (p != null)
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

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    string content = inputLines[i][j].ToString();

                    if (content == "G")
                    {
                        units.Add(new Unit { HitPoints = 200, AttackPower = 3, Position = new Point(j, i), Type = UnitType.Goblin });
                        map[j, i] = ".";
                    }
                    else if (content == "E")
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
            if (paint)
            {
                Console.WriteLine("Initial map");
                PaintMap(map, width, height, units);
                Console.WriteLine();
            }
            int rounds = 0;

            while (units.Count(x => x.Type == UnitType.Elf) > 0 && units.Count(x => x.Type == UnitType.Goblin) > 0)
            {
                units = units.OrderBy(x => x.Position.Y).ThenBy(x => x.Position.X).ToList();

                foreach (var unit in units.ToList())
                {
                    if (unit.Type == UnitType.Elf)
                    {
                        if (unit.HitPoints > 0)
                            UpdateUnit(units, unit, UnitType.Goblin, map, width, height);
                        else
                        {

                        }
                    }
                    else
                    {
                        if (unit.HitPoints > 0)
                            UpdateUnit(units, unit, UnitType.Elf, map, width, height);
                        else
                        {

                        }
                    }
                }
                rounds++;
                if (paint)
                {
                    Console.WriteLine("After round {0}", rounds);
                    PaintMap(map, width, height, units);
                    Console.ReadKey();
                }
            }

            int sum = units.Where(x => x.HitPoints >= 0).Sum(x => x.HitPoints);

            return (rounds - 1) * sum + "";
        }

        private List<Point> GetPossibleDestinations(List<Unit> targets, List<Unit> allUnits, string[,] map)
        {
            List<Point> points = new List<Point>();

            foreach (var t in targets)
            {
                if (map[t.Position.X + 1, t.Position.Y] == ".")
                {
                    if (allUnits.Count(x => x.Position.X == t.Position.X + 1 && x.Position.Y == t.Position.Y) > 0)
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
            int attackPower = 4;
            while (true)
            {
                int width = inputLines[0].Length;
                int height = inputLines.Count;

                string[,] map = new string[inputLines[0].Length, inputLines.Count];

                int elves = 0;

                List<Unit> units = new List<Unit>();

                for (int i = 0; i < height; i++)
                {
                    for (int j = 0; j < width; j++)
                    {
                        string content = inputLines[i][j].ToString();

                        if (content == "G")
                        {
                            units.Add(new Unit { HitPoints = 200, AttackPower = 3, Position = new Point(j, i), Type = UnitType.Goblin });
                            map[j, i] = ".";
                        }
                        else if (content == "E")
                        {
                            elves++;
                            units.Add(new Unit { HitPoints = 200, AttackPower = attackPower, Position = new Point(j, i), Type = UnitType.Elf });
                            map[j, i] = ".";
                        }
                        else
                        {
                            map[j, i] = inputLines[i][j].ToString();
                        }
                    }
                }
                if (paint)
                {
                    Console.WriteLine("Initial map");
                    PaintMap(map, width, height, units);
                    Console.WriteLine();
                }
                int rounds = 0;

                while (units.Count(x => x.Type == UnitType.Elf) > 0 && units.Count(x => x.Type == UnitType.Goblin) > 0)
                {
                    units = units.OrderBy(x => x.Position.Y).ThenBy(x => x.Position.X).ToList();

                    foreach (var unit in units.ToList())
                    {
                        if (unit.Type == UnitType.Elf)
                        {
                            if (unit.HitPoints > 0)
                                UpdateUnit(units, unit, UnitType.Goblin, map, width, height);
                            else
                            {

                            }
                        }
                        else
                        {
                            if (unit.HitPoints > 0)
                                UpdateUnit(units, unit, UnitType.Elf, map, width, height);
                            else
                            {

                            }
                        }
                    }
                    if (units.Count(x => x.Type == UnitType.Elf) != elves)
                        break;
                    rounds++;
                    if (paint)
                    {
                        Console.WriteLine("After round {0}", rounds);
                        PaintMap(map, width, height, units);
                        Console.ReadKey();
                    }
                }
                if (units.Count(x => x.Type == UnitType.Elf) == elves)
                {
                    int sum = units.Where(x => x.HitPoints >= 0).Sum(x => x.HitPoints);
                    return (rounds - 1) * sum + "";
                }
                attackPower++;
            }
        }
    }
}
