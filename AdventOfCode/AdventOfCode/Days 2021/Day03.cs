using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Days_2021
{
    class Day03 : IPuzzle
    {
        public bool Active => true;
        private List<string> inputs;

        public string RunOne()
        {
            inputs = System.IO.File.ReadAllLines(@"..\..\Data\2021\input03.txt").ToList();

            int gamma = 0;
            int epsilon = 0;

            string finalGamma = "";
            string finalEpsilon = "";

            for (int i = 0; i < inputs[0].Length; i++)
            {
                int zeroes = 0;
                int ones = 0;

                for (int j = 0; j < inputs.Count; j++)
                {
                    if (inputs[j][i] == '0')
                        zeroes++;
                    else
                        ones++;
                }

                if (zeroes > ones)
                {
                    finalGamma += "0";
                    finalEpsilon += "1";
                }
                else
                {
                    finalGamma += "1";
                    finalEpsilon += "0";
                }
            }

            gamma = Convert.ToInt32(finalGamma, 2);
            epsilon = Convert.ToInt32(finalEpsilon, 2);

            return (gamma * epsilon).ToString();
        }

        public string RunTwo()
        {
            inputs = System.IO.File.ReadAllLines(@"..\..\Data\2021\input03.txt").ToList();

            int gamma = 0;
            int epsilon = 0;

            string finalGamma = "";
            string finalEpsilon = "";

            int index = 0;
            while (inputs.Count > 1)
            {
                int zeroes = 0;
                int ones = 0;

                for (int j = 0; j < inputs.Count; j++)
                {
                    if (inputs[j][index] == '0')
                        zeroes++;
                    else
                        ones++;
                }


                if (zeroes > ones)
                {
                    inputs = inputs.Where(x => x[index] == '0').ToList();
                }
                else
                {
                    inputs = inputs.Where(x => x[index] == '1').ToList();
                }
                index++;
            }

            finalGamma = inputs[0];
            inputs = System.IO.File.ReadAllLines(@"..\..\Data\2021\input03.txt").ToList();

            index = 0;
            while (inputs.Count > 1)
            {
                int zeroes = 0;
                int ones = 0;

                for (int j = 0; j < inputs.Count; j++)
                {
                    if (inputs[j][index] == '0')
                        zeroes++;
                    else
                        ones++;
                }

                if (zeroes <= ones)
                {
                    inputs = inputs.Where(x => x[index] == '0').ToList();
                }
                else
                {
                    inputs = inputs.Where(x => x[index] == '1').ToList();
                }
                index++;
            }


            finalEpsilon = inputs[0];

            gamma = Convert.ToInt32(finalGamma, 2);
            epsilon = Convert.ToInt32(finalEpsilon, 2);

            return (gamma * epsilon).ToString();
        }
    }
}
