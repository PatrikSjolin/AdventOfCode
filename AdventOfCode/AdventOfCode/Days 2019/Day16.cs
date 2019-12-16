﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Days_2019
{
    public class Day16 : IPuzzle
    {
        public bool Active => false;

        public string RunOne()
        {
            string input = System.IO.File.ReadAllLines(@"..\..\Data\2019\input16.txt")[0];

            List<int> inputs = input.Select(x => (int)char.GetNumericValue(x)).ToList();
            List<int> basePattern = new List<int> { 0, 1, 0, -1 };
            List<int> newList = new List<int>();

            int phases = 4;

            for (int phase = 0; phase < phases; phase++)
            {
                for (int i = 1; i <= inputs.Count; i++)
                {
                    int sum = 0;

                    for (int j = 0; j < inputs.Count; j++)
                    {

                        int repeating = basePattern[((j + 1) / i) % 4];

                        sum += inputs[j] * repeating;
                    }
                    newList.Add(Math.Abs(sum % 10));
                }
                inputs = newList.ToList();
                newList.Clear();
            }

            string result = "";

            for(int i = 0; i < 8; i++)
            {
                result += inputs[i];
            }

            return result;
        }

        public string RunTwo()
        {
            string input = System.IO.File.ReadAllLines(@"..\..\Data\2019\input16.txt")[0];

            List<int> inputs = input.Select(x => (int)char.GetNumericValue(x)).ToList();
            List<int> basePattern = new List<int> { 0, 1, 0, -1 };
            List<int> newList = new List<int>();

            int offset = int.Parse(input.Substring(0, 7));

            List<int> repeatedInput = new List<int>();

            for(int i = 0; i < 10000; i++)
            {
                repeatedInput.AddRange(inputs);
            }

            int phases = 100;

            HashSet<List<int>> cache = new HashSet<List<int>>();

            for (int phase = 0; phase < phases; phase++)
            {
                for (int i = 1; i <= repeatedInput.Count; i++)
                {
                    int sum = 0;

                    for (int j = 0; j < repeatedInput.Count; j++)
                    {

                        int repeating = basePattern[((j + offset) / i) % 4];

                        if(j % 650 == 0)
                        {

                        }

                        sum += repeatedInput[j] * repeating;
                    }
                    newList.Add(Math.Abs(sum % 10));
                }

                if (!cache.Contains(newList))
                {
                    cache.Add(newList.ToList());
                }
                else
                {

                }

                repeatedInput = newList.ToList();
                newList.Clear();
            }

            string result = "";

            for (int i = 0; i < 8; i++)
            {
                result += repeatedInput[i];
            }

            return result;
        }
    }
}
