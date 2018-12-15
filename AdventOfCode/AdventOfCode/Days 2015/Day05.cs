using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Days_2015
{
    public class Day05 : IPuzzle
    {
        public bool Active { get => true; }

        public string RunOne()
        {
            List<string> inputLines = System.IO.File.ReadAllLines(@"..\..\Data\2015\input05.txt").ToList();
            //List<string> inputLines = new List<string> { "aaa" };
            int niceStrings = 0;
            foreach (var s in inputLines)
            {
                if(s.Count(x => x == 'a' || x == 'e' || x == 'i' || x == 'o' || x == 'u') >= 3 &&
                    ContainsPair(s) &&
                    !s.Contains("ab") &&
                    !s.Contains("cd") &&
                    !s.Contains("pq") &&
                    !s.Contains("xy"))
                {
                    niceStrings++;
                }
            }
            return niceStrings.ToString();
        }

        private bool ContainsPair(string s)
        {
            for(int i = 0; i < s.Length - 1; i++)
            {
                if (s[i] == s[i+1])
                    return true;
            }
            return false;
        }

        public string RunTwo()
        {
            List<string> inputLines = System.IO.File.ReadAllLines(@"..\..\Data\2015\input05.txt").ToList();
            int niceStrings = 0;
            foreach (var s in inputLines)
            {
                if (ContainsDoublePair(s) && ContainsRepeating(s))
                {
                    niceStrings++;
                }
            }
            return niceStrings.ToString();
        }

        private bool ContainsDoublePair(string s)
        {
            for(int i = 0; i < s.Length - 1; i ++)
            {
                int count = 0;
                string pair = s.Substring(i, 2);

                for(int j = i + 2; j < s.Length - 1; j++)
                {
                    if (pair == s.Substring(j, 2))
                        return true;
                }
            }
            return false;
        }

        private bool ContainsRepeating(string s)
        {
            for(int i = 0; i < s.Length - 2; i++)
            {
                if (s[i] == s[i + 2])
                    return true;
            }
            return false;
        }
    }
}
