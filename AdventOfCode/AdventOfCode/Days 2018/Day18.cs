using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Days_2018
{
    public class Day18 : IPuzzle
    {
        public bool Active => true;


        List<string> inputLines = System.IO.File.ReadAllLines(@"..\..\Data\2018\input18.txt").ToList();

        public string RunOne()
        {
            int sizeX = inputLines[0].Length;
            int sizeY = inputLines.Count;

            string[,] map = new string[sizeX, sizeY];

            string[,] map2 = new string[sizeX, sizeY];
            

            for(int i = 0; i < sizeX; i++)
            {
                for(int j = 0; j < sizeY; j++)
                {
                    map[j, i] = inputLines[i][j].ToString();
                    map2[j, i] = inputLines[i][j].ToString();
                }
            }

            //PaintMap(map, sizeX, sizeY);

            for (int i = 0; i < 10; i++)
            {
                //if(i % 2 == 0)
                //{
                Update(map2, map, sizeX, sizeY);
                //PaintMap(map2, sizeX, sizeY);
                for(int k = 0;  k < sizeX; k++)
                {
                    for(int j = 0; j < sizeY; j++)
                    {
                        map[k,j] = map2[k,j];
                    }
                }
                //}
                //else
                //{
                //    Update(map, map2, sizeX, sizeY);
                //    PaintMap(map, sizeX, sizeY);
                //}
            }

            int numWoods = 0;
            int numLumbers = 0;

            for (int k = 0; k < sizeX; k++)
            {
                for (int j = 0; j < sizeY; j++)
                {
                    if (map[k, j] == "#")
                        numLumbers++;
                    if (map[k, j] == "|")
                        numWoods++;
                }
            }

             return (numWoods * numLumbers).ToString() ;
        }
        
        private void PaintMap(string[,] map, int sizeX, int sizeY)
        {
            Console.WriteLine();
            for (int i = 0; i < sizeY; i++)
            {
                for (int j = 0; j < sizeX; j++)
                {
                    Console.Write(map[j, i]);
                }
                Console.WriteLine();
            }
            Console.ReadKey();
        }

        private void Update(string[,] map, string[,] map2, int sizeX, int sizeY)
        {
            for(int i = 0; i < sizeX; i++)
            {
                //List<string> adjTrees = new List<string>();
                //List<string> adjLumbers = new List<string>();
                //List<string> adjOpens = new List<string>();
                for(int j = 0; j < sizeY; j++)
                {
                    var list = FillLists(map2, i, j, sizeX, sizeY);
                    if(map2[i, j] == ".")
                    {
                        if(list.Count(x => x == "|") >= 3)
                        {
                            map[i, j] = "|";
                        }
                    }
                    else if(map2[i, j] == "|")
                    {
                        if(list.Count(x => x == "#") >= 3)
                        {
                            map[i, j] = "#";
                        }
                    }
                    else
                    {
                        if(list.Count(x => x == "#") == 0 || list.Count(x => x == "|") == 0)
                        {
                            map[i, j] = ".";
                        }
                    }
                }
            }
        }

        private List<string> FillLists(string[,] map2, int i, int j, int sizeX, int sizeY)
        {
            string diag1 = "";
            string up = "";
            string diag2 = "";
            string right = "";
            string diag3 = "";
            string down = "";
            string diag4 = "";
            string left = "";

            if(i - 1 >= 0 && j - 1 >= 0)
            {
                diag1 = map2[i - 1, j - 1];
            }

            if(j - 1 >= 0)
            {
                up = map2[i, j - 1];
            }

            if(j - 1 >= 0 && i + 1 < sizeX)
            {
                diag2 = map2[i + 1, j - 1];
            }

            if(i + 1 < sizeX)
            {
                right = map2[i + 1, j];
            }

            if(i + 1 < sizeX && j + 1 < sizeY)
            {
                diag3 = map2[i + 1, j + 1];
            }
            
            if(j + 1 < sizeY)
            {
                down = map2[i, j + 1];
            }

            if(i - 1 >= 0 && j + 1 < sizeY)
            {
                diag4 = map2[i - 1, j + 1];
            }

            if(i - 1 >= 0)
            {
                left = map2[i - 1, j];
            }

            return new List<string> { diag1, up, diag2, right, diag3, down, diag4, left };
        }

        public string RunTwo()
        {
            int sizeX = inputLines[0].Length;
            int sizeY = inputLines.Count;

            string[,] map = new string[sizeX, sizeY];

            string[,] map2 = new string[sizeX, sizeY];


            for (int i = 0; i < sizeX; i++)
            {
                for (int j = 0; j < sizeY; j++)
                {
                    map[j, i] = inputLines[i][j].ToString();
                    map2[j, i] = inputLines[i][j].ToString();
                }
            }

            //PaintMap(map, sizeX, sizeY);

            int numWoods = 0;
            int numLumbers = 0;
            //Point p = new Point();
            List<Point> p = new List<Point>();
            for (int i = 0; i < 1000000000; i++)
            {
                //if(i % 2 == 0)
                //{
                Update(map2, map, sizeX, sizeY);
                //PaintMap(map2, sizeX, sizeY);
                for (int k = 0; k < sizeX; k++)
                {
                    for (int j = 0; j < sizeY; j++)
                    {
                        map[k, j] = map2[k, j];
                    }
                }


                numLumbers = 0;
                numWoods = 0;

                for (int k = 0; k < sizeX; k++)
                {
                    for (int j = 0; j < sizeY; j++)
                    {
                        if (map[k, j] == "#")
                            numLumbers++;
                        if (map[k, j] == "|")
                            numWoods++;
                    }
                }
                if(i == 999)
                {

                }
                Point point = new Point(numLumbers, numWoods);
                if (point.X == 306 && point.Y == 569)
                {

                }
                //if(i == 999)
                //{

                //}
                //if (i == 1999)
                //{

                //}
                p.Add(point);
                //}
                //else
                //{
                //    Update(map, map2, sizeX, sizeY);
                //    PaintMap(map, sizeX, sizeY);
                //}
            }


            return (numWoods * numLumbers).ToString();
        }
    }
}
