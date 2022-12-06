using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Days_2022
{
    internal class Day01 : IPuzzle
    {
        public bool Active => true;

        private List<string> inputs;
        List<int> inventories = new List<int>();

        public string RunOne()
        {
            inputs = System.IO.File.ReadAllLines(@"..\..\Data\2022\input01.txt").ToList();

            int max = 0;

            int sum = 0;

            for(int i = 0; i < inputs.Count; i++)
            {
                if (inputs[i] == "")
                {
                    if(sum > max)
                    {
                        max = sum;
                    }
                    inventories.Add(sum);
                    sum = 0;
                }
                else
                {
                    sum += int.Parse(inputs[i]);
                }
            }

            return max.ToString();
        }

        public string RunTwo()
        {
            var a = inventories.OrderByDescending(x => x).ToList();
            return (a[0] + a[1] + a[2]).ToString();
        }
    }
}
