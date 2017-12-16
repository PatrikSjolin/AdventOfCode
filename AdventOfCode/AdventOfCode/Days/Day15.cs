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

            List<long> valuesA = new List<long>();
            List<long> valuesB = new List<long>();

            int numberOfRuns = 5000000;

            for (int i = 0; valuesA.Count < numberOfRuns || valuesB.Count < numberOfRuns; i++)
            {
                if (valuesA.Count < numberOfRuns)
                {
                    resultA = resultA * genAFactor % div;
                    if (resultA % 4 == 0)
                    {
                        valuesA.Add(resultA);
                    }
                }

                if (valuesB.Count < numberOfRuns)
                {
                    resultB = resultB * genBFactor % div;
                    if (resultB % 8 == 0)
                    {
                        valuesB.Add(resultB);
                    }
                }
            }

            for (int i = 0; i < numberOfRuns; i++)
            {
                long modA = valuesA[i] % 65536;
                long modB = valuesB[i] % 65536;

                if (modA == modB)
                    pairs++;
            }

            return pairs.ToString();
        }
    }
}

