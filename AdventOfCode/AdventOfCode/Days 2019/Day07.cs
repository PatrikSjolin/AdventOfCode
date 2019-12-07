using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Days_2019
{
    public class Day07 : IPuzzle
    {
        public bool Active => true;

        private int GetValue(List<int> inputs, int v, int par)
        {
            if (par == 0)
                return inputs[inputs[v]];
            else
                return inputs[v];
        }

        public string RunOne()
        {
            List<int> inputs2 = System.IO.File.ReadAllLines(@"..\..\Data\2019\input07test.txt")[0].Split(',').Select(x => int.Parse(x)).ToList();

            Tuple<int, List<int>> maxSetting = new Tuple<int, List<int>>(int.MinValue, new List<int>());

            List<List<int>> phaseSettings = new List<List<int>>();

            for(int i = 0; i <= 4; i++)
            {
                for(int j = 0; j <= 4; j++)
                {
                    for(int k = 0; k <= 4; k++)
                    {
                        for(int l = 0; l <= 4; l++)
                        {
                            for(int a = 0; a <= 4; a++)
                            {
                                phaseSettings.Add(new List<int> { i, j, k, l, a });
                            }
                        }
                    }
                }
            }

            //phaseSettings = new List<List<int>> { new List<int> { 4, 3, 2, 1, 0 } };


            foreach (var setting in phaseSettings)
            {
                int output = 0;
                for (int j = 0; j < setting.Count; j++)
                {
                    List<int> inputs = inputs2.ToList();

                    for (int i = 0; i < inputs.Count;)
                    {
                        string start = inputs[i].ToString();
                        int op = 0;
                        int par1 = 0;
                        int par2 = 0;

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
                        else
                        {

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
                            if (i == 0)
                            {
                                inputs[inputs[i + 1]] = setting[j];
                            }
                            else
                            {
                                inputs[inputs[i + 1]] = output;
                            }
                            i += 2;
                        }
                        else if (op == 4)
                        {
                            output = inputs[inputs[i + 1]];
                            if (j == 4 && output > maxSetting.Item1)
                                maxSetting = new Tuple<int, List<int>>(output, setting);
                            break;
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
                    }

                }
            }
            return maxSetting.Item1.ToString();
        }

        public string RunTwo()
        {
            throw new NotImplementedException();
        }
    }
}
