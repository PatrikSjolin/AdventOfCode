using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Days_2019
{
    public class Day10 : IPuzzle
    {
        public bool Active => true;

        public string RunOne()
        {
            List<string> inputs = System.IO.File.ReadAllLines(@"..\..\Data\2019\input10test1.txt").ToList();

            Dictionary<Tuple<int, int>, int> astroids = new Dictionary<Tuple<int, int>, int>();

            for(int j = 0; j < inputs.Count; j++)
            {
                for(int i = 0; i < inputs[j].Length; i++)
                {
                    if(inputs[j][i] == '#')
                    {
                        astroids.Add(new Tuple<int, int>(i, j), 0);
                    }
                }
            }

            foreach(var a in astroids)
            {
                int canSee = 0;
                foreach(var b in astroids)
                {
                    if(!a.Key.Equals(b.Key))
                        canSee += IsVisible(a.Key, b.Key, astroids.Keys.ToList());
                }
            }

            int max = int.MinValue;
            Tuple<int, int> bestAstroids = null;

            foreach(var a in astroids)
            {
                if (a.Value > max)
                {
                    max = a.Value;
                    bestAstroids = a.Key;
                }
            }

            return max.ToString();
        }

        private int IsVisible(Tuple<int, int> key1, Tuple<int, int> key2, List<Tuple<int, int>> astroids)
        {
            float x = key2.Item1 - key1.Item1;
            float y = key2.Item2 - key1.Item2;

            y = y / Math.Abs(x);
            x = Math.Abs(x) / x;

            float newX = key1.Item1;
            float newY = key1.Item2;

            while (true)
            {
                newX += x;
                newY += y;

                if(astroids.Contains(new Tuple<int, int>((int)newX, (int)newY)))
                {
                    if (newX == key2.Item1 && newY == key2.Item2)
                        return 1;
                    else
                        return 0;
                }
            }

            return 0;
        }

        public string RunTwo()
        {
            throw new NotImplementedException();
        }
    }
}
