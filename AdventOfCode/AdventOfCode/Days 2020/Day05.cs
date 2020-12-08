using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Days_2020
{
    public class Day05 : IPuzzle
    {
        public bool Active => true;

        List<int> seats = null;

        public string RunOne()
        {
            List<string> inputs = System.IO.File.ReadAllLines(@"..\..\Data\2020\input05.txt").ToList();

            seats = new List<int>();

            foreach(var input in inputs)
            {
                int lower = 0;
                int upper = 127;
                for(int i = 0; i <= 6; i++)
                {
                    if(input[i] == 'F')
                    {
                        upper = upper - ((upper - lower + 1) / 2);
                    }
                    else
                    {
                        lower = lower + ((upper - lower + 1) / 2);
                    }
                }

                int row = lower;
                lower = 0;
                upper = 7;
                for(int i = 7; i < input.Length; i++)
                {
                    if (input[i] == 'L')
                    {
                        upper = upper - ((upper - lower + 1) / 2);
                    }
                    else
                    {
                        lower = lower + ((upper - lower + 1) / 2);
                    }
                }

                int column = lower;

                int seatId = row * 8 + column;
                seats.Add(seatId);

            }


            return seats.Max().ToString();
        }

        public string RunTwo()
        {
            int lowest = seats.Min();

            for(int i = lowest + 8; ; i++)
            {
                if (!seats.Contains(i))
                    return i.ToString();
            }
        }
    }
}
