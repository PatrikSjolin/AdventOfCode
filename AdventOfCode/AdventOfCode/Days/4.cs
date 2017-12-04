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
            List<string> inputLines = System.IO.File.ReadAllLines(@"..\..\input3.txt").ToList();

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

            List<string> inputLines = System.IO.File.ReadAllLines(@"..\..\input3.txt").ToList();

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
                        if(Utilities.IsAnagram(tests[i], tests[j]))
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
    }
}
