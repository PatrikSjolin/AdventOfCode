using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Days_2019
{
    class Day02 : IPuzzle
    {
        public bool Active => true;

        public string RunOne()
        {
            List<int> input = System.IO.File.ReadAllLines(@"..\..\Data\2019\input02.txt")[0].Split(',').Select(x => int.Parse(x)).ToList();
            input[1] = 12;
            input[2] = 2;
            for (int i = 0; i < input.Count; i += 4)
            {
                if (input[i] == 1)
                {
                    input[input[i + 3]] = input[input[i + 1]] + input[input[i + 2]];
                }
                else if (input[i] == 2)
                {
                    input[input[i + 3]] = input[input[i + 1]] * input[input[i + 2]];
                }
                else if (input[i] == 99)
                {
                    return input[0].ToString();
                }
            }

            return "";
        }

        public string RunTwo()
        {
            List<int> startInput = System.IO.File.ReadAllLines(@"..\..\Data\2019\input02.txt")[0].Split(',').Select(x => int.Parse(x)).ToList();
            for (int j = 0; j <= 99; j++)
            {
                for (int k = 0; k <= 99; k++)
                {
                    List<int> input = startInput.ToList();
                    input[1] = j;
                    input[2] = k;
                    for (int i = 0; i < input.Count; i += 4)
                    {
                        if (input[i] == 1)
                        {
                            input[input[i + 3]] = input[input[i + 1]] + input[input[i + 2]];
                        }
                        else if (input[i] == 2)
                        {
                            input[input[i + 3]] = input[input[i + 1]] * input[input[i + 2]];
                        }
                        else if (input[i] == 99)
                        {
                            if (input[0] == 19690720)
                                return (100 * j + k).ToString();
                        }
                    }
                }
            }

            return "";
        }
    }
}
