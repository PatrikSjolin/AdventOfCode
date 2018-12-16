using System;
using System.Collections.Generic;

namespace AdventOfCode.Days_2016
{
    public class Day20 : IPuzzle
    {
        private int taskNumber = 20;

        public bool Active => false;

        public List<string> ReadInput()
        {
            string path = string.Format("../../Tasks/{0}/input.txt", taskNumber);

            List<string> inputList = new List<string>();
            string line = "";
            System.IO.StreamReader file = new System.IO.StreamReader(path);
            while ((line = file.ReadLine()) != null)
            {
                inputList.Add(line);
            }

            return inputList;
        }

        public void GoOne()
        {
            List<string> input = ReadInput();

            long lowestIp = GetLowestIp(input);

            Console.WriteLine(lowestIp);
        }

        private long GetLowestIp(List<string> input)
        {
            List<Tuple<long, long>> numbers = new List<Tuple<long, long>>();

            foreach (string s in input)
            {
                string[] split = s.Split('-');
                numbers.Add(new Tuple<long, long>(long.Parse(split[0]), long.Parse(split[1])));
            }

            numbers = OptimizeNumbers(numbers);

            SortedDictionary<long, long> sorted = new SortedDictionary<long, long>();

            foreach(var t in numbers)
            {
                sorted.Add(t.Item1, t.Item2);
            }

            long previous = 0;

            foreach(var s in sorted)
            {
                if(previous == 0)
                {
                    previous = s.Value;
                    continue;
                }

                if(s.Key - previous - 1 > 0)
                {
                    return previous + 1;
                }
                previous = s.Value;
            }
            return 0;
        }

        private List<Tuple<long, long>> OptimizeNumbers(List<Tuple<long, long>> numbers)
        {
            while (true)
            {
                bool found = false;
                for(int i = 0; i < numbers.Count; i++)
                {
                    for(int j = i+1; j < numbers.Count; j++)
                    {
                        if(numbers[i].Item1 <= numbers[j].Item1 && numbers[i].Item2 >= numbers[j].Item2)
                        {
                            numbers.Remove(numbers[j]);
                            found = true;
                            break;
                        }

                        if (numbers[j].Item1 <= numbers[i].Item1 && numbers[j].Item2 >= numbers[i].Item2)
                        {
                            numbers.Remove(numbers[i]);
                            found = true;
                            break;
                        }

                        if (numbers[i].Item2 >= numbers[j].Item1 && numbers[i].Item2 <= numbers[j].Item2)
                        {
                            numbers[i] = new Tuple<long, long>(numbers[i].Item1, numbers[j].Item2);
                            numbers.Remove(numbers[j]);
                            found = true;
                            break;
                        }
                        else if(numbers[j].Item2 >= numbers[i].Item1 && numbers[j].Item2 <= numbers[i].Item2)
                        {
                            numbers[j] = new Tuple<long, long>(numbers[j].Item1, numbers[i].Item2);
                            numbers.Remove(numbers[i]);
                            found = true;
                            break;
                        }
                    }

                }

                if (!found)
                {
                    break;
                }
            }
            return numbers;
        }

        private bool IsValid(long i, List<Tuple<long, long>> input)
        {
            foreach(Tuple<long, long> s in input)
            {
                if(i < s.Item1 || i > s.Item2)
                {

                }
                else
                {
                    return false;
                }
            }

            return true;
        }

        public void GoTwo()
        {
            List<string> input = ReadInput();

            long count = 0;
            List<Tuple<long, long>> numbers = new List<Tuple<long, long>>();

            foreach (string s in input)
            {
                string[] split = s.Split('-');
                numbers.Add(new Tuple<long, long>(long.Parse(split[0]), long.Parse(split[1])));
            }

            numbers = OptimizeNumbers(numbers);

            SortedDictionary<long, long> sorted = new SortedDictionary<long, long>();

            foreach (var t in numbers)
            {
                sorted.Add(t.Item1, t.Item2);
            }

            long previous = 0;
            foreach (var s in sorted)
            {
                if (previous == 0)
                {
                    previous = s.Value;
                    continue;
                }
                count += (s.Key - previous - 1);
                previous = s.Value;
            }

            Console.WriteLine(count);
        }

        public string RunOne()
        {
            throw new NotImplementedException();
        }

        public string RunTwo()
        {
            throw new NotImplementedException();
        }
    }
}
