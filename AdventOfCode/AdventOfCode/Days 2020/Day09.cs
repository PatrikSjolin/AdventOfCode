using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Days_2020
{
    public class Day09 : IPuzzle
    {
        public bool Active => true;

        long invalidNumber = 0;
        private List<long> inputs;

        public string RunOne()
        {
            inputs = System.IO.File.ReadAllLines(@"..\..\Data\2020\input09.txt").Select(x => long.Parse(x)).ToList();
            int preamble = 25;

            for (int i = preamble; i < inputs.Count; i++)
            {
                long sum = inputs[i];
                bool success = IsNumberValid(preamble, i, sum);
                if (!success)
                {
                    invalidNumber = sum;
                    return inputs[i].ToString();
                }
            }

            return "";
        }

        private bool IsNumberValid(int preamble, int i, long sum)
        {
            for (int j = i - preamble; j < i; j++)
            {
                for (int k = j + 1; k < i; k++)
                {
                    if (j == k)
                        continue;
                    if (inputs[j] == inputs[k])
                        continue;
                    if (inputs[j] + inputs[k] == sum)
                        return true;
                }
            }

            return false;
        }

        public string RunTwo()
        {
            for (int i = 0; i < inputs.Count; i++)
            {
                long sum = inputs[i];
                long min = sum;
                long max = sum;

                for (int j = i + 1; j < inputs.Count; j++)
                {
                    if (inputs[j] < min)
                        min = inputs[j];
                    if (inputs[j] > max)
                        max = inputs[j];

                    sum += inputs[j];

                    if (sum > invalidNumber)
                        break;

                    if (sum == invalidNumber)
                        return (min + max).ToString();
                }
            }

            return "";
        }
    }
}
