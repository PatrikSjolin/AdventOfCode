using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Days_2020
{
    public class Day13 : IPuzzle
    {
        public bool Active => true;

        private List<string> inputs;

        int start;
        List<int> busses;

        public string RunOne()
        {
            inputs = System.IO.File.ReadAllLines(@"..\..\Data\2020\input13.txt").ToList();

            start = int.Parse(inputs[0]);
            List<string> split = inputs[1].Split(',').ToList();

            busses = new List<int>();

            for (int i = 0; i < split.Count; i++)
            {
                if (split[i] != "x")
                    busses.Add(int.Parse(split[i]));
            }

            List<(int, int)> departrues = new List<(int, int)>();

            for (int i = 0; i < busses.Count; i++)
            {
                int bus = busses[i];
                int busCount = bus;

                while (busCount < start)
                {
                    busCount += bus;
                }
                departrues.Add((busCount, bus));
            }

            departrues = departrues.OrderBy(x => x.Item1).ToList();

            return ((departrues[0].Item1 - start) * departrues[0].Item2).ToString();
        }

        public string RunTwo()
        {
            List<string> split = inputs[1].Split(',').ToList();

            List<(int, int)> schedule = new List<(int, int)>();

            for (int i = 0; i < split.Count; i++)
            {
                if (split[i] != "x")
                    schedule.Add((i, int.Parse(split[i])));
            }

            return FindTime(schedule);
        }

        private string FindTime(List<(int, int)> schedule)
        {
            long counter = schedule[schedule.Count - 1].Item2;

            int offset = schedule[schedule.Count - 1].Item1;
            long m = counter;
            for (int i = schedule.Count - 1; i > 0; i--)
            {
                //while (counter % schedule[i - 1].Item2 != (offset - schedule[i - 1].Item1))
                while ((counter - (offset - schedule[i - 1].Item1)) % schedule[i - 1].Item2 != 0)
                {
                    counter += m;
                }
                m *= schedule[i - 1].Item2;
            }

            return (counter - offset).ToString();
        }
    }
}
