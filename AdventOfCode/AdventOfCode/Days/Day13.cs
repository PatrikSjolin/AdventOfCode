using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Days
{
    public class Day13 : IPuzzle
    {
        public string RunOne()
        {
            List<string> inputLines = System.IO.File.ReadAllLines(@"..\..\input13.txt").ToList();

            Dictionary<int, int[]> dic = new Dictionary<int, int[]>();
            foreach (var input in inputLines)
            {
                List<string> split = input.Replace(" ", "").Split(':').ToList();
                dic.Add(int.Parse(split[0]), new int[int.Parse(split[1])]);
            }

            Dictionary<int, int[]> nodes = new Dictionary<int, int[]>();

            for (int i = 0; i < (dic.Last().Key + 1); i++)
            {
                if (!dic.ContainsKey(i))
                {
                    nodes.Add(i, new int[0]);
                }
                else
                {
                    nodes.Add(i, dic[i]);
                }
            }

            int severity = 0;

            for (int i = 0; i < nodes.Count; i++)
            {
                int scanPos = GetScannerPos(i, nodes, 0);

                if (scanPos == 0)
                {
                    severity += (nodes[i].Length) * i;
                }
            }

            return severity.ToString();
        }

        public string RunTwo()
        {
            List<string> inputLines = System.IO.File.ReadAllLines(@"..\..\input13.txt").ToList(); Dictionary<int, int[]> dic = new Dictionary<int, int[]>();
            foreach (var input in inputLines)
            {
                List<string> split = input.Replace(" ", "").Split(':').ToList();
                dic.Add(int.Parse(split[0]), new int[int.Parse(split[1])]);
            }

            Dictionary<int, int[]> nodes = new Dictionary<int, int[]>();

            for (int i = 0; i < (dic.Last().Key + 1); i++)
            {
                if (!dic.ContainsKey(i))
                {
                    nodes.Add(i, new int[0]);
                }
                else
                {
                    nodes.Add(i, dic[i]);
                }
            }

            int delay = 0;

            bool clean = true;

            while (true)
            {
                for (int i = 0; i < nodes.Count; i++)
                {
                    int scanPos = GetScannerPos(i, nodes, delay);
                    if (scanPos == 0)
                    {
                        clean = false;
                        delay++;
                        break;
                    }
                }

                if (clean)
                    break;
                else
                    clean = true;
            }

            return delay.ToString();
        }

        int GetScannerPos(int position, Dictionary<int, int[]> nodes, int delay)
        {
            if (nodes[position].Length > 0)
            {
                int scanPos = 0;
                int stepsPerCycle = nodes[position].Length - 1;
                int scannerSteps = position + delay;

                if ((scannerSteps / stepsPerCycle) % 2 == 0)
                {
                    scanPos = scannerSteps % stepsPerCycle;
                }
                else
                {
                    scanPos = stepsPerCycle - (scannerSteps % stepsPerCycle);
                }

                return scanPos;
            }
            else
            {
                return -1;
            }
        }
    }
}
