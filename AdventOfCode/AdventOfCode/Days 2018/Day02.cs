using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Days_2018
{
    public class Day02 : IPuzzle
    {
        public string RunOne()
        {
            List<string> input = System.IO.File.ReadAllLines(@"..\..\Data\2018\input02.txt").ToList();

            int totalTwos = 0;
            int totalThrees = 0;
            foreach(var i in input)
            {
                Dictionary<char, int> numbers = new Dictionary<char, int>();

                foreach (char c in i)
                {
                    int count = 0;
                    if(numbers.TryGetValue(c, out count))
                    {
                        numbers[c]++;
                    }
                    else
                    {
                        numbers.Add(c, 1);
                    }
                }

                if (numbers.ContainsValue(2))
                    totalTwos++;
                if (numbers.ContainsValue(3))
                    totalThrees++;
            }
            return (totalThrees * totalTwos).ToString();
        }

        public string RunTwo()
        {
            List<string> input = System.IO.File.ReadAllLines(@"..\..\Data\2018\input02.txt").ToList();

            foreach (var v1 in input)
            {
                foreach (var v2 in input)
                {
                    int errors = 0;
                    int length = v1.Length;
                    for (int i = 0; i < v1.Length; i++)
                    {
                        if (v1[i] != v2[i])
                            errors++;
                        if (errors > 1)
                            break;
                    }
                    if (errors == 1)
                    {
                        for (int i = 0; i < v1.Length; i++)
                        {
                            if (v1[i] != v2[i])
                            {
                                return v1.Remove(i, 1);
                            }
                        }
                    }
                }
            }
            return "";
        }
        public string RunTwoO()
        {
            List<string> lines = System.IO.File.ReadAllLines(@"..\..\Data\2018\input02.txt").ToList();
            var result = "";
            for (int i = 0; i < lines.Count; i++)
            {
                for (int j = i + 1; j < lines.Count; j++)
                {
                    int l = 0;
                    for (int k = 0; k < lines[i].Length; k++)
                    {
                        if (lines[i][k] != lines[j][k])
                            l++;
                        if (l > 1)
                            break;
                    }
                    if (l == 1)
                    {
                        for (int k = 0; k < lines[i].Length; k++)
                        {
                            if (lines[i][k] == lines[j][k])
                                result += lines[i][k];
                        }
                        return result;
                    }
                }
            }
            return "";
        }
    }
}
