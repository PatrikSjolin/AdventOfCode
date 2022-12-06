using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Days_2022
{
    internal class Day02 : IPuzzle
    {
        public bool Active => true;
        private List<string> inputs;

        Dictionary<string, int> baseScore = new Dictionary<string, int> { { "X", 1}, { "Y", 2 }, { "Z", 3 } };

        public string RunOne()
        {
            inputs = System.IO.File.ReadAllLines(@"..\..\Data\2022\input02.txt").ToList();

            int score = 0; 

            for(int i = 0; i < inputs.Count; i++)
            {
                string computerMove = inputs[i].Split(' ')[0];
                string playerMove = inputs[i].Split(' ')[1];

                score += baseScore[playerMove];

                if ((playerMove == "X" && computerMove == "A") || (playerMove == "Y" && computerMove == "B") || (playerMove == "Z" && computerMove == "C"))
                    score += 3;
                else if (playerMove == "X" && computerMove == "B")
                    score += 0;
                else if (playerMove == "X" && computerMove == "C")
                    score += 6;
                else if (playerMove == "Y" && computerMove == "A")
                    score += 6;
                else if (playerMove == "Y" && computerMove == "C")
                    score += 0;
                else if (playerMove == "Z" && computerMove == "A")
                    score += 0;
                else if (playerMove == "Z" && computerMove == "B")
                    score += 6;
            }
            return score.ToString();
        }

        public string RunTwo()
        {
            inputs = System.IO.File.ReadAllLines(@"..\..\Data\2022\input02.txt").ToList();

            int score = 0;

            for (int i = 0; i < inputs.Count; i++)
            {
                string computerMove = inputs[i].Split(' ')[0];
                string playerMove = inputs[i].Split(' ')[1];

                if(playerMove == "X")
                {
                    if(computerMove == "A")
                    {
                        score += 3;
                    }
                    if (computerMove == "B")
                    {
                        score += 1;
                    }
                    if (computerMove == "C")
                    {
                        score += 2;
                    }
                }
                if(playerMove == "Y")
                {
                    score += 3;
                    if (computerMove == "A")
                    {
                        score += 1;
                    }
                    if (computerMove == "B")
                    {
                        score += 2;
                    }
                    if (computerMove == "C")
                    {
                        score += 3;
                    }
                }
                if (playerMove == "Z")
                {
                    score += 6;
                    if (computerMove == "A")
                    {
                        score += 2;
                    }
                    if (computerMove == "B")
                    {
                        score += 3;
                    }
                    if (computerMove == "C")
                    {
                        score += 1;
                    }
                }

                //score += baseScore[playerMove];

                //if ((playerMove == "X" && computerMove == "A") || (playerMove == "Y" && computerMove == "B") || (playerMove == "Z" && computerMove == "C"))
                //    score += 3;
                //else if (playerMove == "X" && computerMove == "B")
                //    score += 0;
                //else if (playerMove == "X" && computerMove == "C")
                //    score += 6;
                //else if (playerMove == "Y" && computerMove == "A")
                //    score += 6;
                //else if (playerMove == "Y" && computerMove == "C")
                //    score += 0;
                //else if (playerMove == "Z" && computerMove == "A")
                //    score += 0;
                //else if (playerMove == "Z" && computerMove == "B")
                //    score += 6;
            }
            return score.ToString();
        }
    }
}
