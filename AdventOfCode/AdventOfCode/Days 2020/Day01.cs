﻿using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Days_2020
{
    public class Day01 : IPuzzle
    {
        public bool Active => true;

        private List<int> inputs;

        public string RunOne()
        {
            inputs = System.IO.File.ReadAllLines(@"..\..\Data\2020\input01.txt").Select(x => int.Parse(x)).ToList();
            for(int i = 0; i < inputs.Count; i++)
            {
                int numberOne = inputs[i];
                for (int j = i + 1; j < inputs.Count; j++)
                {
                    int numberTwo = inputs[j];

                    if (numberOne + numberTwo == 2020)
                        return (numberOne * numberTwo).ToString();
                }
            }
            return "";
        }

        public string RunTwo()
        {
            for (int i = 0; i < inputs.Count; i++)
            {
                int numberOne = inputs[i];
                for (int j = i + 1; j < inputs.Count; j++)
                {
                    int numberTwo = inputs[j];
                    if (numberTwo + numberOne > 2020)
                        continue;
                    for (int k = j + 1; k < inputs.Count; k++)
                    {
                        int numberThree = inputs[k];

                        if (numberOne + numberTwo + numberThree == 2020)
                            return (numberOne * numberTwo * numberThree).ToString();
                    }
                }
            }
            return "";
        }
    }
}
