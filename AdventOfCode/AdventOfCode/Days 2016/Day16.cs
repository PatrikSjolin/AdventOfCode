using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Days_2016
{
    public class Day16 : IPuzzle
    {
        public bool Active => false;

        public void GoOne()
        {
            string input = "11101000110010100";
            int lengthOfDisk = 272;

            while(input.Length < lengthOfDisk)
            {
                string b = "";

                foreach (var c in input)
                {
                    b = (c == '1' ? 0 : 1) + b;
                }

                input = input + "0" + b;
            }

            string checksum = "";

            input = input.Substring(0, lengthOfDisk);

            while(checksum.Length % 2 == 0)
            {
                checksum = "";
                for(int i = 0; i < input.Length; i+= 2)
                {
                    checksum = checksum + (input[i] == input[i + 1] ? "1" : "0");
                }

                input = checksum;
            }

            Console.WriteLine(checksum);
        }

        public void GoTwo()
        {
            string input = "11101000110010100";
            int lengthOfDisk = 35651584;

            while (input.Length < lengthOfDisk)
            {
                char[] arr = input.ToCharArray();
                Array.Reverse(arr);

                string b = new string(arr);
                b = b.Replace('1', '2');
                b = b.Replace('0', '1');
                b = b.Replace('2', '0');

                input = input + "0" + b;
            }

            List<char> checksum = new List<char>();

            input = input.Substring(0, lengthOfDisk);

            List<char> finalArray = input.ToList();

            while (checksum.Count % 2 == 0)
            {
                checksum = new List<char>();
                for (int i = 0; i < finalArray.Count; i += 2)
                {
                    checksum.Add(finalArray[i] == finalArray[i + 1] ? '1' : '0');
                }

                finalArray = checksum;
            }

            string answer = new string(checksum.ToArray());

            Console.WriteLine(answer);
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
