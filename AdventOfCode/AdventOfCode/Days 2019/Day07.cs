using System;
using System.Collections.Generic;
using System.Linq;

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
            List<int> inputs2 = System.IO.File.ReadAllLines(@"..\..\Data\2019\input07.txt")[0].Split(',').Select(x => int.Parse(x)).ToList();

            Tuple<int, List<int>> maxSetting = new Tuple<int, List<int>>(int.MinValue, new List<int>());
            List<List<int>> phaseSettings = GeneratePhaseSettings(0, 4);

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
                            output = GetValue(inputs, i + 1, par1);
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

        private static List<List<int>> GeneratePhaseSettings(int start, int end)
        {
            List<List<int>> phaseSettings = new List<List<int>>();

            for (int i = start; i <= end; i++)
            {
                for (int j = start; j <= end; j++)
                {
                    if (i == j)
                        continue;
                    for (int k = start; k <= end; k++)
                    {
                        if (k == i || k == j)
                            continue;
                        for (int l = start; l <= end; l++)
                        {
                            if (l == k || l == j || l == i)
                                continue;
                            for (int a = start; a <= end; a++)
                            {
                                if (a == l || a == k || a == j || a == i)
                                    continue;
                                phaseSettings.Add(new List<int> { i, j, k, l, a });
                            }
                        }
                    }
                }
            }

            return phaseSettings;
        }

        public class State
        {
            public int Pointer { get; set; }
            public List<int> Register { get; set; }
            public int LastOutput { get; set; }
        }

        public string RunTwo()
        {
            List<int> inputs2 = System.IO.File.ReadAllLines(@"..\..\Data\2019\input07.txt")[0].Split(',').Select(x => int.Parse(x)).ToList();

            Tuple<int, List<int>> maxSetting = new Tuple<int, List<int>>(int.MinValue, new List<int>());

            List<List<int>> phaseSettings = GeneratePhaseSettings(5, 9);

            foreach (var setting in phaseSettings)
            {
                int output = 0;

                List<int> inputs = inputs2.ToList();

                Dictionary<int, State> state = new Dictionary<int, State>
                {
                    { 0, new State { Pointer = 0, Register = inputs.ToList() } },
                    { 1, new State { Pointer = 0, Register = inputs.ToList() } },
                    { 2, new State { Pointer = 0, Register = inputs.ToList() } },
                    { 3, new State { Pointer = 0, Register = inputs.ToList() } },
                    { 4, new State { Pointer = 0, Register = inputs.ToList() } },
                };

                bool done = false;

                for (int j = 0; j < setting.Count; j = (j + 1) % 5)
                {
                    for (int i = state[j].Pointer; i < state[j].Register.Count;)
                    {
                        string start = state[j].Register[i].ToString();
                        int op = 0;
                        int par1 = 0;
                        int par2 = 0;

                        if (start.Length == 1)
                        {
                            op = state[j].Register[i];
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
                            op = 99;
                        }

                        if (op == 1)
                        {
                            state[j].Register[state[j].Register[i + 3]] = GetValue(state[j].Register, i + 1, par1) + GetValue(state[j].Register, i + 2, par2);
                            i += 4;
                        }
                        else if (op == 2)
                        {
                            state[j].Register[state[j].Register[i + 3]] = GetValue(state[j].Register, i + 1, par1) * GetValue(state[j].Register, i + 2, par2);
                            i += 4;
                        }
                        else if (op == 3)
                        {
                            if (i == 0)
                            {
                                state[j].Register[state[j].Register[i + 1]] = setting[j];
                            }
                            else
                            {
                                state[j].Register[state[j].Register[i + 1]] = output;
                            }
                            i += 2;
                        }
                        else if (op == 4)
                        {
                            output = GetValue(state[j].Register, i + 1, par1);
                            state[j].LastOutput = output;
                            state[j].Pointer = i + 2;
                            break;
                        }
                        else if (op == 5)
                        {
                            if (GetValue(state[j].Register, i + 1, par1) != 0)
                            {
                                i = GetValue(state[j].Register, i + 2, par2);
                            }
                            else
                            {
                                i += 3;
                            }
                        }
                        else if (op == 6)
                        {
                            if (GetValue(state[j].Register, i + 1, par1) == 0)
                            {
                                i = GetValue(state[j].Register, i + 2, par2);
                            }
                            else
                            {
                                i += 3;
                            }
                        }
                        else if (op == 7)
                        {
                            if (GetValue(state[j].Register, i + 1, par1) < GetValue(state[j].Register, i + 2, par2))
                            {
                                state[j].Register[state[j].Register[i + 3]] = 1;
                            }
                            else
                            {
                                state[j].Register[state[j].Register[i + 3]] = 0;
                            }
                            i += 4;
                        }
                        else if (op == 8)
                        {
                            if (GetValue(state[j].Register, i + 1, par1) == GetValue(state[j].Register, i + 2, par2))
                            {
                                state[j].Register[state[j].Register[i + 3]] = 1;
                            }
                            else
                            {
                                state[j].Register[state[j].Register[i + 3]] = 0;
                            }
                            i += 4;
                        }
                        else if (op == 99)
                        {
                            if (j == 4)
                            {
                                done = true;
                            }
                            break;
                        }
                    }

                    if (done)
                    {
                        if (state[4].LastOutput > maxSetting.Item1)
                        {
                            maxSetting = new Tuple<int, List<int>>(state[4].LastOutput, setting);
                        }
                        break;
                    }
                }
            }
            return maxSetting.Item1.ToString();
        }
    }
}
