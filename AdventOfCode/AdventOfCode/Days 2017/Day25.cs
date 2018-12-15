namespace AdventOfCode.Days
{
    public class Day25 : IPuzzle
    {
        public bool Active { get => true; }

        public string RunOne()
        {
            int steps = 12368930;
            string state = "A";
            int length = 8000;

            int[] tape = new int[length];
            int currentPosition = length / 2;

            for (int i = 0; i < steps; i++)
            {
                if (state == "A")
                {
                    if (tape[currentPosition] == 0)
                    {
                        tape[currentPosition] = 1;
                        currentPosition++;
                        state = "B";
                    }
                    else
                    {
                        tape[currentPosition] = 0;
                        currentPosition++;
                        state = "C";
                    }
                }
                else if (state == "B")
                {
                    if (tape[currentPosition] == 0)
                    {
                        tape[currentPosition] = 0;
                        currentPosition--;
                        state = "A";
                    }
                    else
                    {
                        tape[currentPosition] = 0;
                        currentPosition++;
                        state = "D";
                    }
                }
                else if (state == "C")
                {
                    if (tape[currentPosition] == 0)
                    {
                        tape[currentPosition] = 1;
                        currentPosition++;
                        state = "D";
                    }
                    else
                    {
                        tape[currentPosition] = 1;
                        currentPosition++;
                        state = "A";
                    }
                }
                else if (state == "D")
                {
                    if (tape[currentPosition] == 0)
                    {
                        tape[currentPosition] = 1;
                        currentPosition--;
                        state = "E";
                    }
                    else
                    {
                        tape[currentPosition] = 0;
                        currentPosition--;
                        state = "D";
                    }
                }
                else if (state == "E")
                {
                    if (tape[currentPosition] == 0)
                    {
                        tape[currentPosition] = 1;
                        currentPosition++;
                        state = "F";
                    }
                    else
                    {
                        tape[currentPosition] = 1;
                        currentPosition--;
                        state = "B";
                    }
                }
                else if (state == "F")
                {
                    if (tape[currentPosition] == 0)
                    {
                        tape[currentPosition] = 1;
                        currentPosition++;
                        state = "A";
                    }
                    else
                    {
                        tape[currentPosition] = 1;
                        currentPosition++;
                        state = "E";
                    }
                }
            }

            int sum = 0;
            for (int i = 0; i < length; i++)
            {
                sum += tape[i];
            }

            return sum.ToString();
        }

        public string RunTwo()
        {
            return "All puzzles completed!";
        }
    }
}
