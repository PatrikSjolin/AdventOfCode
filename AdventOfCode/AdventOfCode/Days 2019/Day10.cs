using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Days_2019
{
    public class Day10 : IPuzzle
    {
        public bool Active => true;

        public string RunOne()
        {
            List<string> inputs = System.IO.File.ReadAllLines(@"..\..\Data\2019\input10.txt").ToList();

            Dictionary<Tuple<int, int>, int> astroidVisibles = new Dictionary<Tuple<int, int>, int>();
            HashSet<Tuple<int, int>> astroids = new HashSet<Tuple<int, int>>();

            for (int j = 0; j < inputs.Count; j++)
            {
                for (int i = 0; i < inputs[j].Length; i++)
                {
                    if (inputs[j][i] == '#')
                    {
                        astroids.Add(new Tuple<int, int>(i, j));
                    }
                }
            }

            foreach (var a in astroids)
            {
                int canSee = 0;
                foreach (var b in astroids)
                {
                    if (!a.Equals(b))
                        canSee += IsVisible(a, b, astroids);
                }
                astroidVisibles.Add(a, canSee);
            }

            int max = int.MinValue;

            foreach (var a in astroidVisibles)
            {
                if (a.Value > max)
                {
                    max = a.Value;
                }
            }

            return max.ToString();
        }

        public string RunTwo()
        {
            List<string> inputs = System.IO.File.ReadAllLines(@"..\..\Data\2019\input10.txt").ToList();

            Dictionary<Tuple<int, int>, int> astroidVisibles = new Dictionary<Tuple<int, int>, int>();
            HashSet<Tuple<int, int>> astroids = new HashSet<Tuple<int, int>>();

            for (int j = 0; j < inputs.Count; j++)
            {
                for (int i = 0; i < inputs[j].Length; i++)
                {
                    if (inputs[j][i] == '#')
                    {
                        astroids.Add(new Tuple<int, int>(i, j));
                    }
                }
            }

            foreach (var a in astroids)
            {
                int canSee = 0;
                foreach (var b in astroids)
                {
                    if (!a.Equals(b))
                        canSee += IsVisible(a, b, astroids);
                }
                astroidVisibles.Add(a, canSee);
            }

            int max = int.MinValue;
            Tuple<int, int> bestAstroid = null;

            foreach (var a in astroidVisibles)
            {
                if (a.Value > max)
                {
                    max = a.Value;
                    bestAstroid = a.Key;
                }
            }

            int destroyed = 0;

            while (true)
            {
                List<Tuple<int, int>> visible = new List<Tuple<int, int>>();
                foreach (var a in astroidVisibles.Keys.ToList())
                {
                    if (!a.Equals(bestAstroid))
                    {
                        if (IsVisible(a, bestAstroid, astroids) == 1)
                        {
                            visible.Add(a);
                        }
                    }
                }

                if (destroyed + visible.Count >= 200)
                {
                    List<Tuple<int, int>> positiveX = visible.Where(x => x.Item1 >= bestAstroid.Item1).ToList();
                    List<Tuple<int, int>> negativeX = visible.Where(x => !positiveX.Contains(x)).ToList();

                    positiveX = positiveX.OrderByDescending(x => ((double)bestAstroid.Item2 - x.Item2) / (x.Item1 - (double)bestAstroid.Item1)).ToList();
                    negativeX = negativeX.OrderByDescending(x => ((double)bestAstroid.Item2 - x.Item2) / (x.Item1 - (double)bestAstroid.Item1)).ToList();

                    positiveX.AddRange(negativeX);

                    visible = positiveX;
                }

                for (int i = 0; i < visible.Count; i++)
                {
                    astroidVisibles.Remove(visible[i]);
                    destroyed++;

                    if (destroyed == 200)
                    {
                        return ((visible[i].Item1 * 100) + visible[i].Item2).ToString();
                    }
                }
            }
        }

        private static int GCD(int a, int b)
        {
            while (a != 0 && b != 0)
            {
                if (a > b)
                    a %= b;
                else
                    b %= a;
            }

            return a == 0 ? b : a;
        }

        private int IsVisible(Tuple<int, int> key1, Tuple<int, int> key2, HashSet<Tuple<int, int>> astroids)
        {
            int x = key2.Item1 - key1.Item1;
            int y = key2.Item2 - key1.Item2;

            int div = GCD(Math.Abs(x), Math.Abs(y));

            y = y / div;
            x = x / div;

            int newX = key1.Item1;
            int newY = key1.Item2;

            while (true)
            {
                newX += x;
                newY += y;

                if (astroids.Contains(new Tuple<int, int>(newX, newY)))
                {
                    if (newX == key2.Item1 && newY == key2.Item2)
                        return 1;
                    else
                        return 0;
                }
            }
        }
    }
}
