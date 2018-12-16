using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Days_2016
{
    public class Day09 : IPuzzle
    {
        private int taskNumber = 9;

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
            string inputList = ReadInput(string.Format("../../Tasks/{0}/input.txt", taskNumber))[0];

            int answer = CountDecompressedLength(inputList);

            Console.WriteLine(answer);
        }

        private int CountDecompressedLength(string input)
        {
            string result = "";
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] == '(')
                {
                    string marker = "";
                    while (true)
                    {
                        i++;
                        if (input[i] == ')')
                        {
                            break;
                        }
                        else
                        {
                            marker += input[i];
                        }
                    }

                    int read = int.Parse(marker.Split('x')[0]);
                    int repeat = int.Parse(marker.Split('x')[1]);
                    i++;
                    string text = input.Substring(i, read);
                    string toAdd = "";
                    for (int j = 0; j < repeat; j++)
                    {
                        toAdd += text;
                    }

                    result += toAdd;
                    i += read - 1;
                }
                else
                {
                    result += input[i];
                }
            }

            return result.Length;
        }

        public void GoTwo()
        {
            string inputList = ReadInput(string.Format("../../Tasks/{0}/input.txt", taskNumber))[0];
            CountDecompressedLength2(inputList);

            outputFile.Dispose();
            FileInfo info = new FileInfo("../../WriteLines.txt");


            Console.WriteLine(info.Length);
        }

        StreamWriter outputFile = null;
        //new StreamWriter("../../WriteLines.txt");

        public bool Active => false;

        private string CountDecompressedLength2(string input)
        {
            //long number = 0;
            
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] == '(')
                {
                    string marker = "";
                    while (true)
                    {
                        i++;
                        if (input[i] == ')')
                        {
                            break;
                        }
                        else
                        {
                            marker += input[i];
                        }
                    }

                    int read = int.Parse(marker.Split('x')[0]);
                    int repeat = int.Parse(marker.Split('x')[1]);
                    i++;
                    string text = input.Substring(i, read);
                    string toAdd = "";
                    for (int j = 0; j < repeat; j++)
                    {
                        toAdd += text;
                    }
                    outputFile.Write(CountDecompressedLength2(toAdd));
                    i += read - 1;
                }
                else
                {
                    outputFile.Write(input[i]);
                }
            }
            return "";
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
