using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Days_2020
{
    public class Day10 : IPuzzle
    {
        public bool Active => true;

        private List<int> inputs;

        public string RunOne()
        {
            inputs = System.IO.File.ReadAllLines(@"..\..\Data\2020\input10.txt").Select(x => int.Parse(x)).ToList();
            inputs.Add(0);
            inputs = inputs.OrderBy(x => x).ToList();
            inputs.Add(inputs.Last() + 3);

            int oneDifference = 0;
            int threeDifference = 0;

            for(int i = 0; i < inputs.Count - 1; i++)
            {
                if (inputs[i + 1] - inputs[i] == 1)
                    oneDifference++;
                if (inputs[i + 1] - inputs[i] == 3)
                    threeDifference++;
            }

            return (oneDifference * threeDifference).ToString();
        }

        public string RunTwo()
        {
            int repeating = 0;

            long result = 1;
            for (int i = 0; i < inputs.Count - 1; i++)
            {
                if (inputs[i + 1] - inputs[i] == 1)
                    repeating++;
                else
                {
                 if(repeating > 0)
                        result *= Utilities.Tribonacci(repeating);
                    repeating = 0;
                }
            }

            return result.ToString();
        }
    }
}
