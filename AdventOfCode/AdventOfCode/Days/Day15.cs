using System.Collections.Generic;

namespace AdventOfCode.Days
{
    public class Day15 : IPuzzle
    {
        public string RunOne()
        {
            int genAFactor = 16807;
            int genBFactor = 48271;

            int div = 2147483647;

            long resultA = 883;
            long resultB = 879;

            int pairs = 0;

            for (int i = 0; i < 40000000; i++)
            {
                resultA = resultA * genAFactor % div;
                resultB = resultB * genBFactor % div;

                long modA = resultA % 65536;
                long modB = resultB % 65536;

                if (modA == modB)
                    pairs++;
            }

            return pairs.ToString();
        }

        public string RunTwo()
        {
            int genAFactor = 16807;
            int genBFactor = 48271;

            int div = 2147483647;

            long resultA = 883;
            long resultB = 879;

            int pairs = 0;

            Dictionary<int, long> valuesA = new Dictionary<int, long>();
            Dictionary<int, long> valuesB = new Dictionary<int, long>();
            int countA = 0;
            int countB = 0;

            for (int i = 0; valuesA.Count <= 5000000 || valuesB.Count <= 5000000; i++)
            {
                resultA = resultA * genAFactor % div;
                resultB = resultB * genBFactor % div;

                if (resultA % 4 == 0)
                {
                    valuesA.Add(countA, resultA);
                    countA++;
                }
                if (resultB % 8 == 0)
                {
                    valuesB.Add(countB, resultB);
                    countB++;
                }
            }

            foreach (var comp in valuesB)
            {
                long modA = valuesA[comp.Key] % 65536;
                long modB = comp.Value % 65536;

                if (modA == modB)
                    pairs++;
            }

            return pairs.ToString();
        }
    }
}

