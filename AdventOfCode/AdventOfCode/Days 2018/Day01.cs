using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Days_2018
{
    public class Day01 : IPuzzle
    {
        public string RunOne()
        {
            List<int> input = System.IO.File.ReadAllLines(@"..\..\Data\2018\input01.txt").Select(x => int.Parse(x)).ToList();
            int sum = 0;
            foreach (var number in input)
            {
                sum += number;
            }

            return sum.ToString();
        }

        public string RunTwo()
        {
            List<int> input = System.IO.File.ReadAllLines(@"..\..\Data\2018\input01.txt").Select(x => int.Parse(x)).ToList();
            int sum = 0;

            //List<int> frequencies = new List<int>();
            HashSet<int> frequencies = new HashSet<int>();

            while (true)
            {
                foreach (var number in input)
                {
                    sum += number;

                    if (frequencies.Contains(sum))
                        return sum.ToString();
                    frequencies.Add(sum);
                }
            }
        }
    }
}
