using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace AdventOfCode
{
    public class Point
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public void Rotate90DegreesCWAroundOrigin(int steps)
        {
            for (int j = 0; j < steps; j++)
            {
                int oldX = X;
                X = Y;
                Y = oldX * -1;
            }
        }

        public void Rotate90DegreesCCWAroundOrigin(int steps)
        {
            for (int j = 0; j < steps; j++)
            {
                int oldX = X;
                X = Y * -1;
                Y = oldX;
            }
        }

        public override bool Equals(object obj)
        {
            Point p = (Point)obj;

            return p.X == X && p.Y == Y;
        }

        public override int GetHashCode()
        {
            return X * Y + 11 * 2;
        }
    }

    public class Point3D : Point
    {
        public int Z { get; set; }

        public Point3D(int x, int y, int z) : base(x, y)
        {
            Z = z;
        }

        public override bool Equals(object obj)
        {
            Point3D p = (Point3D)obj;
            return p.Z == Z && base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return Z * base.GetHashCode() * 13 + 21;
        }
    }

    public static class Utilities
    {
        public static int GetManhattanDistance(int x, int y)
        {
            return Math.Abs(x) + Math.Abs(y);
        }

        public static int GetManhattanDistance(int x, int y, int originX, int originY)
        {
            return Math.Abs(x - originX) + Math.Abs(y - originY);
        }

        public static int ModuloWithNegativeNumbersForIndex(int index, int modulo)
        {
            return ((index % modulo) + modulo) % modulo;
        }

        public static void GenerateCoordinates(int distance)
        {
            int r = distance;

            for (int a = -r; a <= r; a++)
            {
                for (int b = -r + Math.Abs(a); b <= r - Math.Abs(a); b++)
                {
                    for (int c = -r + Math.Abs(a) + Math.Abs(b); c <= r - (Math.Abs(a) + Math.Abs(b)); c++)
                    {
                    }
                }
            }
        }

        public static void FindShortestPath(bool[,] map, List<Point> unvisitedNodes, int[,] paths, int x, int y)
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

            List<Point> notVisitedNeighbours = unvisitedNodes.Where(xx => paths[xx.X, xx.Y] < int.MaxValue).ToList();

            int smallestCost = int.MaxValue;
            int smallestX = 0;
            int smallestY = 0;

            foreach (var nv in notVisitedNeighbours)
            {
                if (paths[nv.X, nv.Y] < smallestCost)
                {
                    smallestCost = paths[nv.X, nv.Y];
                    smallestX = nv.X;
                    smallestY = nv.Y;
                }
            }

            if (notVisitedNeighbours.Count() > 0)
                FindShortestPath(map, unvisitedNodes, paths, smallestX, smallestY);
        }

        public static void FindShortestPath2(bool[,] map, List<Point> unvisitedNodes, int[,] paths, int x, int y)
        {
            List<Point> neighbours = GetNeighbours2(map, x, y);

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

            List<Point> notVisitedNeighbours = unvisitedNodes.Where(xx => paths[xx.X, xx.Y] < int.MaxValue).ToList();

            int smallestCost = int.MaxValue;
            int smallestX = 0;
            int smallestY = 0;

            foreach (var nv in notVisitedNeighbours)
            {
                if (paths[nv.X, nv.Y] < smallestCost)
                {
                    smallestCost = paths[nv.X, nv.Y];
                    smallestX = nv.X;
                    smallestY = nv.Y;
                }
            }

            if (notVisitedNeighbours.Count() > 0)
                FindShortestPath2(map, unvisitedNodes, paths, smallestX, smallestY);
        }

        public static void FindShortestPath2(bool[,] map, List<Point> unvisitedNodes, int[,] paths, int x, int y, Point destination)
        {
            List<Point> neighbours = GetNeighbours2(map, x, y);

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

            //List<Point> notVisitedNeighbours = unvisitedNodes.Where(xx => paths[xx.X, xx.Y] < int.MaxValue).ToList();

            int smallestCost = int.MaxValue;
            int smallestX = 0;
            int smallestY = 0;

            foreach (var nv in unvisitedNodes)
            {
                if (paths[nv.X, nv.Y] < smallestCost)
                {
                    smallestCost = paths[nv.X, nv.Y];
                    smallestX = nv.X;
                    smallestY = nv.Y;
                }
            }

            if (smallestX == destination.X && smallestY == destination.Y)
                return;

            if (smallestCost != int.MaxValue)
                FindShortestPath2(map, unvisitedNodes, paths, smallestX, smallestY);
        }

        public static void FindShortestPath(bool[,] map, List<Point> unvisitedNodes, int[,] paths, Point current, Point destination)
        {
            List<Point> neighbours = GetNeighbours(map, current.X, current.Y);

            unvisitedNodes.Remove(current);

            foreach (var n in neighbours)
            {
                int nx = n.X;
                int ny = n.Y;

                if (paths[current.X, current.Y] + 1 < paths[nx, ny])
                {
                    paths[nx, ny] = paths[current.X, current.Y] + 1;
                }
            }

            int smallestCost = int.MaxValue;
            int smallestX = 0;
            int smallestY = 0;

            foreach (var nv in unvisitedNodes)
            {
                if (paths[nv.X, nv.Y] < smallestCost)
                {
                    smallestCost = paths[nv.X, nv.Y];
                    smallestX = nv.X;
                    smallestY = nv.Y;
                }
            }

            if (smallestX == destination.X && smallestY == destination.Y)
                return;

            if (smallestCost != int.MaxValue)
                FindShortestPath(map, unvisitedNodes, paths, new Point(smallestX, smallestY), destination);
        }

        static Dictionary<Point, int> visitedNodesWithTools = new Dictionary<Point, int>();

        public static void FindShortestPath(string[,] map, List<Point> unvisitedNodes, int[,] paths, Point current, Point destination, int tool)
        {
            List<Point> neighbours = GetNeighbours(map, current.X, current.Y);

            unvisitedNodes.Remove(current);

            foreach (var n in neighbours)
            {
                int nx = n.X;
                int ny = n.Y;

                KeyValuePair<int, int> costToGoToNew = GetCostAndTool(tool, map[current.X, current.Y], map[nx, ny]);


                if (paths[current.X, current.Y] + costToGoToNew.Key < paths[nx, ny])
                {
                    visitedNodesWithTools[new Point(nx, ny)] = costToGoToNew.Value;
                    paths[nx, ny] = paths[current.X, current.Y] + costToGoToNew.Key;
                }
            }

            int smallestCost = int.MaxValue;
            int smallestX = 0;
            int smallestY = 0;

            foreach (var nv in unvisitedNodes)
            {
                if (paths[nv.X, nv.Y] < smallestCost)
                {
                    smallestCost = paths[nv.X, nv.Y];
                    smallestX = nv.X;
                    smallestY = nv.Y;
                }
            }

            if (smallestX == destination.X && smallestY == destination.Y)
            {
                int exitTool = visitedNodesWithTools[new Point(smallestX, smallestY)];
                if (exitTool != 1)
                    paths[smallestX, smallestY] += 7;
                return;
            }

            if (smallestCost != int.MaxValue)
            {
                KeyValuePair<int, int> costToGoToNew = GetCostAndTool(visitedNodesWithTools[new Point(smallestX, smallestY)], map[current.X, current.Y],
                    map[smallestX, smallestY]);
                FindShortestPath(map, unvisitedNodes, paths, new Point(smallestX, smallestY), destination, costToGoToNew.Value);
            }
        }

        private static KeyValuePair<int, int> GetCostAndTool(int tool, string v1, string v2)
        {
            //KeyValuePair<int, int> costAndTool = new KeyValuePair<int, int>();
            int cost = -1;
            //int tool = -1;
            if (v1 == v2)
            {
                cost = 1;
                return new KeyValuePair<int, int>(cost, tool);
            }

            if (v1 == "." && v2 == "=")
            {
                if (tool == 2)
                {
                    cost = 1;
                    return new KeyValuePair<int, int>(cost, tool);
                }
                else
                {
                    cost = 8;
                    return new KeyValuePair<int, int>(cost, 2);
                }
            }
            if (v1 == "." && v2 == "|")
            {
                if (tool == 1)
                {
                    cost = 1;
                    return new KeyValuePair<int, int>(cost, tool);
                }
                else
                {
                    cost = 8;
                    return new KeyValuePair<int, int>(cost, 1);
                }
            }
            if (v1 == "=" && v2 == ".")
            {
                if (tool == 2)
                {
                    cost = 1;
                    return new KeyValuePair<int, int>(cost, tool);
                }
                else
                {
                    cost = 8;
                    return new KeyValuePair<int, int>(cost, 2);
                }
            }
            if (v1 == "=" && v2 == "|")
            {
                if (tool == 0)
                {
                    cost = 1;
                    return new KeyValuePair<int, int>(cost, tool);
                }
                else
                {
                    cost = 8;
                    return new KeyValuePair<int, int>(cost, 0);
                }
            }
            if (v1 == "|" && v2 == ".")
            {
                if (tool == 1)
                {
                    cost = 1;
                    return new KeyValuePair<int, int>(cost, tool);
                }
                else
                {
                    cost = 8;
                    return new KeyValuePair<int, int>(cost, 1);
                }
            }
            if (v1 == "|" && v2 == "=")
            {
                if (tool == 0)
                {
                    cost = 1;
                    return new KeyValuePair<int, int>(cost, tool);
                }
                else
                {
                    cost = 8;
                    return new KeyValuePair<int, int>(cost, 0);
                }
            }
            cost = 1;
            return new KeyValuePair<int, int>(cost, tool);
        }

        private static List<Point> GetNeighbours(bool[,] map, int x, int y)
        {
            List<Point> neighbours = new List<Point>();

            if (y + 1 < map.GetLength(1) && map[x, y + 1])
            {
                neighbours.Add(new Point(x, y + 1));
            }
            if (x + 1 < map.GetLength(0) && map[x + 1, y])
            {
                neighbours.Add(new Point(x + 1, y));
            }
            if (y - 1 >= 0 && map[x, y - 1])
            {
                neighbours.Add(new Point(x, y - 1));
            }
            if (x - 1 >= 0 && map[x - 1, y])
            {
                neighbours.Add(new Point(x - 1, y));
            }

            return neighbours;
        }

        private static List<Point> GetNeighbours(string[,] map, int x, int y)
        {
            List<Point> neighbours = new List<Point>();

            if (y + 1 < map.GetLength(1))
            {
                neighbours.Add(new Point(x, y + 1));
            }
            if (x + 1 < map.GetLength(0))
            {
                neighbours.Add(new Point(x + 1, y));
            }
            if (y - 1 >= 0)
            {
                neighbours.Add(new Point(x, y - 1));
            }
            if (x - 1 >= 0)
            {
                neighbours.Add(new Point(x - 1, y));
            }

            return neighbours;
        }

        private static List<Point> GetNeighbours2(bool[,] map, int x, int y)
        {
            List<Point> neighbours = new List<Point>();

            if (y + 2 < map.GetLength(1) && map[x, y + 1] && map[x, y + 2])
            {
                neighbours.Add(new Point(x, y + 2));
            }
            if (x + 2 < map.GetLength(0) && map[x + 1, y] && map[x + 2, y])
            {
                neighbours.Add(new Point(x + 2, y));
            }
            if (y - 2 >= 0 && map[x, y - 1] && map[x, y - 2])
            {
                neighbours.Add(new Point(x, y - 2));
            }
            if (x - 2 >= 0 && map[x - 1, y] && map[x - 2, y])
            {
                neighbours.Add(new Point(x - 2, y));
            }

            return neighbours;
        }

        public static string CalculateMD5Hash(string input)
        {
            MD5 md5 = MD5.Create();

            byte[] inputBytes = Encoding.ASCII.GetBytes(input);

            byte[] hash = md5.ComputeHash(inputBytes);

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }

            return sb.ToString();
        }

        public static bool IsAnagram(string first, string second)
        {
            if (first.Length != second.Length)
                return false;

            if (first == second)
                return true;//or false: Don't know whether a string counts as an anagram of itself

            Dictionary<char, int> pool = new Dictionary<char, int>();
            foreach (char element in first.ToCharArray()) //fill the dictionary with that available chars and count them up
            {
                if (pool.ContainsKey(element))
                    pool[element]++;
                else
                    pool.Add(element, 1);
            }
            foreach (char element in second.ToCharArray()) //take them out again
            {
                if (!pool.ContainsKey(element)) //if a char isn't there at all; we're out
                    return false;
                if (--pool[element] == 0) //if a count is less than zero after decrement; we're out
                    pool.Remove(element);
            }
            return pool.Count == 0;
        }

        public static int Tribonacci(int n)
        {
            if (n == 0)
                return 0;
            else if (n == 1)
                return 1;
            else if (n == 2)
                return 2;
            else if (n == 3)
                return 4;
            else
                return Tribonacci(n - 1) + Tribonacci(n - 2) + Tribonacci(n - 3);
        }

        public static long Factorial(int n)
        {
            if (n == 1)
                return 1;
            else
                return n * Factorial(n - 1);
        }
    }
}
