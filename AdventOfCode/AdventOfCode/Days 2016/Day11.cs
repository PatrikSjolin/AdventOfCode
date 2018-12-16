using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Days_2016
{
    public class Day11 : IPuzzle
    {
        private int taskNumber = 11;

        public bool Active => false;

        public void GoOne()
        {
            Console.WriteLine(GetMovesForObjects(5));
        }

        public void GoTwo()
        {
            Console.WriteLine(GetMovesForObjects(7));
        }

        public string RunOne()
        {
            throw new NotImplementedException();
        }

        public string RunTwo()
        {
            throw new NotImplementedException();
        }

        private int GetMovesForObjects(int numberOfObjects)
        {
            return (numberOfObjects - 2) * 12 + 11;
        }
    }
}
