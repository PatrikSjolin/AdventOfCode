using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Days_2021
{
    public class Day06 : IPuzzle
    {
        public bool Active => true;
        List<int> inputs;

        public string RunOne()
        {
            inputs = System.IO.File.ReadAllLines(@"..\..\Data\2021\input06.txt")[0].Split(',').Select(x => int.Parse(x)).ToList();

            Dictionary<int, int> lanterns = new Dictionary<int, int>();
            for (int i = 0; i <= 8; i++)
            {
                lanterns.Add(i, 0);
            }
            for (int i = 0; i < inputs.Count; i++)
            {
                lanterns[inputs[i]]++;
            }

            Dictionary<int, int> newLanterns = new Dictionary<int, int>();

            for (int i = 0; i < 80; i++)
            {
                newLanterns.Clear();
                for (int j = 0; j < 8; j++)
                {
                    newLanterns[j] = lanterns[j + 1];
                }

                newLanterns[6] += lanterns[0];
                newLanterns[8] = lanterns[0];
                lanterns = new Dictionary<int, int>(newLanterns);
            }

            long sum = 0;
            for (int i = 0; i < lanterns.Count; i++)
                sum += lanterns[i];

            return sum.ToString();
        }

        public string RunTwo()
        {
            Dictionary<long, long> lanterns = new Dictionary<long, long>();
            for (int i = 0; i <= 8; i++)
            {
                lanterns.Add(i, 0);
            }
            for (int i = 0; i < inputs.Count; i++)
            {
                lanterns[inputs[i]]++;
            }

            Dictionary<long, long> newLanterns = new Dictionary<long, long>();

            for (int i = 0; i < 256; i++)
            {
                newLanterns.Clear();
                for (int j = 0; j < 8; j++)
                {
                    newLanterns[j] = lanterns[j + 1];
                }

                newLanterns[6] += lanterns[0];
                newLanterns[8] = lanterns[0];
                lanterns = new Dictionary<long, long>(newLanterns);
            }

            long sum = 0;
            for (int i = 0; i < lanterns.Count; i++)
                sum += lanterns[i];

            return sum.ToString();
        }
    }
}
