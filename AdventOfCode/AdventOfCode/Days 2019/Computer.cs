using System;

namespace AdventOfCode.Days_2019
{
    public class Computer
    {
        private int relevantBase = 0;

        public Computer(int input)
        {
            Input = input;
        }

        public event EventHandler OutputEvent;
        public event EventHandler InputEvent;

        private void SetValue(long[] inputs, int i, int par3, long value)
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

        private long GetValue(long[] inputs, int v, int par)
        {
            if (par == 0)
                return inputs[(int)inputs[v]];
            else if (par == 1)
                return inputs[v];
            else if (par == 2)
                return inputs[(int)inputs[v] + relevantBase];
            else
                return -21345;
        }

        public int Input { get; set; }

        public long Output { get; set; }

        public long Compute(long[] inputs)
        {
            for (int pointer = 0; ;)
            {
                long op = 0;
                int par1 = 0;
                int par2 = 0;
                int par3 = 0;

                switch ((int)Math.Floor(Math.Log10(inputs[pointer])) + 1)
                {
                    case 1:
                        op = inputs[pointer];
                        break;
                    case 2:
                        op = inputs[pointer];
                        break;
                    case 3:
                        op = inputs[pointer] % 10;
                        par1 = (int)inputs[pointer] / 100;
                        break;
                    case 4:
                        op = inputs[pointer] % 10;
                        par1 = (int)(inputs[pointer] / 100) % 10;
                        par2 = (int)inputs[pointer] / 1000;
                        break;
                    case 5:
                        op = inputs[pointer] % 10;
                        par1 = (int)(inputs[pointer] / 100) % 10;
                        par2 = (int)(inputs[pointer] / 1000) % 10;
                        par3 = (int)inputs[pointer] / 10000;
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
                        if (par1 == 2)
                        {
                            if (InputEvent != null)
                                InputEvent(this, new EventArgs());

                            inputs[inputs[pointer + 1] + relevantBase] = Input;
                        }
                        else
                        {
                            if (InputEvent != null)
                                InputEvent(this, new EventArgs());

                            inputs[inputs[pointer + 1]] = Input;
                        }
                        pointer += 2;
                        break;
                    case 4:

                        Output = GetValue(inputs, pointer + 1, par1);

                        pointer += 2;
                        if (OutputEvent != null)
                            OutputEvent(this, new EventArgs());
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
                        return Output;
                }
            }
        }
    }
}
