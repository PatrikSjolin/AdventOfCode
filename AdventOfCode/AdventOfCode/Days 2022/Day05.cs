using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Days_2022
{
    internal class Day05 : IPuzzle
    {
        public bool Active => true;
        private List<string> inputs;

        Dictionary<int, Stack<string>> cargo = new Dictionary<int, Stack<string>>();

        public string RunOne()
        {
            inputs = System.IO.File.ReadAllLines(@"..\..\Data\2022\input05.txt").ToList();

            int i = 0;
            for (i = 0; i < inputs.Count; i++)
            {
                if (inputs[i + 1] == "")
                {
                    //i+=2;
                    break;
                }
            }

            for(int j = i-1; j >= 0; j--)
            { 
                for(int k = 1; k < inputs[j].Length; k+=4)
                {
                    if (inputs[j][k] != ' ')
                    {
                        Stack<string> q;
                        if(cargo.TryGetValue((k / 4) + 1, out q))
                        {
                            q.Push(inputs[j][k].ToString());
                        }
                        else
                        {
                            cargo.Add((k / 4) + 1, q = new Stack<string>());
                            q.Push(inputs[j][k].ToString());
                        }
                    }
                }
            }
            i += 2;
            for(; i < inputs.Count; i++)
            {
                var split = inputs[i].Split(' ').ToList();

                int nMoves = int.Parse(split[1]);

                int from = int.Parse(split[3]);
                int to = int.Parse(split[5]);

                for(int j = 0; j < nMoves; j++)
                {
                    cargo[to].Push(cargo[from].Pop());
                }
            }

            string msg = "";

            for(i = 0; i < cargo.Count; i++)
            {
                msg += cargo[i + 1].First();
            }

            return msg;
        }

        public string RunTwo()
        {
            cargo.Clear();
            inputs = System.IO.File.ReadAllLines(@"..\..\Data\2022\input05.txt").ToList();

            int i = 0;
            for (i = 0; i < inputs.Count; i++)
            {
                if (inputs[i + 1] == "")
                {
                    //i+=2;
                    break;
                }
            }

            for (int j = i - 1; j >= 0; j--)
            {
                for (int k = 1; k < inputs[j].Length; k += 4)
                {
                    if (inputs[j][k] != ' ')
                    {
                        Stack<string> q;
                        if (cargo.TryGetValue((k / 4) + 1, out q))
                        {
                            q.Push(inputs[j][k].ToString());
                        }
                        else
                        {
                            cargo.Add((k / 4) + 1, q = new Stack<string>());
                            q.Push(inputs[j][k].ToString());
                        }
                    }
                }
            }
            i += 2;
            for (; i < inputs.Count; i++)
            {
                var split = inputs[i].Split(' ').ToList();

                int nMoves = int.Parse(split[1]);

                int from = int.Parse(split[3]);
                int to = int.Parse(split[5]);

                Stack<string> q = new Stack<string>();

                for (int j = nMoves - 1; j >= 0; j--)
                {
                    q.Push(cargo[from].Pop());
                }

                for(int j = 0; q.Count > 0; j++)
                {
                    cargo[to].Push(q.Pop());
                }
            }

            string msg = "";

            for (i = 0; i < cargo.Count; i++)
            {
                msg += cargo[i + 1].First();
            }

            return msg;
        }
    }
}
