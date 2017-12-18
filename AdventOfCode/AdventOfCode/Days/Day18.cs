using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace AdventOfCode.Days
{
    public class Day18 : IPuzzle
    {
        public string RunOne()
        {
            List<string> inputLines = System.IO.File.ReadAllLines(@"..\..\input18.txt").ToList();
            Dictionary<string, long> registers = new Dictionary<string, long>();

            long sound = 0;
            long recover = 0;

            for (int i = 0; i < inputLines.Count; i++)
            {
                List<string> split = inputLines[i].Split(' ').ToList();

                string op = split[0];
                string reg1 = split[1];

                string reg2 = "";
                if (split.Count > 2)
                    reg2 = split[2];

                if (!registers.ContainsKey(reg1))
                {
                    registers.Add(reg1, 0);
                }

                if (op == "snd")
                {
                    sound = registers[reg1];
                }
                if (op == "set")
                {
                    long value = 0;
                    if (long.TryParse(reg2, out value))
                    {
                        registers[reg1] = value;
                    }
                    else
                    {
                        registers[reg1] = registers[reg2];
                    }
                }
                if (op == "add")
                {
                    long value = 0;
                    if (long.TryParse(reg2, out value))
                    {
                        registers[reg1] += value;
                    }
                    else
                    {
                        registers[reg1] += registers[reg2];
                    }
                }
                if (op == "mul")
                {
                    long value = 0;
                    if (long.TryParse(reg2, out value))
                    {
                        registers[reg1] *= value;
                    }
                    else
                    {
                        registers[reg1] *= registers[reg2];
                    }
                }
                if (op == "mod")
                {
                    long value = 0;
                    if (long.TryParse(reg2, out value))
                    {
                        registers[reg1] %= value;
                    }
                    else
                    {
                        registers[reg1] %= registers[reg2];
                    }
                }
                if (op == "rcv")
                {
                    if (registers[reg1] != 0)
                    {
                        recover = sound;
                        break;
                    }
                }
                if (op == "jgz")
                {
                    if (registers[reg1] > 0)
                    {
                        long value = 0;
                        if (long.TryParse(reg2, out value))
                        {
                            if (value != 0)
                                value--;
                            i += (int)value;
                        }
                        else
                        {
                            value = registers[reg2];
                            if (value != 0)
                                value--;

                            i += (int)value;
                        }
                    }
                }

            }
            return recover.ToString();
        }


        List<string> inputLines;
        static int countOne;

        Queue<long> sentZero = new Queue<long>();
        Queue<long> sentOne = new Queue<long>();

        public string RunTwo()
        {
            inputLines = System.IO.File.ReadAllLines(@"..\..\input18.txt").ToList();

            Dictionary<string, long> registersZero = new Dictionary<string, long>();
            Dictionary<string, long> registersOne = new Dictionary<string, long>();

            Thread t1 = new Thread(() => RunProgram(sentZero, sentOne, 0, 0));
            Thread t2 = new Thread(() => RunProgram(sentOne, sentZero, 0, 1));

            bool thread1 = false;
            bool thread2 = false;

            countOne = 0;

            t1.Start();
            t2.Start();

            while (true)
            {
                if (t1.IsAlive == false)
                    thread1 = true;

                if (t2.IsAlive == false)
                    thread2 = true;

                if (thread1 && thread2)
                    break;
                Thread.Sleep(1);
            }

            return countOne.ToString();
        }

        public void RunProgram(Queue<long> receive, Queue<long> send, int wait, int id)
        {
            Dictionary<string, long> registers = new Dictionary<string, long>();

            registers.Add("p", id);

            for (int i = 0; i < inputLines.Count; i++)
            {
                List<string> split = inputLines[i].Split(' ').ToList();

                string op = split[0];
                string reg1 = split[1];

                string reg2 = "";
                if (split.Count > 2)
                    reg2 = split[2];

                int test = 0;
                if (!int.TryParse(reg1, out test))
                {
                    if (!registers.ContainsKey(reg1))
                    {
                        registers.Add(reg1, 0);
                    }
                }
                if (op == "snd")
                {
                    lock (send)
                    {
                        long value = 0;
                        if (long.TryParse(reg1, out value))
                        {
                            send.Enqueue(value);
                        }
                        else
                        {
                            send.Enqueue(registers[reg1]);
                        }
                        if (id == 1)
                            countOne++;
                    }
                }
                if (op == "set")
                {
                    long value = 0;
                    if (long.TryParse(reg2, out value))
                    {
                        registers[reg1] = value;
                    }
                    else
                    {
                        registers[reg1] = registers[reg2];
                    }
                }
                if (op == "add")
                {
                    long value = 0;
                    if (long.TryParse(reg2, out value))
                    {
                        registers[reg1] += value;
                    }
                    else
                    {
                        registers[reg1] += registers[reg2];
                    }
                }
                if (op == "mul")
                {
                    long value = 0;
                    if (long.TryParse(reg2, out value))
                    {
                        registers[reg1] *= value;
                    }
                    else
                    {
                        registers[reg1] *= registers[reg2];
                    }
                }
                if (op == "mod")
                {
                    long value = 0;
                    if (long.TryParse(reg2, out value))
                    {
                        registers[reg1] %= value;
                    }
                    else
                    {
                        registers[reg1] %= registers[reg2];
                    }
                }
                if (op == "rcv")
                {
                    bool received = false;
                    wait = 0;
                    while (!received)
                    {
                        lock (receive)
                        {
                            if (receive.Count > 0)
                            {
                                received = true;
                                registers[reg1] = receive.Dequeue();
                            }
                        }
                        if (!received)
                        {
                            wait++;

                            if (wait == 4)
                            {
                                return;
                            }
                            Thread.Sleep(1);
                        }
                    }
                }
                if (op == "jgz")
                {
                    if (reg1 == "1" || registers[reg1] > 0)
                    {
                        long value = 0;
                        if (long.TryParse(reg2, out value))
                        {
                            if (value != 0)
                                value --;

                            i += (int)value;
                        }
                        else
                        {
                            value = registers[reg2];
                            if (value != 0)
                                value--;

                            i += (int)value;
                        }
                    }
                }
            }
        }
    }
}
