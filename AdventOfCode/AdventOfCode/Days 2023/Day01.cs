using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Days_2023
{
    public class Day01 : IPuzzle
    {
        public bool Active => true;

        private List<string> inputs;

        public string RunOne()
        {
            inputs = System.IO.File.ReadAllLines(@"..\..\Data\2023\input01.txt").ToList();

            int sum = 0;

            for(int i = 0; i < inputs.Count; i++)
            {
                bool firstFound = false;
                string first = "";
                string second = "";
                for(int j = 0; j < inputs[i].Length; j++)
                {
                    if (Char.IsNumber(inputs[i][j]))
                    {
                        second = inputs[i][j] + "";
                        if (!firstFound)
                        {
                            first += inputs[i][j];
                            firstFound = true;
                        }
                    }
                }
                sum += int.Parse(first + second);
            }

            return sum.ToString();
        }

        private Dictionary<string, int> letters = new Dictionary<string, int> { { "one", 1 }, { "two", 2 }, { "three", 3 }, { "four", 4 }, { "five", 5 }, { "six", 6 }, { "seven", 7 }, { "eight", 8 }, { "nine", 9 } };

        private string GetFirstDigit(string s)
        {
            bool firstFound = false;
            string first = "";
            string second = "";

            for(int i = 0; i < s.Length; i++)
            {
                if (Char.IsNumber(s[i]))
                {
                    if (!firstFound)
                    {
                        first = s[i] + "";
                        firstFound = true;
                    }
                    second = s[i] + "";
                }

                string restOfString = s.Substring(i);

                foreach (var word in letters.Keys)
                {
                    if (restOfString.StartsWith(word))
                    {
                        if (!firstFound)
                        {
                            first = letters[word] + "";
                            firstFound = true;
                        }
                        second = letters[word] + "";
                    }
                }
            }

            return first + second;
        }

        public string RunTwo()
        {
            inputs = System.IO.File.ReadAllLines(@"..\..\Data\2023\input01.txt").ToList();

            int sum = 0;

            for (int i = 0; i < inputs.Count; i++)
            {
                sum += int.Parse(GetFirstDigit(inputs[i]));
            }

            return sum.ToString();
        }
    }
}
