using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Days_2016
{
    public class Day15 : IPuzzle
    {
        public bool Active => false;

        public void GoOne()
        {

            int solution = 0;

            while (true)
            {
                if((5 + solution) % 17 == 0 &&
                   (8 + solution + 1) % 19 == 0 &&
                   (1 + solution + 2) % 7 == 0 &&
                   (7 + solution + 3) % 13 == 0 &&
                   (1 + solution + 4) % 5 == 0 &&
                   (0 + solution + 5) % 3 == 0)
                {
                    break;
                }

                solution++;
            }
            Console.WriteLine(solution - 1);

            //Disc #1 has 17 positions; at time=0, it is at position 5.
            //Disc #2 has 19 positions; at time=0, it is at position 8.
            //Disc #3 has 7 positions; at time=0, it is at position 1.
            //Disc #4 has 13 positions; at time=0, it is at position 7.
            //Disc #5 has 5 positions; at time=0, it is at position 1.
            //Disc #6 has 3 positions; at time=0, it is at position 0.


            //Disc #1 has 5 positions; at time=0, it is at position 4.
            //Disc #2 has 2 positions; at time=0, it is at position 1.
        }

        public void GoTwo()
        {
            int solution = 0;

            while (true)
            {
                if ((5 + solution) % 17 == 0 &&
                   (8 + solution + 1) % 19 == 0 &&
                   (1 + solution + 2) % 7 == 0 &&
                   (7 + solution + 3) % 13 == 0 &&
                   (1 + solution + 4) % 5 == 0 &&
                   (0 + solution + 5) % 3 == 0 &&
                   (0 + solution + 6) % 11 == 0)
                {
                    break;
                }

                solution++;
            }
            Console.WriteLine(solution - 1);
        }

        public string RunOne()
        {
            throw new NotImplementedException();
        }

        public string RunTwo()
        {
            throw new NotImplementedException();
        }
    }
}
