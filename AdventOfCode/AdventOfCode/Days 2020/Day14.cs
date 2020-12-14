using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Days_2020
{
    public class Day14 : IPuzzle
    {
        public bool Active => true;

        private List<string> inputs;

        public string RunOne()
        {
            inputs = System.IO.File.ReadAllLines(@"..\..\Data\2020\input14.txt").ToList();


            Dictionary<int, string> memory = new Dictionary<int, string>();
            string mask = "";

            for (int i = 0; i < inputs.Count; i++)
            {
                List<string> split = inputs[i].Split(' ').ToList();

                if (split[0] == "mask")
                {
                    mask = split[2];
                }
                else
                {
                    string mem = split[0].Split('[')[1];
                    mem = mem.Substring(0, mem.Length - 1);
                    int memoryIndex = int.Parse(mem);

                    if (memory.ContainsKey(memoryIndex))
                    {
                        memory[memoryIndex] = Mask(mask, split[2]);
                    }
                    else
                    {
                        memory.Add(memoryIndex, Mask(mask, split[2]));
                    }
                }
            }

            long sum = 0;
            foreach (var value in memory.Values)
            {
                sum += Convert.ToInt64(value, 2);
            }

            return sum.ToString();
        }

        private string Mask(string mask, string v)
        {
            string result = "";
            string binary = Convert.ToString(int.Parse(v), 2);

            for (int i = 0; i < mask.Length; i++)
            {
                if (mask[mask.Length - 1 - i] != 'X')
                    result = mask[mask.Length - 1 - i] + result;
                else if (i >= binary.Length)
                {
                    if (mask[mask.Length - 1 - i] == '1')
                        result = "1" + result;
                    else
                        result = "0" + result;
                }
                else
                    result = binary[binary.Length - 1 - i] + result;
            }
            return result;
        }

        public string RunTwo()
        {
            inputs = System.IO.File.ReadAllLines(@"..\..\Data\2020\input14.txt").ToList();
            Dictionary<string, int> memory = new Dictionary<string, int>();
            string mask = "";

            for (int i = 0; i < inputs.Count; i++)
            {
                List<string> split = inputs[i].Split(' ').ToList();

                if (split[0] == "mask")
                {
                    mask = split[2];
                }
                else
                {
                    string value = split[2];
                    string mem = split[0].Split('[')[1];
                    mem = mem.Substring(0, mem.Length - 1);
                    int memoryIndex = int.Parse(mem);

                    string result = "";
                    string binary = Convert.ToString(memoryIndex, 2);

                    //Apply mask
                    for (int j = 0; j < mask.Length; j++)
                    {
                        if (mask[mask.Length - 1 - j] == 'X')
                            result = "X" + result;
                        else if (mask[mask.Length - 1 - j] == '1')
                        {
                            result = "1" + result;
                        }
                        else
                        {
                            if (j < binary.Length)
                                result = binary[binary.Length - 1 - j] + result;
                            else
                                result = "0" + result;
                        }
                    }

                    int numberOfX = result.Count(x => x == 'X');
                    int newAddresses = (int)Math.Pow(2, numberOfX);

                    //Generate 2^numberOfX fluctuations
                    List<string> fluctuations = GenerateBinaryNumbers(newAddresses, numberOfX);

                    List<string> addresses = new List<string>();

                    for (int j = 0; j < fluctuations.Count; j++)
                    {
                        string final = "";
                        int index = 0;
                        for (int k = 0; k < result.Length; k++)
                        {
                            if (result[k] != 'X')
                                final = final + result[k];
                            else
                            {
                                final = final + fluctuations[j][index];
                                index++;
                            }
                        }
                        addresses.Add(final);
                    }

                    foreach (var a in addresses)
                    {
                        if (memory.ContainsKey(a))
                        {
                            memory[a] = int.Parse(value);
                        }
                        else
                        {
                            memory.Add(a, int.Parse(value));
                        }
                    }
                }
            }

            long sum = 0;
            foreach (var value in memory.Values)
            {
                sum += value;
            }

            return sum.ToString();
        }

        private List<string> GenerateBinaryNumbers(int newAddresses, int numberOfX)
        {
            List<string> list = new List<string>();
            for(int i = 0; i < newAddresses; i++)
            {
                string binary = Convert.ToString(i, 2);
                while (binary.Length != numberOfX)
                    binary = "0" + binary;
                list.Add(binary);
            }
            return list;
        }
    }
}
