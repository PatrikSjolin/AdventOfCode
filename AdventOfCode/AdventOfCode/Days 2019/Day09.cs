﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Days_2019
{
    public class Day09 : IPuzzle
    {
        public bool Active => true;

        public class Computer
        {
            public List<int> Program { get; set; }

            public int Pointer { get; set; }
        }

        private static long GetValue(long[] inputs, int v, int par)
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

        static int relevantBase = 0;

        public string RunOne()
        {
            relevantBase = 0;
            List<long> inputs = System.IO.File.ReadAllLines(@"..\..\Data\2019\input09.txt")[0].Split(',').Select(x => long.Parse(x)).ToList();

            for (int i = 0; i < 1000; i++)
            {
                inputs.Add(0);
            }

            return Compute(inputs.ToArray(), 1).ToString();
        }

        private static void SetValue(long[] inputs, int i, int par3, long value)
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

        public static long Compute(long[] inputs, int input)
        {
            long output = input;

            for (int pointer = 0; ;)
            {
                string start = inputs[pointer].ToString();
                long op = 0;
                int par1 = 0;
                int par2 = 0;
                int par3 = 0;

                switch (start.Length)
                {
                    case 1:
                        op = inputs[pointer];
                        break;
                    case 2:
                        op = inputs[pointer];
                        break;
                    case 3:
                        op = (int)char.GetNumericValue(start[2]);
                        par1 = (int)char.GetNumericValue(start[0]);
                        break;
                    case 4:
                        op = (int)char.GetNumericValue(start[3]);
                        par1 = (int)char.GetNumericValue(start[1]);
                        par2 = (int)char.GetNumericValue(start[0]);
                        break;
                    case 5:
                        op = (int)char.GetNumericValue(start[4]);
                        par1 = (int)char.GetNumericValue(start[2]);
                        par2 = (int)char.GetNumericValue(start[1]);
                        par3 = (int)char.GetNumericValue(start[0]);
                        break;
                    default:
                        break;
                }

                switch (op)
                {
                    case 1:
                            SetValue(inputs, pointer + 3, par3, GetValue(inputs, pointer + 1, par1) + GetValue(inputs, pointer + 2, par2));
                            pointer += 4;
                            break;
                    case 2:
                        
                            SetValue(inputs, pointer + 3, par3, GetValue(inputs, pointer + 1, par1) * GetValue(inputs, pointer + 2, par2));
                            pointer += 4;
                        break;
                    case 3:
                        
                            if (pointer == 0)
                            {
                                inputs[inputs[pointer + 1]] = output;
                            }
                            else
                            {
                                if (par1 == 2)
                                {
                                    inputs[inputs[pointer + 1] + relevantBase] = output;
                                }
                                else
                                {
                                    inputs[inputs[pointer + 1]] = output;
                                }
                            }
                            pointer += 2;
                        break;
                    case 4:
                        
                            output = GetValue(inputs, pointer + 1, par1);
                            pointer += 2;
                        break;
                    case 5:
                        
                        
                            if (GetValue(inputs, pointer + 1, par1) != 0)
                            {
                                pointer = (int)GetValue(inputs, pointer + 2, par2);
                            }
                            else
                            {
                                pointer += 3;
                            }
                        break;
                    case 6:
                        
                            if (GetValue(inputs, pointer + 1, par1) == 0)
                            {
                                pointer = (int)GetValue(inputs, pointer + 2, par2);
                            }
                            else
                            {
                                pointer += 3;
                            }
                        break;
                    case 7:
                        
                            if (GetValue(inputs, pointer + 1, par1) < GetValue(inputs, pointer + 2, par2))
                            {
                                SetValue(inputs, pointer + 3, par3, 1);
                            }
                            else
                            {
                                SetValue(inputs, pointer + 3, par3, 0);
                            }
                            pointer += 4;
                        break;
                    case 8:
                            if (GetValue(inputs, pointer + 1, par1) == GetValue(inputs, pointer + 2, par2))
                            {
                                SetValue(inputs, pointer + 3, par3, 1);
                            }
                            else
                            {
                                SetValue(inputs, pointer + 3, par3, 0);
                            }
                            pointer += 4;
                        break;
                    case 9:
                            relevantBase += (int)GetValue(inputs, pointer + 1, par1);
                            pointer += 2;
                        break;
                    case 99:
                            return output;
                }
            }
        }

        public string RunTwo()
        {
            relevantBase = 0;
            List<long> inputs = System.IO.File.ReadAllLines(@"..\..\Data\2019\input09.txt")[0].Split(',').Select(x => long.Parse(x)).ToList();

            for (int i = 0; i < 1000; i++)
            {
                inputs.Add(0);
            }

            return Compute(inputs.ToArray(), 2).ToString();
        }
    }
}
