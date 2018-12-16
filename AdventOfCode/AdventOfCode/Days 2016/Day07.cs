using System;
using System.Collections.Generic;

namespace AdventOfCode.Days_2016
{
    public class Day07 : IPuzzle
    {
        private int taskNumber = 7;

        public bool Active => false;

        public List<string> ReadInput(string path)
        {
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
            List<string> inputList = ReadInput(string.Format("../../Tasks/{0}/input.txt", taskNumber));

            int number = GetTLSAddresses(inputList);

            Console.WriteLine(number);
        }

        private int GetTLSAddresses(List<string> inputList)
        {
            int tls = 0;

            foreach(var input in inputList)
            {
                bool tlsValid = CheckTls(input);

                if (tlsValid)
                {
                    tls++;
                }
            }
            return tls;
        }

        private bool CheckTls(string input)
        {
            var starting = input.Split('[');

            List<string> outside = new List<string>();
            List<string> inside = new List<string>();
            
            foreach(var s in starting)
            {
                var split = s.Split(']');

                if(split.Length == 1)
                {
                    outside.Add(split[0]);
                    continue;
                }

                for(int i = 0; i < split.Length; i++)
                {
                    if (i % 2 == 1)
                        outside.Add(split[i]);
                    else
                        inside.Add(split[i]);
                }
            }

            for(int i = 0; i < inside.Count; i++)
            {
                if (IsValid(inside[i]))
                {
                    return false;
                }
            }

            for(int i = 0; i < outside.Count; i++)
            {
                if (IsValid(outside[i]))
                {
                    return true;
                }
            }

            return false;
        }

        private bool IsValid(string input)
        {
            for (int i = 0; i < input.Length - 3; i++)
            {
                if (input[i] == input[i + 3] &&
                    input[i + 1] == input[i + 2] &&
                    input[i] != input[i+1])
                {
                    return true;
                }
            }
            return false;
        }

        public void GoTwo()
        {
            List<string> inputList = ReadInput(string.Format("../../Tasks/{0}/input.txt", taskNumber));
            int number = CountIps(inputList);
            Console.WriteLine(number);
        }

        private int CountIps(List<string> inputList)
        {
            int ips = 0;

            foreach (var input in inputList)
            {
                bool tlsValid = CheckIps(input);

                if (tlsValid)
                {
                    ips++;
                }
            }
            return ips;
        }

        private bool CheckIps(string input)
        {
            var starting = input.Split('[');

            List<string> outside = new List<string>();
            List<string> inside = new List<string>();

            foreach (var s in starting)
            {
                var split = s.Split(']');

                if (split.Length == 1)
                {
                    outside.Add(split[0]);
                    continue;
                }

                for (int i = 0; i < split.Length; i++)
                {
                    if (i % 2 == 1)
                        outside.Add(split[i]);
                    else
                        inside.Add(split[i]);
                }
            }

            List<string> insideBab = new List<string>();

            for (int i = 0; i < inside.Count; i++)
            {
                insideBab.AddRange(GetBabs(inside[i]));
            }

            List<string> outsideAba = new List<string>();

            for (int i = 0; i < outside.Count; i++)
            {
                outsideAba.AddRange(GetBabs(outside[i]));
            }

            for(int i = 0; i < insideBab.Count; i++)
            {
                if(outsideAba.Contains(insideBab[i][1].ToString() + insideBab[i][0].ToString() + insideBab[i][1].ToString()))
                {
                    return true;
                }
            }

            return false;
        }

        private IEnumerable<string> GetBabs(string input)
        {
            List<string> babs = new List<string>();
            for (int i = 0; i < input.Length - 2; i++)
            {
                if (input[i] == input[i + 2] &&
                    input[i] != input[i + 1])
                {
                    babs.Add(input[i].ToString() + input[i + 1].ToString() + input[i + 2].ToString());
                }
            }

            return babs;
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
