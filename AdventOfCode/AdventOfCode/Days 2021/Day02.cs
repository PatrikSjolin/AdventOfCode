using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Days_2021
{
    public class Day02 : IPuzzle
    {
        public bool Active => true;
        private List<string> inputs;

        public string RunOne()
        {

            inputs = System.IO.File.ReadAllLines(@"..\..\Data\2021\input02.txt").ToList();



            int depth = 0;
            int horizontal = 0;


            for(int i = 0; i < inputs.Count; i++)
            {
                if (inputs[i].StartsWith("forward"))
                    horizontal += int.Parse(inputs[i].Split(' ')[1]);
                if(inputs[i].StartsWith("down"))
                    depth += int.Parse(inputs[i].Split(' ')[1]);
                if(inputs[i].StartsWith("up"))
                    depth -= int.Parse(inputs[i].Split(' ')[1]);
            }

            return (horizontal * depth).ToString();
        }

        public string RunTwo()
        {
            int depth = 0;
            int horizontal = 0;
            int aim = 0; 


            for (int i = 0; i < inputs.Count; i++)
            {
                if (inputs[i].StartsWith("forward"))
                {
                    horizontal += int.Parse(inputs[i].Split(' ')[1]);
                    depth += aim * int.Parse(inputs[i].Split(' ')[1]);
                }
                if (inputs[i].StartsWith("down"))
                    aim += int.Parse(inputs[i].Split(' ')[1]);
                if (inputs[i].StartsWith("up"))
                    aim -= int.Parse(inputs[i].Split(' ')[1]);
            }

            return (horizontal * depth).ToString();
        }
    }
}
