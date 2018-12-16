using System;
using System.Collections.Generic;

namespace AdventOfCode.Days_2016
{
    public class Day03 : IPuzzle
    {
        public bool Active => true;

        static List<Triangle> ReadInput(string path)
        {
            List<Triangle> triangles = new List<Triangle>();
            string line = "";
            System.IO.StreamReader file = new System.IO.StreamReader(path);
            while ((line = file.ReadLine()) != null)
            {
                var split = line.Split(' ');
                List<string> correct = new List<string>();
                foreach (var s in split)
                {
                    if (s != "")
                    {
                        correct.Add(s);
                    }
                }
                triangles.Add(new Triangle
                {
                    A = int.Parse(correct[0]),
                    B = int.Parse(correct[1]),
                    C = int.Parse(correct[2])
                });
            }

            return triangles;
        }

        static List<Triangle> ReadInputThreeByColumns(string path)
        {
            List<int> triangleLine = new List<int>();
            string line = "";
            System.IO.StreamReader file = new System.IO.StreamReader(path);
            while ((line = file.ReadLine()) != null)
            {
                var split = line.Split(' ');
                List<string> correct = new List<string>();
                foreach (var s in split)
                {
                    if (s != "")
                    {
                        correct.Add(s);
                    }
                }
                triangleLine.Add(int.Parse(correct[0]));
                triangleLine.Add(int.Parse(correct[1]));
                triangleLine.Add(int.Parse(correct[2]));
            }

            List<Triangle> triangles = new List<Triangle>();

            for (int i = 0; i < triangleLine.Count; i += 9)
            {
                for (int j = 0; j < 3; j++)
                {
                    triangles.Add(new Triangle
                    {
                        A = triangleLine[i + j],
                        B = triangleLine[i + j + 3],
                        C = triangleLine[i + j + 6]
                    });
                }
            }

            return triangles;
        }


        private static bool IsValidTriangle(Triangle t)
        {
            return
                t.A + t.B > t.C && t.B + t.C > t.A && t.A + t.C > t.B;
        }

        public string RunOne()
        {
            List<Triangle> inputList = ReadInput(@"..\..\Data\2016\input03.txt");

            int countValidTriangles = 0;

            foreach (Triangle t in inputList)
            {
                if (IsValidTriangle(t))
                {
                    countValidTriangles++;
                }
            }

            return countValidTriangles.ToString();
        }

        public string RunTwo()
        {
            List<Triangle> inputList = ReadInputThreeByColumns(@"..\..\Data\2016\input03.txt");

            int countValidTriangles = 0;

            foreach (Triangle t in inputList)
            {
                if (IsValidTriangle(t))
                {
                    countValidTriangles++;
                }
            }

            return countValidTriangles.ToString();
        }

        public class Triangle
        {
            public int A { get; set; }
            public int B { get; set; }
            public int C { get; set; }
        }
    }
}
