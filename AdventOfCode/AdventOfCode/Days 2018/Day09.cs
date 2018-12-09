using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Days_2018
{
    public class Day09 : IPuzzle
    {
        public string RunOne()
        {
            //List<string> inputLines = System.IO.File.ReadAllLines(@"..\..\Data\2018\input09.txt").ToList();
            int numPlayers = 426;
            int lastMarbleScore = 72058 * 100;

            Dictionary<int, int> playerScores = new Dictionary<int, int>();

            for (int i = 0; i < numPlayers; i++)
            {
                playerScores.Add(i, 0);
            }

            int score = 0;
            int marble = 0;
            int playerTurn = 0;

            List<int> marbles = new List<int>();
            marbles.Add(marble);
            marble++;

            int index = 1;

            while (marble != lastMarbleScore)
            {
                marbles.Insert(index, marble);
                if((marble + 1) % 23 != 0)
                    index = ((index + 1) % marbles.Count) + 1;

                playerTurn++;
                playerTurn = playerTurn % numPlayers;
                marble++;

                if (marble % 23 == 0)
                {
                    score = marble;
                    int newIndex = 0;
                    if(index - 7 < 0)
                    {
                        newIndex = marbles.Count + (index - 7);
                    }
                    else
                    {
                        newIndex = index - 7;
                    }
                    int m = marbles[newIndex];
                    marbles.RemoveAt(newIndex);
                    index = newIndex;
                    score += m;
                    playerScores[playerTurn] += score;
                    marble++;
                    playerTurn++;
                    playerTurn = playerTurn % numPlayers;
                    index = ((index + 1) % marbles.Count) + 1;
                }
            }

            int highest = 0;
            
            foreach(var v in playerScores)
            {
                if(v.Value > highest)
                {
                    highest = v.Value;
                }
            }

            return highest.ToString();

        }

        public string RunTwo()
        {
            //List<string> inputLines = System.IO.File.ReadAllLines(@"..\..\Data\2018\input09.txt").ToList();

            return "";
        }
    }
}
