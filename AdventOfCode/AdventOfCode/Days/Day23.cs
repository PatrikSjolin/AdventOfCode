using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Days
{
    public class Day23 : IPuzzle
    {
        public string RunOne()
        {
            List<string> input = System.IO.File.ReadAllLines(@"..\..\Data\2017\input23.txt").ToList();
            Dictionary<string, int> registers = new Dictionary<string, int> { { "a", 0 }, { "b", 0 }, { "c", 0 }, { "d", 0 }, { "e", 0 }, { "f", 0 }, { "g", 0 }, { "h", 0 } };

            int muls = 0;

            for(int i = 0; i < input.Count; i++)
            {
                List<string> op = input[i].Split(' ').ToList();

                if (input[i].StartsWith("set"))
                {
                    int value = 0;

                    if(int.TryParse(op[2], out value))
                    {
                        registers[op[1]] = value;
                    }
                    else
                    {
                        registers[op[1]] = registers[op[2]];
                    }
                }
                else if (input[i].StartsWith("sub"))
                {
                    int value = 0;

                    if (int.TryParse(op[2], out value))
                    {
                        registers[op[1]] -= value;
                    }
                    else
                    {
                        registers[op[1]] -= registers[op[2]];
                    }
                }
                else if (input[i].StartsWith("mul"))
                {
                    muls++;
                    int value = 0;

                    if (int.TryParse(op[2], out value))
                    {
                        registers[op[1]] *= value;
                    }
                    else
                    {
                        registers[op[1]] *= registers[op[2]];
                    }
                }
                else if (input[i].StartsWith("jnz"))
                {
                    int test = 0;

                    if (!int.TryParse(op[1], out test))
                    {
                        test = registers[op[1]];
                    }


                    if (test != 0)
                    {
                        long value = 0;
                        if (long.TryParse(op[2], out value))
                        {
                            if (value != 0)
                                value--;
                            i += (int)value;
                        }
                        else
                        {
                            value = registers[op[2]];
                            if (value != 0)
                                value--;

                            i += (int)value;
                        }
                    }
                }
            }

            return muls.ToString();
        }

        public string RunTwo()
        {
            int h = 0;

            for (int b = 106500; b <= 123500; b += 17)
            {
                int f = 1;
                for (int d = 2; d < Math.Sqrt(b); d++)
                {
                    if(b % d == 0)
                    {
                        f = 0;
                        break;
                    }
                }
                if (f == 0)
                    h++;
            }

            return h.ToString();
        }
    }
}
