using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Days
{
    public class Day10 : IPuzzle
    {
        public string RunOne()
        {
            List<string> inputLines = System.IO.File.ReadAllLines(@"..\..\input10.txt").ToList();
            List<int> input = inputLines[0].Split(',').Select(x => int.Parse(x)).ToList();
            List<int> list = new List<int>();
            for (int i = 0; i < 256; i++)
            {
                list.Add(i);
            }
            int current = 0;
            int skipSize = 0;
            for (int i = 0; i < input.Count; i++)
            {
                List<int> reversed = GetReversed(list, current, input[i]);

                for (int j = current; j < current + input[i]; j++)
                {
                    list[j % list.Count] = reversed[j - current];
                }
                current = (current + input[i] + skipSize) % list.Count;
                skipSize++;
            }

            return (list[0] * list[1]).ToString();
        }

        private List<int> GetReversed(List<int> list, int current, int v)
        {
            List<int> reversed = new List<int>();

            for(int i = current; i < current + v; i++)
            {
                reversed.Add(list[i % list.Count]);
            }

            reversed.Reverse();
            return reversed;
        }

        public string RunTwo()
        {
            List<string> inputLines = System.IO.File.ReadAllLines(@"..\..\input10.txt").ToList();

            string input = inputLines[0];

            List<int> extraCharacters = new List<int> { 17, 31, 73, 47, 23};

            List<int> inputAscii = GetAscii(input.Replace(" ", ""));
            inputAscii.AddRange(extraCharacters);

            List<int> list = new List<int>();
            for (int i = 0; i < 256; i++)
            {
                list.Add(i);
            }

            int current = 0;
            int skipSize = 0;

            for (int k = 0; k < 64; k++)
            { 
                for (int i = 0; i < inputAscii.Count; i++)
                {
                    List<int> reversed = GetReversed(list, current, inputAscii[i]);

                    for (int j = current; j < current + inputAscii[i]; j++)
                    {
                        list[j % list.Count] = reversed[j - current];
                    }
                    current = (current + inputAscii[i] + skipSize) % list.Count;
                    skipSize++;
                }
            }

            string hash = "";

            for(int i = 0; i < 16; i++)
            {
                int ascii = 0;
                for(int j = 0; j < 16; j++)
                {
                    ascii ^= list[i * 16 + j];
                }
                string hashInc = ascii.ToString("X").ToLower();
                if (hashInc.Length == 1)
                    hashInc = "0" + hashInc;
                hash += hashInc;
            }

            return hash;
        }

        private List<int> GetAscii(string input)
        {
            List<int> ascii = new List<int>();

            for(int i = 0; i < input.Length; i++)
            {
                ascii.Add(input[i]);
            }

            return ascii;
        }
    }
}
