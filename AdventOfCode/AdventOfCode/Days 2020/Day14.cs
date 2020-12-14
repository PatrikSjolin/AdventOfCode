using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
            string binary = Convert.ToString(int.Parse(v), 2);

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < mask.Length; i++)
            {
                if (mask[mask.Length - 1 - i] != 'X')
                    sb.Insert(0, mask[mask.Length - 1 - i]);
                else if (i >= binary.Length)
                {
                    if (mask[mask.Length - 1 - i] == '1')
                        sb.Insert(0, "1");
                    else
                        sb.Insert(0, "0");
                }
                else
                    sb.Insert(0, binary[binary.Length - 1 - i]);
            }
            return sb.ToString();
        }

        public string RunTwo()
        {
            Dictionary<string, int> memory = new Dictionary<string, int>();
            Dictionary<int, List<string>> fluctuationsCache = new Dictionary<int, List<string>>();
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
                    int value = int.Parse(split[2]);
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
                            result = "1" + result;
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
                    List<string> fluctuations;
                    if (fluctuationsCache.ContainsKey(numberOfX))
                    {
                        fluctuations = fluctuationsCache[numberOfX];
                    }
                    else
                    {
                        fluctuations = GenerateBinaryNumbers(newAddresses, numberOfX);
                        fluctuationsCache.Add(numberOfX, fluctuations);
                    }
                    
                    //Generate addresses based on fluctuations
                    List<string> addresses = new List<string>();
                    StringBuilder sb = new StringBuilder();
                    for (int j = 0; j < fluctuations.Count; j++)
                    {
                        sb.Clear();
                        int index = 0;
                        for (int k = 0; k < result.Length; k++)
                        {
                            if (result[k] != 'X')
                                sb.Append(result[k]);
                            else
                            {
                                sb.Append(fluctuations[j][index]);
                                index++;
                            }
                        }
                        addresses.Add(sb.ToString());
                    }

                    //Set values to generated addresses
                    foreach (var a in addresses)
                    {
                        if (memory.ContainsKey(a))
                        {
                            memory[a] = value;
                        }
                        else
                        {
                            memory.Add(a, value);
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
