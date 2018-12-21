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

        public override bool Equals(object obj)
        {
            Point p = (Point)obj;

            if (p.X == X && p.Y == Y)
            {
                return true;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return X * Y + 11 * 2;
        }
    }

    public static class Utilities
    {
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

            //List<Point> notVisitedNeighbours = unvisitedNodes.Where(x => paths[x.X, x.Y] < int.MaxValue).ToList();

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

        private static List<Point> GetNeighbours(bool[,] map, int x, int y)
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
    }
}
