using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Days_2018
{
    public class Day05 : IPuzzle
    {
        private string Reduce(string s)
        {
            string realInput = s;
            StringBuilder sb = new StringBuilder();
            //string newString = "";

            bool removed = true;
            while (removed)
            {
                removed = false;
                for (int i = 0; i < realInput.Length; i++)
                {
                    if (realInput.Length - 1 == i)
                    {
                        sb.Append(realInput[i]);
                        continue;
                    }
                    if (realInput[i].ToString().ToLower() == realInput[i + 1].ToString().ToLower() && realInput[i] != realInput[i + 1])
                    {
                        removed = true;
                        i++;
                    }
                    else
                    {
                        sb.Append(realInput[i]);
                    }
                }
                realInput = sb.ToString();
                sb.Clear();
            }

            return realInput;
        }

        public string RunOne()
        {
            List<string> input = System.IO.File.ReadAllLines(@"..\..\Data\2018\input05.txt").ToList();
            string realInput = input[0];

            string reduced = Reduce(realInput);
            return reduced.Length.ToString();
        }

        public string RunTwo()
        {
            List<string> input = System.IO.File.ReadAllLines(@"..\..\Data\2018\input05.txt").ToList();
            string realInput = input[0];
            //string realInput = "dabAcCaCBAcCcaDA";

            List<string> letters = new List<string>();
            //{ 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };
            for(int i = 0; i < realInput.Length; i++)
            {
                if (!letters.Contains(realInput[i].ToString().ToLower()))
                {
                    letters.Add(realInput[i].ToString().ToLower());
                }
            }

            StringBuilder sb = new StringBuilder();
            //string test = "";
            int count = 3763732;
            for(int i = 0; i < letters.Count; i++)
            {
                for(int j = 0; j < realInput.Length; j++)
                {
                    if(realInput[j].ToString().ToLower() == letters[i])
                    {

                    }
                    else
                    {
                        sb.Append(realInput[j]);
                    }
                }
                string reduced = Reduce(sb.ToString());
                if(reduced.Length < count)
                {
                    count = reduced.Length;
                }
                sb.Clear();
            }

            return count.ToString();
        }
    }
}
