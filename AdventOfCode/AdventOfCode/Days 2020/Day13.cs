using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Days_2020
{
    public class Day13 : IPuzzle
    {
        public bool Active => false;

        private List<string> inputs;

        int start;
        List<int> busses;

        public string RunOne()
        {
            inputs = System.IO.File.ReadAllLines(@"..\..\Data\2020\input13.txt").ToList();

            start = int.Parse(inputs[0]);
            List<string> split = inputs[1].Split(',').ToList();

            busses = new List<int>();

            for(int i = 0; i < split.Count; i++)
            {
                if (split[i] != "x")
                    busses.Add(int.Parse(split[i]));
            }

            List<(int,int)> departrues = new List<(int, int)>();

            //int minimum = 99999999999999999;

            for(int i = 0; i < busses.Count; i++)
            {
                int bus = busses[i];
                int busCount = bus;

                while(busCount < start)
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
            inputs = System.IO.File.ReadAllLines(@"..\..\Data\2020\input13test2.txt").ToList();
            List<string> split = inputs[1].Split(',').ToList();

            List<(int, int)> schedule = new List<(int, int)>();

            for (int i = 0; i < split.Count; i++)
            {
                if (split[i] != "x")
                    schedule.Add((i, int.Parse(split[i])));
            }

            long N = 1;
            foreach ((int, int) bus in schedule)
                N *= bus.Item2;
            //return BruteForce(schedule);
            return SearchBySieving(schedule);
        }

        private string SearchBySieving(List<(int, int)> schedule)
        {
            //Sorting on modulo actions
            schedule = schedule.OrderByDescending(x => x.Item1).ToList();

            long m = 1;
            long current = 0;
            bool first = true;
            for(int i = 0; i < schedule.Count - 1; i++)
            {
                //bus ids
                m *= schedule[i].Item2;
                //modulo (offset bus times)
                long a = schedule[i].Item1;

                long next = schedule[i + 1].Item1;

                if (first)
                {
                    current = a + m;
                    first = false;
                }
                while (((current) % a) != next)
                {
                    current = current + m;
                }
            }

            return current.ToString();
        }

        private string BruteForce(List<(int, int)> schedule)
        {
            long max = schedule.OrderByDescending(x => x.Item2).First().Item2;
            int offset = schedule.OrderByDescending(x => x.Item2).First().Item1;

            for (long i = max; ; i += max)
            {
                bool success = true;
                for (int j = 0; j < schedule.Count; j++)
                {
                    if ((i - offset + schedule[j].Item1) % schedule[j].Item2 != 0)
                    {
                        success = false;
                        break;
                    }
                }
                if (success)
                {
                    return (i - offset).ToString();
                }
            }
        }
    }
}
