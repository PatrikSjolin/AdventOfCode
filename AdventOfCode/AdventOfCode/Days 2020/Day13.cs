using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            inputs = System.IO.File.ReadAllLines(@"..\..\Data\2020\input13test.txt").ToList();

            start = int.Parse(inputs[0]);
            List<string> split = inputs[1].Split(',').ToList();

            List<(int, int)> schedule = new List<(int, int)>();

            for (int i = 0; i < split.Count; i++)
            {
                if (split[i] != "x")
                    schedule.Add((i, int.Parse(split[i])));
            }

            long max = schedule.OrderByDescending(x => x.Item2).First().Item2;
            int offset = schedule.OrderByDescending(x => x.Item2).First().Item1;

            for(long i = max; ; i+= max)
            {
                bool success = true;
                for (int j = 0; j < schedule.Count; j++)
                {
                    if ((i - offset + schedule[j].Item1) % schedule[j].Item2 != 0)
                    {
                        success = false;
                        break;
                    }
                    else
                    {

                    }
                }
                if (success)
                {
                    return (i - offset).ToString();
                }
            }



            //long offset = schedule[0].Item2;

            int index = 0;

            for (long i = offset; ; i += offset)
            {
                if (index + 1 < schedule.Count)
                {
                    if (i >= (schedule[index + 1].Item2 * schedule[index].Item2) - schedule[index].Item2)
                    {
                        index++;
                        offset *= schedule[index].Item2;
                    }
                }

                Console.WriteLine(i);
                bool success = true;

                //CHECK
                for(int j = 0; j < schedule.Count; j++)
                {
                    if ((i - schedule[j].Item1) % schedule[j].Item2 != 0)
                    {
                        success = false;
                        break;
                    }
                    else
                    {

                    }
                }
                if (success)
                {
                    return offset.ToString();
                }
            }
            return "";
        }
    }
}
