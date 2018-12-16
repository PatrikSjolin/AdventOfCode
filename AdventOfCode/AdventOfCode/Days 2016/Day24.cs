using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Days_2016
{
    public class Day24 : IPuzzle
    {
        private int taskNumber = 24;

        public bool Active => false;

        public List<string> ReadInput(string fileName)
        {
            string path = string.Format("../../Tasks/{0}/{1}.txt", taskNumber, fileName);

            List<string> inputList = new List<string>();
            string line = "";
            System.IO.StreamReader file = new System.IO.StreamReader(path);
            while ((line = file.ReadLine()) != null)
            {
                inputList.Add(line);
            }

            return inputList;
        }

        public void GoOne()
        {
            List<string> rows = ReadInput("input");
            string[,] map = ConstructMap(rows);
            int[,] paths = new int[map.GetLength(0), map.GetLength(1)];


            Dictionary<string, Tuple<int, int>> startPoints = GetStartPoints(map);
            Dictionary<string, Dictionary<string, int>> shortestPaths = new Dictionary<string, Dictionary<string, int>>();

            foreach (var startPoint in startPoints)
            {
                List<Tuple<int, int>> unvisitedNodes = new List<Tuple<int, int>>();

                for (int i = 0; i < map.GetLength(0); i++)
                {
                    for (int j = 0; j < map.GetLength(1); j++)
                    {
                        if (map[i, j] != "#")
                        {
                            paths[i, j] = int.MaxValue;
                            unvisitedNodes.Add(new Tuple<int, int>(i, j));
                        }
                    }
                }
                paths[startPoint.Value.Item1, startPoint.Value.Item2] = 0;

                FindShortestPath(map, unvisitedNodes, paths, startPoint.Value.Item1, startPoint.Value.Item2);
                Dictionary<string, int> shortestPath = GetPathsToParts(paths, map);
                shortestPaths.Add(startPoint.Key, shortestPath);
            }
        }

        private Dictionary<string, int> GetPathsToParts(int[,] paths, string[,] map)
        {
            Dictionary<string, int> shortestPaths = new Dictionary<string, int>();

            for(int i = 0; i < map.GetLength(0); i++)
            {
                for(int j = 0; j < map.GetLength(1); j++)
                {
                    if(map[i, j] != "#" && map[i, j] != ".")
                    {
                        shortestPaths[map[i, j]] = paths[i, j];
                    }
                }
            }

            return shortestPaths;
        }

        private Dictionary<string, Tuple<int, int>> GetStartPoints(string[,] map)
        {
            Dictionary < string, Tuple < int, int>> startPoints = new Dictionary<string, Tuple<int, int>>();
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    if(map[i, j] != "#" && map[i, j] != ".")
                    {
                        startPoints.Add(map[i, j], new Tuple<int, int>(i, j));
                    }
                }
            }
            return startPoints;
        }

        private void FindShortestPath(string[,] map, List<Tuple<int, int>> unvisitedNodes, int[,] paths, int x, int y)
        {
            List<Tuple<int, int>> neighbours = GetNeighbours(map, x, y);

            unvisitedNodes.Remove(new Tuple<int, int>(x, y));

            foreach (var n in neighbours)
            {
                int nx = n.Item1;
                int ny = n.Item2;

                if (paths[x, y] + 1 < paths[nx, ny])
                {
                    paths[nx, ny] = paths[x, y] + 1;
                }
            }

            Tuple<int, int> nextNode = GetNextNode(unvisitedNodes, paths);
            if (nextNode == null) return;

            FindShortestPath(map, unvisitedNodes, paths, nextNode.Item1, nextNode.Item2);
        }

        private Tuple<int, int> GetNextNode(List<Tuple<int, int>> unvisitedNodes, int[,] paths)
        {
            int smallest = int.MaxValue;

            Tuple<int, int> node = null;

            foreach (var unvis in unvisitedNodes)
            {
                if(paths[unvis.Item1, unvis.Item2] < smallest)
                {
                    smallest = paths[unvis.Item1, unvis.Item2];
                    node = new Tuple<int, int>(unvis.Item1, unvis.Item2);
                }
            }
            return node;
        }

        private List<Tuple<int, int>> GetNeighbours(string[,] map, int x, int y)
        {
            List<Tuple<int, int>> neighbours = new List<Tuple<int, int>>();

            if (y + 1 < map.GetLength(1) && map[x, y + 1] != "#")
            {
                neighbours.Add(new Tuple<int, int>(x, y + 1));
            }
            if (x + 1 < map.GetLength(0) && map[x + 1, y] != "#")
            {
                neighbours.Add(new Tuple<int, int>(x + 1, y));
            }
            if (y - 1 >= 0 && map[x, y - 1] != "#")
            {
                neighbours.Add(new Tuple<int, int>(x, y - 1));
            }
            if (x - 1 >= 0 && map[x - 1, y] != "#")
            {
                neighbours.Add(new Tuple<int, int>(x - 1, y));
            }

            return neighbours;
        }

        private string[,] ConstructMap(List<string> rows)
        {
            string[,] map = new string[rows.Count, rows[0].Length];

            for(int i = 0; i < rows.Count; i++)
            {
                for(int j = 0; j < rows[i].Length; j++)
                {
                    map[i, j] = rows[i][j].ToString();
                }
            }

            return map;
        }

        public void GoTwo()
        {
        }

        public string RunOne()
        {
            throw new NotImplementedException();
        }

        public string RunTwo()
        {
            throw new NotImplementedException();
        }
    }
}
