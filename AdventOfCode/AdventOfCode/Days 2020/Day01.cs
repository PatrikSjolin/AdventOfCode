using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Days_2020
{
    public class Day01 : IPuzzle
    {
        public bool Active => true;

        public string RunOne()
        {
            List<int> inputs = System.IO.File.ReadAllLines(@"..\..\Data\2020\input01.txt").Select(x => int.Parse(x)).ToList();
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
            List<int> inputs = System.IO.File.ReadAllLines(@"..\..\Data\2020\input01.txt").Select(x => int.Parse(x)).ToList();
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
