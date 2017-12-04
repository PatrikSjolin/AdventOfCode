using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Days
{
    public class _4 : IPuzzle
    {
        public void RunOne()
        {
            List<string> inputLines = System.IO.File.ReadAllLines(@"C:\Users\patri\Documents\GitHub\AdventOfCode\AdventOfCode\AdventOfCode\input3.txt").ToList();

            int valid = 0;

            foreach(var line in inputLines)
            {
                List<string> tests = line.Split(' ').ToList();
                Dictionary<string, string> ok = new Dictionary<string, string>();

                for(int i = 0; i < tests.Count; i++)
                {
                    try
                    {
                        ok.Add(tests[i], "hej");
                        if(i == tests.Count - 1)
                        {
                            valid++;
                        }
                    }
                    catch(Exception e)
                    {
                        break;
                    }
                }
            }
            Console.WriteLine(valid);
        }

        public void RunTwo()
        {

            List<string> inputLines = System.IO.File.ReadAllLines(@"C:\Users\patri\Documents\GitHub\AdventOfCode\AdventOfCode\AdventOfCode\input3.txt").ToList();

            int valid = 0;

            foreach (var line in inputLines)
            {
                bool end = false;
                List<string> tests = line.Split(' ').ToList();
                for(int i = 0; i < tests.Count; i++)
                {
                    for (int j = 0; j < tests.Count; j++)
                    {
                        if (i == j) continue;
                        if(IsAnagram(tests[i], tests[j]))
                        {
                            end = true;
                            break;
                        }
                    }

                    if (end) break;

                    if (i == tests.Count - 1)
                    {
                        valid++;
                    }
                }
            }
            Console.WriteLine(valid);
        }

        public bool IsAnagram(string first, string second)
        {
            if (first.Length != second.Length)
                return false;

            if (first == second)
                return true;//or false: Don't know whether a string counts as an anagram of itself

            Dictionary<char, int> pool = new Dictionary<char, int>();
            foreach (char element in first.ToCharArray()) //fill the dictionary with that available chars and count them up
            {
                if (pool.ContainsKey(element))
                    pool[element]++;
                else
                    pool.Add(element, 1);
            }
            foreach (char element in second.ToCharArray()) //take them out again
            {
                if (!pool.ContainsKey(element)) //if a char isn't there at all; we're out
                    return false;
                if (--pool[element] == 0) //if a count is less than zero after decrement; we're out
                    pool.Remove(element);
            }
            return pool.Count == 0;
        }
    }
}
