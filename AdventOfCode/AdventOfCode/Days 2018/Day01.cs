using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Days_2018
{
    public class Day01 : IPuzzle
    {
        public string RunOne()
        {
            return System.IO.File.ReadAllLines(@"..\..\Data\2018\input01.txt").Select(x => int.Parse(x)).Sum().ToString();
        }

        public string RunTwo()
        {
            List<int> numbers = System.IO.File.ReadAllLines(@"..\..\Data\2018\input01.txt").Select(x => int.Parse(x)).ToList();
            int sum = 0;
            HashSet<int> frequencies = new HashSet<int>();

            while (true)
            {
                foreach (var number in numbers)
                {
                    sum += number;

                    if (!frequencies.Add(sum))
                        return sum.ToString();
                }
            }
        }
    }
}
