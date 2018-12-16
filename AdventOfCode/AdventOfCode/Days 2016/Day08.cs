using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Days_2016
{
    public class Day08 : IPuzzle
    {
        private int taskNumber = 8;

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
            bool[,] screen = ReturnScreen(inputList);

            int pixelsLit = 0;
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 50; j++)
                {
                    pixelsLit += screen[i, j] ? 1 : 0;
                }
            }

            Console.WriteLine(pixelsLit);
        }

        private bool[,] ReturnScreen(List<string> inputList)
        {
            bool[,] screen = new bool[6, 50];

            foreach(var input in inputList)
            {
                if (input.StartsWith("rect"))
                {
                    string operation = input.Split(' ')[1];
                    int rows = int.Parse(operation.Split('x')[0]);
                    int columns = int.Parse(operation.Split('x')[1]);
                    for(int i = 0; i < columns; i++)
                    {
                        for(int j = 0; j < rows; j++)
                        {
                            screen[i, j] = true;
                        }
                    }
                }
                else
                {
                    //List<string> operation = new List<string>();
                    List<string> operation = input.Split(' ').ToList();
                    string rotate = operation[1];
                    if(rotate == "column")
                    {
                        int index = int.Parse(operation[2].Split('=')[1]);
                        int number = int.Parse(operation[4]);

                        bool[] previousColumn = new bool[6];

                        for (int i = 0; i < 6; i++)
                        {
                            previousColumn[i] = screen[i, index];
                        }

                        for (int i = 0; i < 6; i++)
                        {
                            screen[(i + number) % 6, index] = previousColumn[i % 6];
                        }
                    }
                    else
                    {
                        int index = int.Parse(operation[2].Split('=')[1]);
                        int number = int.Parse(operation[4]);

                        bool[] previousRow = new bool[50];

                        for(int i = 0; i < 50; i++)
                        {
                            previousRow[i] = screen[index, i];
                        }

                        for(int i = 0; i < 50; i++)
                        {
                            screen[index, (i + number) % 50] = previousRow[i % 50];
                        }
                    }
                }
            }



            return screen;
        }

        public void GoTwo()
        {
            List<string> inputList = ReadInput(string.Format("../../Tasks/{0}/input.txt", taskNumber));
            bool[,] screen = ReturnScreen(inputList);

            for(int i = 0; i < 6; i++)
            {
                for(int j = 0; j < 50; j++)
                {
                    Console.ForegroundColor = screen[i, j] ? ConsoleColor.Green : ConsoleColor.White;
                    Console.Write(screen[i, j] ? "X" : "0");
                }
                Console.WriteLine();
            }
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
