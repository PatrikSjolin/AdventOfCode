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

        List<string> inputLines = System.IO.File.ReadAllLines(@"..\..\Data\2018\input15.txt").ToList();

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

        public void UpdateUnit(List<Unit> units, Unit unit, UnitType attackType, string[,] map)
        {

            var targets = units.Where(x => x.Type == attackType);
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
                        attackable = attackable.OrderBy(x => x.Position.X).OrderBy(x => x.Position.Y).ToList();
                        //More than one with same hitpoints
                        Unit u = attackable[0];
                        u.HitPoints -= unit.AttackPower;
                        if (u.HitPoints <= 0)
                            units.RemoveAll(x => x.Position.X == u.Position.X && x.Position.Y == u.Position.Y);
                    }
                }
                else
                {
                    Unit u = attackable[0];
                    u.HitPoints -= unit.AttackPower;
                    if (u.HitPoints <= 0)
                        units.RemoveAll(x => x.Position.X == u.Position.X && x.Position.Y == u.Position.Y);
                }
            }
            else
            {
                List<Point> possibleDestinations = GetPossibleDestinations(targets, map);
                List<Point> reachable = GetReachable(unit, map);
                Point closest = GetClosest(unit, reachable, map);
                if(closest.Y > unit.Position.Y)
                {

                }
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

                foreach (var unit in units)
                {
                    if(unit.Type == UnitType.Elf)
                    {
                        UpdateUnit(units, unit, UnitType.Goblin, map);
                    }
                    else
                    {
                        UpdateUnit(units, unit, UnitType.Elf, map);
                    }
                }
                rounds++;
                PaintMap(map, width, height, units);
                Console.ReadKey();
            }


            return "";
        }

        private Point GetClosest(Unit unit, List<Point> reachable, string[,] map)
        {
            Point p = new Point(0, 0);

            return p;
        }

        private List<Point> GetReachable(Unit unit, string[,] map)
        {
            List<Point> points = new List<Point>();

            return points;
        }

        private List<Point> GetPossibleDestinations(IEnumerable<Unit> targets, string[,] map)
        {
            List<Point> points = new List<Point>();


            return points;
        }

        public string RunTwo()
        {
         
            return "";
        }
    }
}
