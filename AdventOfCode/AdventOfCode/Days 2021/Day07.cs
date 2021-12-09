using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Days_2021
{
    public class Day07 : IPuzzle
    {
        public bool Active => true;
        List<int> inputs;

        public string RunOne()
        {
            inputs = System.IO.File.ReadAllLines(@"..\..\Data\2021\input07.txt")[0].Split(',').Select(x => int.Parse(x)).ToList();

            inputs.Sort();


            int fuelUsed = 0;
            while (true)
            {
                int start = 0;
                int end = 0;

                int first = inputs[0];
                int last = inputs.Last();

                for (int i = 0; i < inputs.Count; i++)
                {
                    if (inputs[i] == first)
                        start++;
                }
                for (int i = inputs.Count - 1; i >= 0; i--)
                {
                    if (inputs[i] == last)
                        end++;
                }

                if (start == end && start == inputs.Count)
                    return fuelUsed.ToString();

                if (start < end)
                {
                    for (int i = 0; i < start; i++)
                    {
                        inputs[i]++;
                        fuelUsed++;
                    }
                }
                else
                {
                    for (int i = inputs.Count - 1; i > inputs.Count - 1 - end; i--)
                    {
                        inputs[i]--;
                        fuelUsed++;
                    }
                }
            }

            return "";
        }

        public string RunTwo()
        {
            inputs = System.IO.File.ReadAllLines(@"..\..\Data\2021\input07.txt")[0].Split(',').Select(x => int.Parse(x)).ToList();

            inputs.Sort();

            Dictionary<int, int> costs = new Dictionary<int, int>();

            for (int i = 0; i < inputs.Count; i++)
            {
                costs.Add(i, 1);
            }


            int fuelUsed = 0;
            while (true)
            {
                int start = 0;
                int end = 0;

                int first = inputs[0];
                int last = inputs.Last();

                for (int i = 0; i < inputs.Count; i++)
                {
                    if (inputs[i] == first)
                        start++;
                }
                for (int i = inputs.Count - 1; i >= 0; i--)
                {
                    if (inputs[i] == last)
                        end++;
                }

                if (start == end && start == inputs.Count)
                    return fuelUsed.ToString();

                int startCost = 0;
                int endCost = 0;


                for (int i = 0; i < start; i++)
                {
                    startCost += costs[i];
                }

                for (int i = inputs.Count - 1; i > inputs.Count - 1 - end; i--)
                {
                    endCost += costs[i];
                }


                if (startCost < endCost)
                {
                    for (int i = 0; i < start; i++)
                    {
                        inputs[i]++;
                        fuelUsed += costs[i];
                        costs[i]++;
                    }
                }
                else
                {
                    for (int i = inputs.Count - 1; i > inputs.Count - 1 - end; i--)
                    {
                        inputs[i]--;
                        fuelUsed += costs[i];
                        costs[i]++;
                    }
                }
            }

            return "";
        }
    }
}
