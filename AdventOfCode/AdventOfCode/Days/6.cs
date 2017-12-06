using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Days
{
    public class State
    {
        public List<int> Memory { get; set; }

        public bool Equals(State s)
        {
            for(int i = 0; i < 16; i++)
            {
                if (s.Memory[i] != Memory[i])
                    return false;
            }
            return true;
        }
    }

    public class _6 : IPuzzle
    {
        public void RunOne()
        {
            List<State> states = new List<State>();
            List<string> inputLines = System.IO.File.ReadAllLines(@"..\..\input6.txt").ToList();
            List<int> split = inputLines[0].Split('\t').Select(x => int.Parse(x)).ToList();

            int count = 0;

            while (true)
            {
                for (int i = 0; i < states.Count; i++)
                {
                    if (states[i].Equals(new State() { Memory = split.ToList() }))
                    {
                        System.Diagnostics.Debugger.Break();
                    }
                }

                states.Add(new State { Memory = split.ToList() });


                int max = split.Max();

                int indexOfMax = split.IndexOf(max);

                split[indexOfMax] = 0;

                for(int i = indexOfMax+1; i < (max+indexOfMax+1); i++)
                {
                    split[i % 16]++;
                }
                
            }
        }

        public void RunTwo()
        {
        }
    }
}
