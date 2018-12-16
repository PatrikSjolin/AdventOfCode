using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Days_2016
{
    public class Day06 : IPuzzle
    {
        private int taskNumber = 6;

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
            List<string> columns = new List<string>();

            
            string word = "";
            for (int i = 0; i < 8; i++)
            {
                Dictionary<string, int> charCount = new Dictionary<string, int>();
                
                foreach(string s in inputList)
                {
                    int value = 0;
                    string character = s[i].ToString();
                    if (charCount.TryGetValue(character, out value))
                    {
                        value++;
                        charCount[character] = value;
                    }
                    else
                    {
                        charCount.Add(character, 1);
                    }
                }
                int maxOccurence = 0;
                string bestChar = "";
                foreach(var test in charCount)
                {
                    if(test.Value > maxOccurence)
                    {
                        maxOccurence = test.Value;
                        bestChar = test.Key;
                    }
                }
                word += bestChar;
            }

            Console.WriteLine(word);
        }

        public void GoTwo()
        {
            List<string> inputList = ReadInput(string.Format("../../Tasks/{0}/input.txt", taskNumber));
            
            string word = "";
            for (int i = 0; i < 8; i++)
            {
                Dictionary<string, int> charCount = new Dictionary<string, int>();

                foreach (string s in inputList)
                {
                    int value = 0;
                    string character = s[i].ToString();
                    if (charCount.TryGetValue(character, out value))
                    {
                        value++;
                        charCount[character] = value;
                    }
                    else
                    {
                        charCount.Add(character, 1);
                    }
                }
                int minOccurence = 9999;
                string bestChar = "";
                foreach (var test in charCount)
                {
                    if (test.Value < minOccurence)
                    {
                        minOccurence = test.Value;
                        bestChar = test.Key;
                    }
                }
                word += bestChar;
            }

            Console.WriteLine(word);
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
