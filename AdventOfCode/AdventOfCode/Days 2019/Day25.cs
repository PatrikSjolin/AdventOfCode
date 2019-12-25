using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Days_2019
{
    public class Day25 : IPuzzle
    {
        public bool Active => true;

        bool play = false;

        public string RunOne()
        {
            List<long> inputs = System.IO.File.ReadAllLines(@"..\..\Data\2019\input25.txt")[0].Split(',').Select(x => long.Parse(x)).ToList();

            for (int i = 0; i < 10000; i++)
            {
                inputs.Add(0);
            }
            if (play)
            {
                Computer c = new Computer(0);
                c.InputEvent += C_InputEvent;
                c.OutputEvent += C_OutputEvent;
                c.Compute(inputs.ToArray());
            }

            //Take Hypercube, Dehydrated water, Candy cane and Antenna
            return "2147502592";
        }

        private void C_OutputEvent(object sender, EventArgs e)
        {
            Computer c = ((Computer)sender);

            Console.Write((char)c.Output);
        }

        bool read = true;
        int i = 0;
        string input = "";

        private void C_InputEvent(object sender, EventArgs e)
        {
            Computer c = ((Computer)sender);

            if (read)
            {
                input = Console.ReadLine();
                input += "\n";
                i = 0;
                read = false;
                c.Input = input[i];
                i++;
            }
            else
            {
                c.Input = input[i];
                if (input[i] == 10)
                    read = true;
                i++;
            }
        }

        public string RunTwo()
        {
            return "All puzzles completed!";
        }
    }
}
