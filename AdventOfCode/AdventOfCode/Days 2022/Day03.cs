using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Days_2022
{
    internal class Day03 : IPuzzle
    {
        public bool Active => true;
        private List<string> inputs;

        private int GetPriority(string first, string second)
        {
            for (int j = 0; j < first.Length; j++)
            {
                for (int k = 0; k < second.Length; k++)
                {
                    if (first[j] == second[k])
                    {
                        if (first[j] > 90)
                            return first[j] - 96;
                        else
                            return first[j] - 38;
                    }
                }
            }

            return 0;
        }

        public string RunOne()
        {
            inputs = System.IO.File.ReadAllLines(@"..\..\Data\2022\input03.txt").ToList();

            int sum = 0;

            for(int i = 0; i < inputs.Count; i++)
            {
                string first = inputs[i].Substring(0, inputs[i].Length / 2);
                string second = inputs[i].Substring(inputs[i].Length / 2);

                sum += GetPriority(first, second);
            }

            return sum.ToString();
        }

        public string RunTwo()
        {
            inputs = System.IO.File.ReadAllLines(@"..\..\Data\2022\input03.txt").ToList();

            int sum = 0;


            for (int i = 0; i < inputs.Count; i+=3)
            {
                string first = inputs[i];
                string second = inputs[i+1];
                string third = inputs[i+2];

                sum += GetBadgeScore(first, second, third);
            }

            return sum.ToString();
        }

        private int GetBadgeScore(string first, string second, string third)
        {
            for (int j = 0; j < first.Length; j++)
            {
                for (int k = 0; k < second.Length; k++)
                {
                    for (int l = 0; l < third.Length; l++)
                    {
                        if ((first[j] == second[k]) && (first[j] == third[l]))
                        {
                            if (first[j] > 90)
                                return first[j] - 96;
                            else
                                return first[j] - 38;
                        }
                    }
                }
            }

            return 0;
        }
    }
}
