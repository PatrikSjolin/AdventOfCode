using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Days_2018
{
    public class Day16 : IPuzzle
    {
        public bool Active => true;
        List<string> inputLines = System.IO.File.ReadAllLines(@"..\..\Data\2018\input16.txt").ToList();

        private Dictionary<int, int> Addr(Dictionary<int, int> registers, int a, int b, int c)
        {
            registers[c] = registers[a] + registers[b];
            return registers;
        }

        private Dictionary<int, int> Addi(Dictionary<int, int> registers, int a, int b, int c)
        {
            registers[c] = registers[a] + b;
            return registers;
        }

        private Dictionary<int, int> Mulr(Dictionary<int, int> registers, int a, int b, int c)
        {
            registers[c] = registers[a] * registers[b];
            return registers;
        }

        private Dictionary<int, int> Muli(Dictionary<int, int> registers, int a, int b, int c)
        {
            registers[c] = registers[a] * b;
            return registers;
        }

        private Dictionary<int, int> Banr(Dictionary<int, int> registers, int a, int b, int c)
        {
            registers[c] = registers[a] & registers[b];
            return registers;
        }

        private Dictionary<int, int> Bani(Dictionary<int, int> registers, int a, int b, int c)
        {
            registers[c] = registers[a] & b;
            return registers;
        }

        private Dictionary<int, int> Borr(Dictionary<int, int> registers, int a, int b, int c)
        {
            registers[c] = registers[a] | registers[b];
            return registers;
        }

        private Dictionary<int, int> Bori(Dictionary<int, int> registers, int a, int b, int c)
        {
            registers[c] = registers[a] | b;
            return registers;
        }

        private Dictionary<int, int> Setr(Dictionary<int, int> registers, int a, int b, int c)
        {
            registers[c] = registers[a];
            return registers;
        }

        private Dictionary<int, int> Seti(Dictionary<int, int> registers, int a, int b, int c)
        {
            registers[c] = a;
            return registers;
        }

        private Dictionary<int, int> Gtir(Dictionary<int, int> registers, int a, int b, int c)
        {
            if (a > registers[b])
                registers[c] = 1;
            else
                registers[c] = 0;
            return registers;
        }

        private Dictionary<int, int> Gtri(Dictionary<int, int> registers, int a, int b, int c)
        {
            if (registers[a] > b)
                registers[c] = 1;
            else
                registers[c] = 0;
            return registers;
        }

        private Dictionary<int, int> Gtrr(Dictionary<int, int> registers, int a, int b, int c)
        {
            if (registers[a] > registers[b])
                registers[c] = 1;
            else
                registers[c] = 0;
            return registers;
        }

        private Dictionary<int, int> Eqir(Dictionary<int, int> registers, int a, int b, int c)
        {
            if (a == registers[b])
                registers[c] = 1;
            else
                registers[c] = 0;
            return registers;
        }

        private Dictionary<int, int> Eqri(Dictionary<int, int> registers, int a, int b, int c)
        {
            if (registers[a] == b)
                registers[c] = 1;
            else
                registers[c] = 0;
            return registers;
        }

        private Dictionary<int, int> Eqrr(Dictionary<int, int> registers, int a, int b, int c)
        {
            if (registers[a] == registers[b])
                registers[c] = 1;
            else
                registers[c] = 0;
            return registers;
        }

        List<Func<Dictionary<int, int>, int, int, int, Dictionary<int, int>>> functions = new List<Func<Dictionary<int, int>, int, int, int, Dictionary<int, int>>>();

        public string RunOne()
        {
            functions.Add(Addr);
            functions.Add(Addi);
            functions.Add(Mulr);
            functions.Add(Muli);
            functions.Add(Banr);
            functions.Add(Bani);
            functions.Add(Borr);
            functions.Add(Bori);
            functions.Add(Setr);
            functions.Add(Seti);
            functions.Add(Gtir);
            functions.Add(Gtri);
            functions.Add(Gtrr);
            functions.Add(Eqir);
            functions.Add(Eqri);
            functions.Add(Eqrr);
            int opcodesMoreThanThree = 0;

            for (int i = 0; i < inputLines.Count; i += 4)
            {
                string first = inputLines[i + 0].Substring(9, inputLines[i + 0].Length - 9).Replace("]", "").Replace(" ", "");

                Dictionary<int, int> registers = new Dictionary<int, int>();

                var numbers = first.Split(',').ToList();
                int op = int.Parse(numbers[0]);
                registers.Add(0, op);
                int a = int.Parse(numbers[1]);
                registers.Add(1, a);
                int b = int.Parse(numbers[2]);
                registers.Add(2, b);
                int c = int.Parse(numbers[3]);
                registers.Add(3, c);
                string second = inputLines[i + 1];
                
                numbers = second.Split(' ').ToList();
                op = int.Parse(numbers[0]);
                a = int.Parse(numbers[1]);
                b = int.Parse(numbers[2]);
                c = int.Parse(numbers[3]);

                int fit = 0;

                foreach(var f in functions)
                {
                    Dictionary<int, int> test = new Dictionary<int, int>();
                    test[0] = registers[0];
                    test[1] = registers[1];
                    test[2] = registers[2];
                    test[3] = registers[3];
                    var result = f.Invoke(test, a, b, c);

                    string third = inputLines[i + 2].Substring(9, inputLines[i + 2].Length - 9).Replace("]", "").Replace(" ", "");
                    numbers = third.Split(',').ToList();
                    int resop = int.Parse(numbers[0]);
                    int resa = int.Parse(numbers[1]);
                    int resb = int.Parse(numbers[2]);
                    int resc = int.Parse(numbers[3]);

                    if(result[0] == resop && result[1] == resa && result[2] == resb && result[3] == resc)
                    {
                        fit++;
                    }
                }

                if (fit >= 3)
                    opcodesMoreThanThree++;
            }
            return opcodesMoreThanThree.ToString();
        }



        public string RunTwo()
        {
            Dictionary<int, Func<Dictionary<int, int>, int, int, int, Dictionary<int, int>>> operationToFunction = new Dictionary<int, Func<Dictionary<int, int>, int, int, int, Dictionary<int, int>>>();

            for (int i = 0; i < inputLines.Count; i += 4)
            {
                string first = inputLines[i + 0].Substring(9, inputLines[i + 0].Length - 9).Replace("]", "").Replace(" ", "");

                Dictionary<int, int> registers = new Dictionary<int, int>();

                var numbers = first.Split(',').ToList();
                int reg1 = int.Parse(numbers[0]);
                registers.Add(0, reg1);
                int reg2 = int.Parse(numbers[1]);
                registers.Add(1, reg2);
                int reg3 = int.Parse(numbers[2]);
                registers.Add(2, reg3);
                int reg4 = int.Parse(numbers[3]);
                registers.Add(3, reg4);

                string second = inputLines[i + 1];

                numbers = second.Split(' ').ToList();
                int op = int.Parse(numbers[0]);
                int a = int.Parse(numbers[1]);
                int b = int.Parse(numbers[2]);
                int c = int.Parse(numbers[3]);

                int fit = 0;
                Func<Dictionary<int, int>, int, int, int, Dictionary<int, int>> func = null;

                foreach (var f in functions)
                {
                    Dictionary<int, int> test = new Dictionary<int, int>();
                    test[0] = registers[0];
                    test[1] = registers[1];
                    test[2] = registers[2];
                    test[3] = registers[3];
                    var result = f.Invoke(test, a, b, c);

                    string third = inputLines[i + 2].Substring(9, inputLines[i + 2].Length - 9).Replace("]", "").Replace(" ", "");
                    numbers = third.Split(',').ToList();
                    int resop = int.Parse(numbers[0]);
                    int resa = int.Parse(numbers[1]);
                    int resb = int.Parse(numbers[2]);
                    int resc = int.Parse(numbers[3]);

                    if (result[0] == resop && result[1] == resa && result[2] == resb && result[3] == resc)
                    {
                        fit++;
                        func = f;
                    }
                }

                if (fit == 1)
                {
                    if (!operationToFunction.ContainsKey(op))
                    {
                        operationToFunction.Add(op, func);
                        functions.Remove(func);
                    }
                }
            }

            inputLines = System.IO.File.ReadAllLines(@"..\..\Data\2018\input16b.txt").ToList();
            Dictionary<int, int> regs = new Dictionary<int, int>();
            regs.Add(0, 0);
            regs.Add(1, 0);
            regs.Add(2, 0);
            regs.Add(3, 0);

            for (int i = 0; i < inputLines.Count; i++)
            {
                int op = int.Parse(inputLines[i].Split(' ')[0]);
                int a = int.Parse(inputLines[i].Split(' ')[1]);
                int b = int.Parse(inputLines[i].Split(' ')[2]);
                int c = int.Parse(inputLines[i].Split(' ')[3]);

                operationToFunction[op].Invoke(regs, a, b, c);
            }

            return regs[0].ToString();
        }
    }
}
