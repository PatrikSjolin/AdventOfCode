using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Days_2018
{
    public class Day05 : IPuzzle
    {
        public bool Active { get => true; }

        private string Reduce(string s)
        {
            string realInput = s;
            StringBuilder sb = new StringBuilder();

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

        private string Reduce2(string s)
        {
            List<string> ss = s.Select(x => x.ToString()).ToList();

            for(int i = 0; i < ss.Count-1; i++)
            {
                if (ss[i].ToLower() == ss[i + 1].ToLower() && ss[i] != ss[i + 1])
                {
                    ss.RemoveAt(i);
                    ss.RemoveAt(i);
                    //ss[i] = "0";
                    //ss[i + 1] = "0";
                    if (i > 0)
                        i -= 2;
                    else
                        i--;
                }
            }

            return string.Join
                (string.Empty, ss.ToArray());
        }

        private string Reduce3(string s)
        {
            List<string> input = s.Select(x => x.ToString()).ToList();
            //input = "dabAcCaCBAcCcaDA";

            int j = 1;

            for (int i = 0; j < input.Count; i++)
            {
                while (input[i] == "0")
                    i++;

                if (i == j)
                    j++;

                if (input[i].ToLower() == input[j].ToLower() && input[i] != input[j])
                {
                    input[i] = "0";
                    input[j] = "0";

                    while (input[i] == "0" && i > 0)
                        i--;
                    i--;
                }
                j++;
            }

            input.RemoveAll(x => x == "0");

            return string.Join
                (string.Empty, input.ToArray());

        }

        private int Reduce4(string s)
        {
            List<string> input = s.Select(x => x.ToString()).ToList();

            int j = 1;
            int count = 0;

            for (int i = 0; j < input.Count; i++)
            {
                while (input[i] == "0")
                    i++;

                if (i == j)
                    j++;

                if (input[i].ToLower() == input[j].ToLower() && input[i] != input[j])
                {
                    count += 2;
                    input[i] = "0";
                    input[j] = "0";

                    while (input[i] == "0" && i > 0)
                        i--;
                    i--;
                }
                j++;
            }

            return s.Count() - count;

        }

        public string RunOne()
        {
            List<string> inputLines = System.IO.File.ReadAllLines(@"..\..\Data\2018\input05.txt").ToList();
            string input = inputLines[0];

            string reduced = Reduce3(input);
            return reduced.Length.ToString();
        }

        public string RunTwo()
        {
            List<string> inputLines = System.IO.File.ReadAllLines(@"..\..\Data\2018\input05.txt").ToList();
            string input = inputLines[0];

            List<string> letters = new List<string>();
            //{ 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };
            for(int i = 0; i < input.Length; i++)
            {
                if (!letters.Contains(input[i].ToString().ToLower()))
                {
                    letters.Add(input[i].ToString().ToLower());
                }
            }

            int count = int.MaxValue;
            for(int i = 0; i < letters.Count; i++)
            {
                string inp = input.Replace(letters[i], "");
                inp = inp.Replace(letters[i].ToUpper(), "");

                string reduced = Reduce3(inp);
                if(reduced.Length < count)
                {
                    count = reduced.Length;
                }
            }

            return count.ToString();
        }
    }
}
