using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Days_2016
{
    public class Day21 : IPuzzle
    {
        private int taskNumber = 21;

        public bool Active => false;

        private void SwapPosition(List<string> password, int first, int second)
        {
            string s = password[first].ToString();

            password[first] = password[second];
            password[second] = s;
        }

        private void SwapLetter(List<string> password, int first, int second)
        {
            string s = password[first].ToString();

            password[first] = password[second];
            password[second] = s;
        }

        private void RotateLeft(List<string> password, int number)
        {
            for (int i = 0; i < number; i++)
            {
                string first = password[0];
                password.Add(first);
                password.RemoveAt(0);
            }
        }

        private void RotateRight(List<string> password, int number)
        {
            for (int i = 0; i < number; i++)
            {
                string last = password.Last();
                password.Insert(0, last);
                password.RemoveAt(password.Count() - 1);
            }
        }

        private void RotateBased(List<string> password, int index, bool invert)
        {
            int rotations = 1 + index + (index >= 4 ? 1 : 0);

            if (invert)
            {
                if (index == 0 || index == 1)
                {
                    string first = password[0];
                    password.Add(first);
                    password.RemoveAt(0);
                }
                else if (index == 2)
                {
                    for (int j = 0; j < 2; j++)
                    {
                        string last = password.Last();
                        password.Insert(0, last);
                        password.RemoveAt(password.Count() - 1);
                    }
                }
                else if (index == 3)
                {
                    for (int j = 0; j < 2; j++)
                    {
                        string first = password[0];
                        password.Add(first);
                        password.RemoveAt(0);
                    }
                }
                else if (index == 4)
                {
                    string last = password.Last();
                    password.Insert(0, last);
                    password.RemoveAt(password.Count() - 1);
                }
                else if (index == 5)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        string first = password[0];
                        password.Add(first);
                        password.RemoveAt(0);
                    }
                }
                else if (index == 7)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        string first = password[0];
                        password.Add(first);
                        password.RemoveAt(0);
                    }
                }
            }
            else
            {
                for (int i = 0; i < rotations; i++)
                {
                    string last = password.Last();
                    password.Insert(0, last);
                    password.RemoveAt(password.Count() - 1);
                }
            }
        }

        private void Reverse(List<string> password, int first, int second)
        {
            List<string> reversed = new List<string>();

            for (int i = first; i <= second; i++)
            {
                reversed.Add(password[i]);
            }

            reversed.Reverse();

            for (int i = first; i <= second; i++)
            {
                password[i] = reversed[i - first];
            }
        }

        private void Move(List<string> password, int remove, int insert)
        {
            string s = password[remove];

            password.RemoveAt(remove);
            password.Insert(insert, s);
        }

        public void GoOne()
        {
            List<string> password = new List<string> { "a", "b", "c", "d", "e", "f", "g", "h" };
            List<string> operations = ReadInput();

            foreach (var o in operations)
            {
                string[] split = o.Split(' ');

                string action = split[0];

                if (action.StartsWith("swap"))
                {
                    if (split[1] == "position")
                    {
                        int first = int.Parse(split[2]);
                        int second = int.Parse(split[5]);
                        SwapPosition(password, first, second);
                    }
                    else if (split[1] == "letter")
                    {
                        int first = password.IndexOf(split[2]);
                        int second = password.IndexOf(split[5]);
                        SwapLetter(password, first, second);
                    }
                }
                else if (action.StartsWith("rotate"))
                {
                    if (split[1] == "left")
                    {
                        int number = int.Parse(split[2]);
                        RotateLeft(password, number);
                    }
                    if (split[1] == "right")
                    {
                        int number = int.Parse(split[2]);
                        RotateRight(password, number);
                    }
                    if (split[1] == "based")
                    {
                        int index = password.IndexOf(split[6]);
                        RotateBased(password, index, false);
                    }
                }
                else if (action.StartsWith("reverse"))
                {
                    int first = int.Parse(split[2]);
                    int second = int.Parse(split[4]);
                    Reverse(password, first, second);
                }
                else if (action.StartsWith("move"))
                {
                    int remove = int.Parse(split[2]);
                    int insert = int.Parse(split[5]);

                    Move(password, remove, insert);
                }
            }

            foreach (string s in password)
            {
                Console.Write(s);
            }
            Console.WriteLine();
        }

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

        public void GoTwo()
        {
            bool reverse = true;
            List<string> password;
            List<string> operations = ReadInput();

            if (reverse)
            {
                string s = "fbgdceah";
                password = new List<string>();
                for (int i = 0; i < s.Length; i++)
                {
                    password.Add(s[i].ToString());
                }
                
                operations.Reverse();
            }
            else
            {
                password = new List<string> { "f", "b", "g", "d", "c", "e", "a", "h" };
            }

            foreach (var o in operations)
            {
                string[] split = o.Split(' ');

                string action = split[0];

                if (action.StartsWith("swap"))
                {
                    if (split[1] == "position")
                    {
                        int first = int.Parse(split[2]);
                        int second = int.Parse(split[5]);
                        SwapPosition(password, first, second);
                    }
                    else if (split[1] == "letter")
                    {
                        int first = password.IndexOf(split[2]);
                        int second = password.IndexOf(split[5]);
                        SwapLetter(password, first, second);
                    }
                }
                else if (action.StartsWith("rotate"))
                {
                    if (split[1] == "left")
                    {
                        int number = int.Parse(split[2]);
                        if (reverse)
                        {
                            RotateRight(password, number);
                        }
                        else
                        {
                            RotateLeft(password, number);
                        }
                    }
                    if (split[1] == "right")
                    {

                        int number = int.Parse(split[2]);
                        if (reverse)
                        {
                            RotateLeft(password, number);
                        }
                        else
                        {
                            RotateRight(password, number);
                        }
                    }
                    if (split[1] == "based")
                    {
                        int index = password.IndexOf(split[6]);
                        RotateBased(password, index, reverse);
                    }
                }
                else if (action.StartsWith("reverse"))
                {
                    int first = int.Parse(split[2]);
                    int second = int.Parse(split[4]);
                    Reverse(password, first, second);
                }
                else if (action.StartsWith("move"))
                {
                    int remove = int.Parse(split[2]);
                    int insert = int.Parse(split[5]);

                    if (reverse)
                    {
                        Move(password, insert, remove);
                    }
                    else
                    {
                        Move(password, remove, insert);
                    }
                }
            }

            foreach (string s in password)
            {
                Console.Write(s);
            }
            Console.WriteLine();
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

