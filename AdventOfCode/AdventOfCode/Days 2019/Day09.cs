using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AdventOfCode.Days_2019.Day07;

namespace AdventOfCode.Days_2019
{
    public class Day09 : IPuzzle
    {
        public bool Active => false;

        public class Computer
        {
            public List<int> Program { get; set; }

            public int Pointer { get; set; }
        }

        private long GetValue(List<long> inputs, int v, int par)
        {
            if (par == 0)
            {
                return inputs[(int)inputs[v]];
            }
            else if (par == 1)
                return inputs[v];
            else if (par == 2)
                return inputs[(int)inputs[v] + relevantBase];
            else
                return -21345;
        }

        int relevantBase = 0;

        public string RunOne()
        {
            List<long> inputs = System.IO.File.ReadAllLines(@"..\..\Data\2019\input09.txt")[0].Split(',').Select(x => long.Parse(x)).ToList();

            for(int i = 0; i < 10000; i++)
            {
                inputs.Add(0);
            }

            //State state = new State {  }

            long output = 2;

            for (int i = 0; ;)
            {
                string start = inputs[i].ToString();
                long op = 0;
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
                else if(start.Length == 2)
                {
                    op = 99;
                }
                else if(start.Length == 5)
                {
                    op = (int)char.GetNumericValue(start[4]);
                    par1 = (int)char.GetNumericValue(start[2]);
                    par2 = (int)char.GetNumericValue(start[1]);
                    par3 = (int)char.GetNumericValue(start[0]);
                }
                else
                {
                    throw new Exception();
                }

                if (op == 1)
                {
                    if (par3 == 2)
                    {
                        inputs[(int)inputs[i + 3] + relevantBase] = GetValue(inputs, i + 1, par1) + GetValue(inputs, i + 2, par2);
                    }
                    else
                    {
                        inputs[(int)inputs[i + 3]] = GetValue(inputs, i + 1, par1) + GetValue(inputs, i + 2, par2);
                    }
                    i += 4;
                }
                else if (op == 2)
                {
                    if(par3 == 2)
                    {
                        inputs[(int)inputs[i + 3] + relevantBase] = GetValue(inputs, i + 1, par1) * GetValue(inputs, i + 2, par2);
                    }
                    else if(par3 == 1)
                    {

                    }
                    else if(par3 == 0)
                    {
                        inputs[(int)inputs[i + 3]] = GetValue(inputs, i + 1, par1) * GetValue(inputs, i + 2, par2);
                    }
                    else
                    {
                        throw new Exception();
                    }
                    i += 4;
                }
                else if (op == 3)
                {
                    if (i == 0)
                    {
                        inputs[(int)inputs[i + 1]] = output;
                    }
                    else
                    {
                        if(par1 == 2)
                        {
                            inputs[(int)inputs[i + 1] + relevantBase] = output;
                        }
                        else
                        {
                            inputs[(int)inputs[i + 1]] = output;
                        }
                    }
                    i += 2;
                }
                else if (op == 4)
                {
                    output = GetValue(inputs, i + 1, par1);
                    //if (j == 4 && output > maxSetting.Item1)
                    //    maxSetting = new Tuple<int, List<int>>(output, setting);
                    i += 2;
                    //break;
                }
                else if (op == 5)
                {
                    if (GetValue(inputs, i + 1, par1) != 0)
                    {
                        i = (int)GetValue(inputs, i + 2, par2);
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
                        i = (int)GetValue(inputs, i + 2, par2);
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
                        SetValue(inputs, i + 3, par3, 1);
                        //inputs[(int)inputs[i + 3]] = 1;
                    }
                    else
                    {
                        SetValue(inputs, i + 3, par3, 0);
                        //inputs[(int)inputs[i + 3]] = 0;
                    }
                    i += 4;
                }
                else if (op == 8)
                {
                    if (GetValue(inputs, i + 1, par1) == GetValue(inputs, i + 2, par2))
                    {
                        if(par3 == 2)
                        {
                            inputs[(int)inputs[i + 3] + relevantBase] = 1;
                        }
                        else
                        {
                            inputs[(int)inputs[i + 3]] = 1;
                        }
                    }
                    else
                    {
                        if(par3 == 2)
                        {
                            inputs[(int)inputs[i + 3] + relevantBase] = 0;
                        }
                        else
                        {
                            inputs[(int)inputs[i + 3]] = 0;
                        }
                    }
                    i += 4;
                }
                else if(op == 9)
                {
                    relevantBase += (int)GetValue(inputs, i + 1, par1);
                    i += 2;
                }
                else if (op == 99)
                {
                    return inputs[0].ToString();
                }
            }

            return "";
        }

        private void SetValue(List<long> inputs, int i, int par3, long value)
        {
            if (par3 == 2)
            {
                inputs[(int)inputs[i] + relevantBase] = value;
            }
            else
            {
                inputs[(int)inputs[i]] = value;
            }
        }

        public string RunTwo()
        {
            throw new NotImplementedException();
        }
    }
}
