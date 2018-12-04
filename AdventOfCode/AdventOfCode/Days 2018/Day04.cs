using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Days_2018
{
    public class SleepTime
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
    public class Day04 : IPuzzle
    {
        SortedDictionary<int, List<SleepTime>> guards = new SortedDictionary<int, List<SleepTime>>();

        public string RunOne()
        {
            List<string> input = System.IO.File.ReadAllLines(@"..\..\Data\2018\input04.txt").ToList();
            SortedDictionary<DateTime, string> inputs = new SortedDictionary<DateTime, string>();

            foreach (var line in input)
            {
                DateTime date = new DateTime(int.Parse(line.Substring(1, 4)), int.Parse(line.Substring(6, 2)), int.Parse(line.Substring(9, 2)), int.Parse(line.Substring(12, 2)), int.Parse(line.Substring(15, 2)), 0);
                inputs.Add(date, line);
            }

            guards = new SortedDictionary<int, List<SleepTime>>();
            int guard = -1;
            SleepTime st = null;
            foreach (var line in inputs)
            {
                if (line.Value.Contains("begins"))
                {
                    guard = int.Parse(line.Value.Split('#')[1].Split(' ')[0]);

                    if (!guards.ContainsKey(guard))
                    {
                        guards.Add(guard, new List<SleepTime>());
                    }
                }
                if (line.Value.Contains("falls"))
                {
                    st = new SleepTime { Start = line.Key };
                    List<SleepTime> sleepTimes = guards[guard];
                    sleepTimes.Add(st);
                }
                if (line.Value.Contains("wakes"))
                {
                    st.End = line.Key.Subtract(new TimeSpan(0, 1, 0));
                }
            }

            Dictionary<int, int> guardSleepTimes = new Dictionary<int, int>();

            foreach (var g in guards)
            {
                guardSleepTimes.Add(g.Key, 0);
                foreach (var sleepTime in g.Value)
                {
                    guardSleepTimes[g.Key] += (int)sleepTime.End.Subtract(sleepTime.Start).TotalMinutes + 1;
                }
            }

            int guardKey = 0;
            int total = 0;
            foreach (var gs in guardSleepTimes)
            {
                if (gs.Value > total)
                {
                    guardKey = gs.Key;
                    total = gs.Value;
                }
            }

            int guardMostSleep = guardKey;
            int mostMinute = 0;

            var sleepy = guards[guardMostSleep];

            Dictionary<int, int> minutes = new Dictionary<int, int>();

            foreach (var s in sleepy)
            {
                for (int i = s.Start.Minute; i <= s.End.Minute; i++)
                {
                    if (!minutes.ContainsKey(i))
                        minutes.Add(i, 0);
                    minutes[i]++;
                }
            }
            int finalKey = -1;
            foreach (var v in minutes)
            {
                if (v.Value > mostMinute)
                {
                    mostMinute = v.Value;
                    finalKey = v.Key;
                }
            }

            return (guardMostSleep * finalKey).ToString();
        }

        public string RunTwo()
        {
            Dictionary<int, int> minutes = new Dictionary<int, int>();

            int occurences = 0;
            int mostOccuringMinute = -1;
            int guardKey = 0;
            foreach (var guard in guards)
            {
                minutes.Clear();
                foreach (var sleepTime in guard.Value)
                {
                    for (int i = sleepTime.Start.Minute; i <= sleepTime.End.Minute; i++)
                    {
                        if (!minutes.ContainsKey(i))
                            minutes.Add(i, 0);
                        minutes[i]++;
                    }
                }
                foreach (var v in minutes)
                {
                    if (v.Value > occurences)
                    {
                        occurences = v.Value;
                        mostOccuringMinute = v.Key;
                        guardKey = guard.Key;
                    }
                }
            }

            return (guardKey * mostOccuringMinute).ToString();
        }
    }
}
