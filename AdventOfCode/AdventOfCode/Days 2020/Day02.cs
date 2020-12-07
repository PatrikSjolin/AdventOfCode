using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Days_2020
{
    public class Day02 : IPuzzle
    {
        public bool Active => true;

        public string RunOne()
        {
            List<string> inputs = System.IO.File.ReadAllLines(@"..\..\Data\2020\input02.txt").ToList();

            int valid = 0;

            for (int i = 0; i < inputs.Count; i++)
            {
                List<string> split = inputs[i].Split(' ').ToList();
                int low = int.Parse(split[0].Split('-')[0]);
                int high = int.Parse(split[0].Split('-')[1]);

                char letter = split[1].First();

                string password = split[2];

                int occurs = 0;

                for (int j = 0; j < password.Length; j++)
                {
                    if (password[j] == letter)
                        occurs++;
                }
                if (occurs >= low && occurs <= high)
                    valid++;
            }

            return valid.ToString();
        }

        public string RunTwo()
        {
            List<string> inputs = System.IO.File.ReadAllLines(@"..\..\Data\2020\input02.txt").ToList();

            int valid = 0;

            for (int i = 0; i < inputs.Count; i++)
            {
                List<string> split = inputs[i].Split(' ').ToList();

                int low = int.Parse(split[0].Split('-')[0]);
                int high = int.Parse(split[0].Split('-')[1]);

                string letter = split[1].First().ToString();

                string password = split[2];

                if (KeyMatch(low - 1, password, letter) ^ KeyMatch(high - 1, password, letter))
                {
                    valid++;
                }
            }

            return valid.ToString();
        }

        private bool KeyMatch(int index, string password, string letter)
        {
            return password[index].ToString() == letter;
        }
    }
}
