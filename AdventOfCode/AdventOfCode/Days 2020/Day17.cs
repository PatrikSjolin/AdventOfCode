﻿using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Days_2020
{
    public class Day17 : IPuzzle
    {
        public bool Active => true;

        private List<string> inputs;

        public string RunOne()
        {
            inputs = System.IO.File.ReadAllLines(@"..\..\Data\2020\input17.txt").ToList();

            int cycles = 6;

            Dictionary<(int, int, int), char> space = new Dictionary<(int, int, int), char>();

            //Read input
            for (int i = 0; i < inputs.Count; i++)
            {
                for (int j = 0; j < inputs[i].Length; j++)
                {
                    space.Add((i, j, 0), inputs[i][j]);
                }
            }

            //Go through cycles
            for (int i = 0; i < cycles; i++)
            {
                //Find relevant nodes to go through
                HashSet<(int, int, int)> relevantNodes = new HashSet<(int, int, int)>();
                foreach (var cube in space)
                {
                    if (cube.Value == '#')
                    {
                        var neighbours = GetNeighbours3D(cube.Key);

                        foreach (var n in neighbours)
                        {
                            if (!relevantNodes.Contains(n))
                                relevantNodes.Add(n);
                        }
                    }
                }

                Dictionary<(int, int, int), char> tmpSpace = new Dictionary<(int, int, int), char>();
                foreach (var r in relevantNodes)
                {
                    int active = GetActiveNeighbours3D(r, space);

                    if (space.ContainsKey(r))
                    {
                        if (space[r] == '#')
                        {
                            if (active == 2 || active == 3)
                                tmpSpace.Add(r, '#');
                            else
                                tmpSpace.Add(r, '.');
                        }
                        else
                        {
                            if (active == 3)
                                tmpSpace.Add(r, '#');
                            else
                                tmpSpace.Add(r, '.');
                        }
                    }
                    else
                    {
                        if (active == 3)
                            tmpSpace.Add(r, '#');
                    }
                }

                space = tmpSpace;
            }

            return space.Count(x => x.Value == '#').ToString();
        }

        private int GetActiveNeighbours3D((int, int, int) cube, Dictionary<(int, int, int), char> space)
        {
            int active = 0;

            int x = cube.Item1;
            int y = cube.Item2;
            int z = cube.Item3;

            for (int i = x - 1; i <= x + 1; i++)
            {
                for (int j = y - 1; j <= y + 1; j++)
                {
                    for (int k = z - 1; k <= z + 1; k++)
                    {
                        if (i == x && j == y && k == z)
                            continue;
                        char c = '.';

                        if (space.TryGetValue((i, j, k), out c))
                            if (c == '#')
                                active++;
                    }
                }
            }

            return active;
        }

        private HashSet<(int, int, int)> GetNeighbours3D((int, int, int) cube)
        {
            HashSet<(int, int, int)> neighbours = new HashSet<(int, int, int)>();
            int x = cube.Item1;
            int y = cube.Item2;
            int z = cube.Item3;

            for (int i = x - 1; i <= x + 1; i++)
            {
                for (int j = y - 1; j <= y + 1; j++)
                {
                    for (int k = z - 1; k <= z + 1; k++)
                    {
                        if (i == x && j == y && k == z)
                            continue;
                        neighbours.Add((i, j, k));
                    }
                }
            }

            return neighbours;
        }

        public string RunTwo()
        {
            int cycles = 6;

            Dictionary<(int, int, int, int), char> space = new Dictionary<(int, int, int, int), char>();

            //Read input
            for (int i = 0; i < inputs.Count; i++)
            {
                for (int j = 0; j < inputs[i].Length; j++)
                {
                    space.Add((i, j, 0, 0), inputs[i][j]);
                }
            }

            //Go through cycles
            for (int i = 0; i < cycles; i++)
            {
                //Find relevant nodes to go through
                HashSet<(int, int, int, int)> relevantNodes = new HashSet<(int, int, int, int)>();
                foreach (var cube in space)
                {
                    if (cube.Value == '#')
                    {
                        var neighbours = GetNeighbours4D(cube.Key);

                        foreach (var n in neighbours)
                        {
                            if (!relevantNodes.Contains(n))
                                relevantNodes.Add(n);
                        }
                    }
                }

                Dictionary<(int, int, int, int), char> tmpSpace = new Dictionary<(int, int, int, int), char>();
                foreach (var r in relevantNodes)
                {
                    int active = GetActiveNeighbours4D(r, space);

                    if (space.ContainsKey(r))
                    {
                        if (space[r] == '#')
                        {
                            if (active == 2 || active == 3)
                                tmpSpace.Add(r, '#');
                            else
                                tmpSpace.Add(r, '.');
                        }
                        else
                        {
                            if (active == 3)
                                tmpSpace.Add(r, '#');
                            else
                                tmpSpace.Add(r, '.');
                        }
                    }
                    else
                    {
                        if (active == 3)
                            tmpSpace.Add(r, '#');
                    }
                }

                space = tmpSpace;
            }

            return space.Count(x => x.Value == '#').ToString();
        }

        private int GetActiveNeighbours4D((int, int, int, int) cube, Dictionary<(int, int, int, int), char> space)
        {
            int active = 0;

            int x = cube.Item1;
            int y = cube.Item2;
            int z = cube.Item3;
            int w = cube.Item4;

            for (int i = x - 1; i <= x + 1; i++)
            {
                for (int j = y - 1; j <= y + 1; j++)
                {
                    for (int k = z - 1; k <= z + 1; k++)
                    {
                        for (int l = w - 1; l <= w + 1; l++)
                        {
                            if (i == x && j == y && k == z && l == w)
                                continue;
                            char c = '.';

                            if (space.TryGetValue((i, j, k, l), out c))
                                if (c == '#')
                                    active++;
                        }
                    }
                }
            }

            return active;
        }

        private HashSet<(int, int, int, int)> GetNeighbours4D((int, int, int, int) cube)
        {
            HashSet<(int, int, int, int)> neighbours = new HashSet<(int, int, int, int)>();
            int x = cube.Item1;
            int y = cube.Item2;
            int z = cube.Item3;
            int w = cube.Item4;

            for (int i = x - 1; i <= x + 1; i++)
            {
                for (int j = y - 1; j <= y + 1; j++)
                {
                    for (int k = z - 1; k <= z + 1; k++)
                    {
                        for (int l = w - 1; l <= w + 1; l++)
                        {
                            if (i == x && j == y && k == z && l == w)
                                continue;
                            neighbours.Add((i, j, k, l));
                        }
                    }
                }
            }

            return neighbours;
        }
    }
}
