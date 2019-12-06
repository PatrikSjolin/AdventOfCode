using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Days_2019
{
    public class Day05 : IPuzzle
    {
        public bool Active => false;

        public string RunOne()
        {
            List<int> inputs = System.IO.File.ReadAllLines(@"..\..\Data\2019\input05.txt")[0].Split(',').Select(x => int.Parse(x)).ToList();

            int input = 1;

            for (int i = 0; i < inputs.Count;)
            {
                int op = 0;
                int par1 = 0;
                int par2 = 0;
                string start = inputs[i].ToString();

                if(start.Length == 1)
                {
                    op = inputs[i];
                }
                else if(start.Length == 4)
                {
                    op = (int)char.GetNumericValue(start[3]);
                    par1 = (int)char.GetNumericValue(start[1]);
                    par2 = (int)char.GetNumericValue(start[0]);
                }
                else if(start.Length == 3)
                {
                    op = (int)char.GetNumericValue(start[2]);
                    par1 = (int)char.GetNumericValue(start[0]);
                }
                else if(start.Length == 5)
                {

                }
                else
                {
                    break;
                }

                if (op == 1)
                {
                    inputs[inputs[i + 3]] = GetValue(inputs, i + 1, par1) + GetValue(inputs, i + 2, par2);
                    i += 4;
                }
                else if (op == 2)
                {
                    inputs[inputs[i + 3]] = GetValue(inputs, i + 1, par1) * GetValue(inputs, i + 2, par2);
                    i += 4;
                }
                else if (op == 3)
                {
                    inputs[inputs[i + 1]] = input;
                    i += 2;
                }
                else if (op == 4)
                {
                    Console.WriteLine(inputs[inputs[i + 1]].ToString());
                    i += 2;
                }
                else if (op == 99)
                {
                    return inputs[0].ToString();
                }
                else
                {

                }
            }

            return "";
        }

        private int GetValue(List<int> inputs, int v, int par)
        {
            if (par == 0)
                return inputs[inputs[v]];
            else
                return inputs[v];
        }

        public string RunTwo()
        {
            List<int> inputs = System.IO.File.ReadAllLines(@"..\..\Data\2019\input05.txt")[0].Split(',').Select(x => int.Parse(x)).ToList();
            int input = 5;

            for (int i = 0; i < inputs.Count;)
            {
                string start = inputs[i].ToString();
                int op;
                int par1 = 0;
                int par2 = 0;
                int par3 = 0;

                if (start.Length == 1)
                {
                    op = inputs[i];
                }
                else if (start.Length == 4)
                {
                    op = (int)char.GetNumericValue(start[3]);
                    par1 = (int)char.GetNumericValue(start[1]);
                    par2 = (int)char.GetNumericValue(start[0]);

                }
                else if (start.Length == 3)
                {
                    op = (int)char.GetNumericValue(start[2]);
                    par1 = (int)char.GetNumericValue(start[0]);
                }
                else if (start.Length == 5)
                {
                    op = (int)char.GetNumericValue(start[4]);
                    par1 = (int)char.GetNumericValue(start[2]);
                    par2 = (int)char.GetNumericValue(start[1]);
                    par3 = (int)char.GetNumericValue(start[0]);
                }
                else
                {
                    op = 0;
                }



                if (op == 1)
                {
                    inputs[inputs[i + 3]] = GetValue(inputs, i + 1, par1) + GetValue(inputs, i + 2, par2);
                    i += 4;
                }
                else if (op == 2)
                {
                    inputs[inputs[i + 3]] = GetValue(inputs, i + 1, par1) * GetValue(inputs, i + 2, par2);
                    i += 4;
                }
                else if (op == 3)
                {
                    inputs[inputs[i + 1]] = input;
                    i += 2;
                }
                else if (op == 4)
                {
                    Console.WriteLine(inputs[inputs[i + 1]].ToString());
                    i += 2;
                }
                else if (op == 5)
                {
                    if (GetValue(inputs, i + 1, par1) != 0)
                    {
                        i = GetValue(inputs, i + 2, par2);
                    }
                    else
                    {
                        i += 3;
                    }
                }
                else if (op == 6)
                {
                    if (GetValue(inputs, i + 1, par1) == 0)
                    {
                        i = GetValue(inputs, i + 2, par2);
                    }
                    else
                    {
                        i += 3;
                    }
                }
                else if (op == 7)
                {
                    if (GetValue(inputs, i + 1, par1) < GetValue(inputs, i + 2, par2))
                    {
                        inputs[inputs[i + 3]] = 1;
                    }
                    else
                    {
                        inputs[inputs[i + 3]] = 0;
                    }
                    i += 4;
                }
                else if (op == 8)
                {
                    if (GetValue(inputs, i + 1, par1) == GetValue(inputs, i + 2, par2))
                    {
                        inputs[inputs[i + 3]] = 1;
                    }
                    else
                    {
                        inputs[inputs[i + 3]] = 0;
                    }
                    i += 4;
                }
                else if (op == 99)
                {
                    return inputs[0].ToString();
                }
                else
                {

                }
            }


            return "";

        }
    }
}
