using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Days_2016
{
    public class Day02 : IPuzzle
    {
        public bool Active => true;

        public List<string> ReadInput(string path)
        {
            string text = System.IO.File.ReadAllText(path);

            List<string> list = text.Split('\n').ToList();

            for (int i = 0; i < list.Count; i++)
            {
                list[i] = list[i].Replace('\r', ' ');
                list[i] = list[i].TrimEnd();
            }

            return list;
        }

        private static List<Tuple<int, int>> GetCodeFromRows(List<string> inputlist)
        {
            List<int> code = new List<int>();

            Tuple<int, int>[] directions = { new Tuple<int, int>(0, 1), new Tuple<int, int>(1, 0), new Tuple<int, int>(0, -1), new Tuple<int, int>(-1, 0) };

            Dictionary<string, Tuple<int, int>> map = new Dictionary<string, Tuple<int, int>>();
            map.Add("U", directions[0]);
            map.Add("R", directions[1]);
            map.Add("D", directions[2]);
            map.Add("L", directions[3]);

            List<Tuple<int, int>> coordinates = new List<Tuple<int, int>>();

            int currentX = 0;
            int currentY = 0;
            foreach (string s in inputlist)
            {

                foreach (char c in s)
                {
                    currentX += map[c.ToString()].Item1;
                    currentY += map[c.ToString()].Item2;

                    currentX = Math.Min(currentX, 1);
                    currentX = Math.Max(currentX, -1);

                    currentY = Math.Min(currentY, 1);
                    currentY = Math.Max(currentY, -1);
                }
                coordinates.Add(new Tuple<int, int>(currentX, currentY));

            }

            return coordinates;
        }

                private static string CodeFromRowsTwo(List<string> inputList)
        {
            string result = "";
            List<int> code = new List<int>();

            Tuple<int, int>[] directions = { new Tuple<int, int>(0, 1), new Tuple<int, int>(1, 0), new Tuple<int, int>(0, -1), new Tuple<int, int>(-1, 0) };

            Dictionary<string, Tuple<int, int>> map = new Dictionary<string, Tuple<int, int>>();
            map.Add("D", directions[0]);
            map.Add("R", directions[1]);
            map.Add("U", directions[2]);
            map.Add("L", directions[3]);

            List<Tuple<int, int>> coordinates = new List<Tuple<int, int>>();

            Dictionary<Tuple<int, int>, string> mapping = new Dictionary<Tuple<int, int>, string>();
            mapping.Add(new Tuple<int, int>(0, 0), "X");
            mapping.Add(new Tuple<int, int>(1, 0), "X");
            mapping.Add(new Tuple<int, int>(2, 0), "1");
            mapping.Add(new Tuple<int, int>(3, 0), "X");
            mapping.Add(new Tuple<int, int>(4, 0), "X");
            mapping.Add(new Tuple<int, int>(0, 1), "X");
            mapping.Add(new Tuple<int, int>(1, 1), "2");
            mapping.Add(new Tuple<int, int>(2, 1), "3");
            mapping.Add(new Tuple<int, int>(3, 1), "4");
            mapping.Add(new Tuple<int, int>(4, 1), "X");
            mapping.Add(new Tuple<int, int>(0, 2), "5");
            mapping.Add(new Tuple<int, int>(1, 2), "6");
            mapping.Add(new Tuple<int, int>(2, 2), "7");
            mapping.Add(new Tuple<int, int>(3, 2), "8");
            mapping.Add(new Tuple<int, int>(4, 2), "9");
            mapping.Add(new Tuple<int, int>(0, 3), "X");
            mapping.Add(new Tuple<int, int>(1, 3), "A");
            mapping.Add(new Tuple<int, int>(2, 3), "B");
            mapping.Add(new Tuple<int, int>(3, 3), "C");
            mapping.Add(new Tuple<int, int>(4, 3), "X");
            mapping.Add(new Tuple<int, int>(0, 4), "X");
            mapping.Add(new Tuple<int, int>(1, 4), "X");
            mapping.Add(new Tuple<int, int>(2, 4), "D");
            mapping.Add(new Tuple<int, int>(3, 4), "X");
            mapping.Add(new Tuple<int, int>(4, 4), "X");

            int currentX = 0;
            int currentY = 2;

            foreach (string s in inputList)
            {

                foreach (char c in s)
                {
                    int newcurrentX = currentX + map[c.ToString()].Item1;
                    int newcurrentY = currentY + map[c.ToString()].Item2;

                    string value;
                    if (mapping.TryGetValue(new Tuple<int, int>(newcurrentX, newcurrentY), out value))
                    {
                        if (value != "X")
                        {
                            currentX = newcurrentX;
                            currentY = newcurrentY;
                        }
                    }
                }

                result += mapping[new Tuple<int, int>(currentX, currentY)];
            }
            return result;
        }

        private static string GetCodeFromCoordinates(List<Tuple<int, int>> code)
        {
            string result = "";
            foreach (var c in code)
            {
                if (c.Item1 == -1 && c.Item2 == 1) { result += 1; }
                if (c.Item1 == 0 && c.Item2 == 1) { result += 2; }
                if (c.Item1 == 1 && c.Item2 == 1) { result += 3; }
                if (c.Item1 == -1 && c.Item2 == 0) { result += 4; }
                if (c.Item1 == 0 && c.Item2 == 0) { result += 5; }
                if (c.Item1 == 1 && c.Item2 == 0) { result += 6; }
                if (c.Item1 == -1 && c.Item2 == -1) { result += 7; }
                if (c.Item1 == 0 && c.Item2 == -1) { result += 8; }
                if (c.Item1 == 1 && c.Item2 == -1) { result += 9; }
            }

            return result;
        }

        public string RunOne()
        {
            List<string> inputList = ReadInput(@"..\..\Data\2016\input02.txt");
            List<Tuple<int, int>> code = GetCodeFromRows(inputList);

            string codeString = GetCodeFromCoordinates(code);

            string code2 = CodeFromRowsTwo(inputList);

            return codeString;
        }

        public string RunTwo()
        {
            List<string> inputList = ReadInput(@"..\..\Data\2016\input02.txt");
            List<Tuple<int, int>> code = GetCodeFromRows(inputList);

            string codeString = GetCodeFromCoordinates(code);

            string code2 = CodeFromRowsTwo(inputList);

            return code2;
        }
    }
}
