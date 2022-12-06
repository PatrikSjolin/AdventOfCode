using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Days_2022
{
    internal class Day06 : IPuzzle
    {
        public bool Active => true;
        private List<string> inputs;


        public string RunOne()
        {
            inputs = System.IO.File.ReadAllLines(@"..\..\Data\2022\input06.txt").ToList();

            string message = inputs[0];

            HashSet<char> duplicateChecker = new HashSet<char>();

            int codeLength = 4;

            for(int i = 0; i < message.Length; i++)
            {
                for (int j = 0; j < codeLength; j++)
                {
                    if (duplicateChecker.Add(message[i + j]))
                    {
                        if(j == 3)
                        {
                            return (i + j + 1).ToString();
                        }
                    }
                    else
                    {
                        break;
                    }
                }

                duplicateChecker.Clear();
            }

            return "";
        }

        public string RunTwo()
        {
            inputs = System.IO.File.ReadAllLines(@"..\..\Data\2022\input06.txt").ToList();

            string message = inputs[0];

            HashSet<char> duplicateChecker = new HashSet<char>();

            int codeLength = 14;

            for (int i = 0; i < message.Length; i++)
            {
                for (int j = 0; j < codeLength; j++)
                {
                    if (duplicateChecker.Add(message[i + j]))
                    {
                        if (j == 13)
                        {
                            return (i + j + 1).ToString();
                        }
                    }
                    else
                    {
                        break;
                    }
                }

                duplicateChecker.Clear();
            }

            return "";
        }
    }
}
