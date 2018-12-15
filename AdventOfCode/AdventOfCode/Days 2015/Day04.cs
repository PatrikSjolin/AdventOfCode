using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Days_2015
{
    public class Day04 : IPuzzle
    {
        public bool Active { get => true; }

        public string RunOne()
        {
            string input = "ckczppom";
            int count = 0;
            while(!Utilities.CalculateMD5Hash(input + count).StartsWith("00000"))
            {
                count++;
            }

            return count.ToString();
        }

        public string RunTwo()
        {
            string input = "ckczppom";
            int count = 0;
            while (!Utilities.CalculateMD5Hash(input + count).StartsWith("000000"))
            {
                count++;
            }

            return count.ToString();
        }
    }
}
