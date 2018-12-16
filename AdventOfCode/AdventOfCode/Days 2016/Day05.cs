using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Days_2016
{
    public class Day05 : IPuzzle
    {
        private int taskNumber = 5;

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
            string input = "uqwqemis";

            string password = "";
            string hash = "";
            int index = 0;
            while(true)
            { 
                hash = CalculateMD5Hash(input + index);
                if (hash.StartsWith("00000"))
                {
                    password += hash[5];
                    if (password.Length == 8) break;
                }
                index++;
            }
            Console.WriteLine(password);
        }

        public string CalculateMD5Hash(string input)
        {
            // step 1, calculate MD5 hash from input

            MD5 md5 = System.Security.Cryptography.MD5.Create();

            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);

            byte[] hash = md5.ComputeHash(inputBytes);

            // step 2, convert byte array to hex string

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }

            return sb.ToString();
        }

        public void GoTwo()
        {
            string input = "uqwqemis";

            string[] password = new string[8];
            string hash = "";
            int index = 0;
            int length = 0;
            while (true)
            {
                hash = CalculateMD5Hash(input + index);
                if (hash.StartsWith("00000"))
                {
                    int position = 0;
                    if(int.TryParse(hash[5].ToString(), out position))
                    {
                        if (position >= 0 && position <= 7)
                        {
                            if(password[position] == null)
                            {
                                password[position] = hash[6].ToString();
                                length++;
                                if (length == 8) break;
                            }
                        }
                    }
                }
                index++;
            }
            string finalPassword = "";
            foreach(string c in password)
            {
                finalPassword += c;
            }
            Console.WriteLine(finalPassword);
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
