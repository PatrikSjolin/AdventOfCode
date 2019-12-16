using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Days_2019
{
    public class Day16 : IPuzzle
    {
        public bool Active => true;

        public string RunOne()
        {
            string input = System.IO.File.ReadAllLines(@"..\..\Data\2019\input16.txt")[0];

            int[] inputs = input.Select(x => (int)char.GetNumericValue(x)).ToArray();
            List<int> basePattern = new List<int> { 0, 1, 0, -1 };
            List<int> newList = new List<int>();

            int phases = 100;

            for (int phase = 0; phase < phases; phase++)
            {
                for (int i = 1; i <= inputs.Length; i++)
                {
                    int sum = 0;

                    for (int j = 0; j < inputs.Length; j++)
                    {

                        int repeating = basePattern[((j + 1) / i) % 4];

                        sum += inputs[j] * repeating;
                    }
                    newList.Add(Math.Abs(sum % 10));
                }
                inputs = newList.ToArray();
                newList.Clear();
            }

            string result = "";

            for (int i = 0; i < 8; i++)
            {
                result += inputs[i];
            }

            return result;
        }

        public string RunTwo()
        {
            string input = System.IO.File.ReadAllLines(@"..\..\Data\2019\input16.txt")[0];

            List<int> inputs = input.Select(x => (int)char.GetNumericValue(x)).ToList();
            

            int offset = int.Parse(input.Substring(0, 7));

            List<int> repeat = new List<int>();

            for (int i = 0; i < 10000; i++)
            {
                repeat.AddRange(inputs);
            }


            List<int> repeatedInput = repeat.ToList();

            int phases = 100;
            repeatedInput.RemoveRange(0, offset);
            int[] newList = new int[repeatedInput.Count];


            repeatedInput.Reverse();

            int[] workArray = repeatedInput.ToArray();

            for (int phase = 0; phase < phases; phase++)
            {
                int sum = 0;
                for (int i = 0; i < workArray.Length; i++)
                {
                    sum += workArray[i];
                    newList[i] = (sum % 10);
                }
                workArray = newList.ToArray();
            }

            repeatedInput = workArray.ToList();
            repeatedInput.Reverse();

            string result = "";

            for (int i = 0; i < 8; i++)
            {
                result += repeatedInput[i];
            }

            return result;
        }
    }
}
