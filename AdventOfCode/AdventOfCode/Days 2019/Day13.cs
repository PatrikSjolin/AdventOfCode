using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Days_2019
{
    public class Day13 : IPuzzle
    {
        public bool Active => false;

        Dictionary<Tuple<long, long>, long> gameBoard = new Dictionary<Tuple<long, long>, long>();

        int i = 0;

        public string RunOne()
        {
            List<long> inputs = System.IO.File.ReadAllLines(@"..\..\Data\2019\input13.txt")[0].Split(',').Select(x => long.Parse(x)).ToList();

            for (int i = 0; i < 1000; i++)
            {
                inputs.Add(0);
            }


            Computer c = new Computer(0);
            c.OutputEvent += C_OutputEvent;
            c.Compute(inputs.ToArray());

            int sum = 0;

            foreach(var tile in gameBoard)
            {
                if (tile.Value == 2)
                    sum++;
            }

            return sum.ToString();
        }

        long x = 0;
        long y = 0;

        private void C_OutputEvent(object sender, EventArgs e)
        {
            Computer c = ((Computer)sender);
            long output = c.Output;

            if(i % 3 == 2)
            {
                gameBoard.Add(new Tuple<long, long>(x, y), output);
            }
            else if(i % 3 == 1)
            {
                y = output;
            }
            else
            {
                x = output;
            }
            i++;
        }

        public string RunTwo()
        {
            return "";
        }
    }
}
