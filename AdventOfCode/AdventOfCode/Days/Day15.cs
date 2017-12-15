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

                string binaryA = GetIntBinaryStringRemoveZeros(resultA);
                string binaryB = GetIntBinaryStringRemoveZeros(resultB);

                if (binaryA.Length >= 16 && binaryB.Length >= 16)
                {
                    string binAcomp = binaryA.Substring(binaryA.Length - 16);
                    string binBcomp = binaryB.Substring(binaryB.Length - 16);

                    if (binAcomp == binBcomp)
                    {
                        pairs++;
                    }
                }
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
                string binaryA = GetIntBinaryStringRemoveZeros(valuesA[comp.Key]);
                string binaryB = GetIntBinaryStringRemoveZeros(comp.Value);

                if (binaryA.Length >= 16 && binaryB.Length >= 16)
                {
                    string binAcomp = binaryA.Substring(binaryA.Length - 16);
                    string binBcomp = binaryB.Substring(binaryB.Length - 16);

                    if (binAcomp == binBcomp)
                    {
                        pairs++;
                    }
                }
            }

            return pairs.ToString();
        }

        private static string GetIntBinaryStringRemoveZeros(long n)
        {
            char[] b = new char[32];
            int pos = 31;
            int i = 0;

            while (i < 32)
            {
                if ((n & (1 << i)) != 0)
                {
                    b[pos] = '1';
                }
                else
                {
                    b[pos] = '0';
                }
                pos--;
                i++;
            }
            return new string(b).TrimStart('0');
        }
    }
}

