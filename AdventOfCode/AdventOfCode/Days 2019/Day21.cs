using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Days_2019
{
    public class Day21 : IPuzzle
    {
        public bool Active => true;

        public string RunOne()
        {
            List<long> inputs = System.IO.File.ReadAllLines(@"..\..\Data\2019\input21.txt")[0].Split(',').Select(x => long.Parse(x)).ToList();
            //i = 0;
            for (int i = 0; i < 10000; i++)
            {
                inputs.Add(0);
            }


            Computer c = new Computer(0);
            c.OutputEvent += C_OutputEvent;
            c.InputEvent += C_InputEvent;
            c.Compute(inputs.ToArray());

            return c.Output.ToString();
        }

        int[] incstructionsA = new int[]
        {            
            'O', 'R', ' ', 'A', ' ', 'T', 10,
            'O', 'R', ' ', 'B', ' ', 'T', 10,
            'O', 'R', ' ', 'C', ' ', 'T', 10,
            'A', 'N', 'D', ' ', 'A', ' ', 'T', 10,
            'A', 'N', 'D', ' ', 'B', ' ', 'T', 10,
            'A', 'N', 'D', ' ', 'C', ' ', 'T', 10,
            'N', 'O', 'T', ' ', 'T', ' ', 'J', 10,
            'A', 'N', 'D', ' ', 'D', ' ', 'J', 10,
            'W', 'A', 'L', 'K', 10
        };

        int i = 0;

        bool visualize = true;

        private void C_InputEvent(object sender, EventArgs e)
        {
            Computer c = ((Computer)sender);
            c.Input = incstructionsA[i++];
        }

        private void C_OutputEvent(object sender, EventArgs e)
        {
            Computer c = ((Computer)sender);
            if(visualize)
                Console.Write((char)c.Output);
        }

        int[] incstructionsB = new int[]
        {           
            'O', 'R', ' ', 'A', ' ', 'T', 10,
            'O', 'R', ' ', 'B', ' ', 'T', 10,
            'O', 'R', ' ', 'C', ' ', 'T', 10,
            'A', 'N', 'D', ' ', 'A', ' ', 'T', 10,
            'A', 'N', 'D', ' ', 'B', ' ', 'T', 10,
            'A', 'N', 'D', ' ', 'C', ' ', 'T', 10,
            'N', 'O', 'T', ' ', 'T', ' ', 'J', 10,
            'O', 'R', ' ', 'E', ' ', 'T', 10,
            'O', 'R', ' ', 'H', ' ', 'T', 10,
            'A', 'N', 'D', ' ', 'T', ' ', 'J', 10,
            'A', 'N', 'D', ' ', 'D', ' ', 'J', 10,
            'R', 'U', 'N', 10
        };

        public string RunTwo()
        {
            List<long> inputs = System.IO.File.ReadAllLines(@"..\..\Data\2019\input21.txt")[0].Split(',').Select(x => long.Parse(x)).ToList();
            i = 0;

            for (int i = 0; i < 10000; i++)
            {
                inputs.Add(0);
            }


            Computer c = new Computer(0);
            c.OutputEvent += C_OutputEvent;
            c.InputEvent += C_InputEvent1; ;
            c.Compute(inputs.ToArray());

            return c.Output.ToString();
        }

        private void C_InputEvent1(object sender, EventArgs e)
        {
            Computer c = ((Computer)sender);
            c.Input = incstructionsB[i++];
        }
    }
}
