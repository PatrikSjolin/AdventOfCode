using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Days_2018
{
    public class Day07 : IPuzzle
    {
        public bool Active { get => true; }

        List<string> inputLines = System.IO.File.ReadAllLines(@"..\..\Data\2018\input07.txt").ToList();

        public string RunOne()
        {
            List<string> nodes = new List<string>();
            Dictionary<string, List<string>> edges = new Dictionary<string, List<string>>();
            List<KeyValuePair<string, string>> lines = new List<KeyValuePair<string, string>>();

            SortedDictionary<string, int> queue = new SortedDictionary<string, int>();

            foreach (var v in inputLines)
            {
                List<string> split = v.Split(' ').ToList();

                string s1 = split[1];
                string s2 = split[7];

                lines.Add(new KeyValuePair<string, string>(s1, s2));

                if (!edges.ContainsKey(s1))
                    edges.Add(s1, new List<string>());

                edges[s1].Add(s2);

                if (!nodes.Contains(s1))
                    nodes.Add(s1);

                if (!nodes.Contains(s2))
                    nodes.Add(s2);
            }

            string result = "E";

            while (result.Length != nodes.Count)
            {
                SortedDictionary<string, int> possible = new SortedDictionary<string, int>();
                if(!result.Contains("P"))
                    possible.Add("P", 0);
                if(!result.Contains("X"))
                    possible.Add("X", 0);

                foreach (var v in result)
                {
                    var d = edges[v.ToString()];
                    foreach (var q in d)
                    {
                        if (!possible.ContainsKey(q) && !result.Contains(q))
                        {
                            bool ok = true;
                            foreach(var line in lines.Where(x => x.Value == q))
                            {
                                if (!result.Contains(line.Key))
                                    ok = false;
                            }
                            if(ok)
                                possible.Add(q, 0);
                        }
                    }
                }


                result += possible.First().Key;
            }


            return result;
        }

        public string RunTwo()
        {
            List<string> nodes = new List<string>();
            Dictionary<string, List<string>> edges = new Dictionary<string, List<string>>();
            List<KeyValuePair<string, string>> lines = new List<KeyValuePair<string, string>>();

            SortedDictionary<string, int> queue = new SortedDictionary<string, int>();

            foreach (var v in inputLines)
            {
                List<string> split = v.Split(' ').ToList();

                string s1 = split[1];
                string s2 = split[7];

                lines.Add(new KeyValuePair<string, string>(s1, s2));

                if (!edges.ContainsKey(s1))
                    edges.Add(s1, new List<string>());

                edges[s1].Add(s2);

                if (!nodes.Contains(s1))
                    nodes.Add(s1);

                if (!nodes.Contains(s2))
                    nodes.Add(s2);
            }

            string result = "";
            int totalSeconds = 1;

            int secondsExtra = 60;
            Dictionary<string, int> times = new Dictionary<string, int>();


            int e = Convert.ToChar("E") - 64;
            int p = Convert.ToChar("P") - 64;
            int x = Convert.ToChar("X") - 64;

            times.Add("E", e + secondsExtra);
            times.Add("P", p + secondsExtra);
            times.Add("X", x + secondsExtra);

            int workers = 5;

            while (result.Length != nodes.Count)
            {
                foreach (var v in times.ToList())
                {
                    times[v.Key]--;
                }
                foreach (var v in times.ToList())
                {
                    if (v.Value == 0)
                    {
                        result += v.Key;
                        times.Remove(v.Key);
                    }
                }
                if (result.Length == nodes.Count)
                    break;

                SortedDictionary<string, int> possible = new SortedDictionary<string, int>();

                if (!result.Contains("P"))
                    possible.Add("P", 0);
                if (!result.Contains("X"))
                    possible.Add("X", 0);

                foreach (var v in result)
                {
                    var d = edges[v.ToString()];
                    foreach (var q in d)
                    {
                        if (!possible.ContainsKey(q) && !result.Contains(q))
                        {
                            bool ok = true;
                            foreach (var line in lines.Where(xx => xx.Value == q))
                            {
                                if (!result.Contains(line.Key))
                                    ok = false;
                            }
                            if (ok)
                                possible.Add(q, 0);
                        }
                    }
                    foreach(var pos in possible)
                    {
                        //string next = possible.First(xy => !result.Contains(xy.Key)).Key;

                        if (times.Count < workers && !times.ContainsKey(pos.Key))
                        {
                            times.Add(pos.Key, Convert.ToChar(pos.Key) - 64 + secondsExtra);
                        }

                    }

                }
                totalSeconds++;
            }


            return totalSeconds.ToString();
        }
    }
}
