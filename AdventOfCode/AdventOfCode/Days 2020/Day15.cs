using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Days_2020
{
    public class Day15 : IPuzzle
    {
        public bool Active => true;

        private List<int> inputs;

        public string RunOne()
        {
            string input = "14,3,1,0,9,5";
            inputs = input.Split(',').Select(x => int.Parse(x)).ToList();

            int number = 2020;
            int spoken = FindNthSpokenNumber(number);
            return spoken.ToString();
        }

        public string RunTwo()
        {
            int number = 30000000;
            int spoken = FindNthSpokenNumber(number);
            return spoken.ToString();
        }

        private int FindNthSpokenNumber(int number)
        {
            Dictionary<int, (int, int)> spokenNumbers = new Dictionary<int, (int, int)>();

            bool newNumber = false;

            int spoken = 0;
            for (int i = 0; i < number; i++)
            {
                if (i < inputs.Count)
                    spoken = inputs[i];
                else
                {
                    if (newNumber)
                    {
                        spoken = 0;
                    }
                    else
                    {
                        spoken = spokenNumbers[spoken].Item2 - spokenNumbers[spoken].Item1;
                    }
                }
                (int, int) numbers;
                if (spokenNumbers.TryGetValue(spoken, out numbers))
                {
                    newNumber = false;
                    spokenNumbers[spoken] = (numbers.Item2, i);
                }
                else
                {
                    newNumber = true;
                    spokenNumbers.Add(spoken, (0, i));
                }
            }

            return spoken;
        }
    }
}
