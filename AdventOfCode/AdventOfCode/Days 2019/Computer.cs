using System;

namespace AdventOfCode.Days_2019
{
    public class Computer
    {
        private int relevantBase = 0;

        public event EventHandler OutputEvent;
        public event EventHandler InputEvent;
        public int ID { get; set; }
        public bool IsWriting { get; set; }
        public long Input { get; set; }
        public long Output { get; set; }

        public Computer(int input)
        {
            Input = input;
        }

        private int CountDigits(long n)
        {
            if (n < 10) return 1;
            if (n < 100) return 2;
            if (n < 1000) return 3;
            if (n < 10000) return 4;
            if (n < 100000) return 5;
            if (n < 1000000) return 6;
            if (n < 10000000) return 7;
            if (n < 100000000) return 8;
            if (n < 1000000000) return 9;
            return 10;
        }

        public long Compute(long[] inputs)
        {
            for (int pointer = 0; ;)
            {
                long operation = 0;
                int par1 = 0;
                int par2 = 0;
                int par3 = 0;

                switch (CountDigits(inputs[pointer]))
                //switch ((int)Math.Log10(inputs[pointer]) + 1)
                {
                    case 1:
                        operation = inputs[pointer];
                        break;
                    case 2:
                        operation = inputs[pointer];
                        break;
                    case 3:
                        operation = inputs[pointer] % 10;
                        par1 = (int)inputs[pointer] / 100;
                        break;
                    case 4:
                        operation = inputs[pointer] % 10;
                        par1 = (int)(inputs[pointer] / 100) % 10;
                        par2 = (int)inputs[pointer] / 1000;
                        break;
                    case 5:
                        operation = inputs[pointer] % 10;
                        par1 = (int)(inputs[pointer] / 100) % 10;
                        par2 = (int)(inputs[pointer] / 1000) % 10;
                        par3 = (int)inputs[pointer] / 10000;
                        break;
                    default:
                        break;
                }

                switch (operation)
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
                        if (InputEvent != null)
                            InputEvent(this, new EventArgs());
                        if (par1 == 2)
                        {
                            inputs[inputs[pointer + 1] + relevantBase] = Input;
                        }
                        else
                        {
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
    }
}