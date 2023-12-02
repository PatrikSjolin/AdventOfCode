using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Days_2023
{
    public class Game
    {
        public int ID { get; set; }
        public List<Session> Sessions { get; set; }
    }

    public class Session
    {
        public int Red { get; set; }
        public int Green { get; set; }
        public int Blue { get; set; }
    }

    public class Day02 : IPuzzle
    {
        public bool Active => true;

        private List<string> inputs;

        int maxBlue = 14;
        int maxGreen = 13;
        int maxRed = 12;

        public string RunOne()
        {
            inputs = System.IO.File.ReadAllLines(@"..\..\Data\2023\input02.txt").ToList();
            List<Game> allGames = new List<Game>();

            int validGame = 0;

            foreach(var game in inputs)
            {
                Game current = new Game();
                List<string> games = game.Split(':').ToList();
                current.ID = int.Parse(games[0].Split(' ')[1]);
                current.Sessions = new List<Session>();

                List<string> sessions = games[1].Split(';').ToList();

                foreach(var session in sessions)
                {
                    Session s = new Session();
                    List<string> round = session.Split(',').ToList();

                    foreach(var r in round)
                    {
                        List<string> roundInfo = r.Split(' ').ToList();
                        int number = int.Parse(roundInfo[1]);
                        string color = roundInfo[2];

                        if(color == "blue")
                        {
                            s.Blue += number;
                        }
                        else if(color == "green")
                        {
                            s.Green += number;
                        }
                        else if(color == "red")
                        {
                            s.Red += number;
                        }
                    }

                    current.Sessions.Add(s);
                }

                allGames.Add(current);
            }

            foreach(var game in allGames)
            {
                bool validSession = true;
                foreach(var session in game.Sessions)
                {
                    if(session.Red <= maxRed && session.Blue <= maxBlue && session.Green <= maxGreen)
                    {
                    }
                    else
                    {
                        validSession = false;
                        break;
                    }
                }

                if(validSession)
                {
                    validGame += game.ID;
                }
            }

            return validGame.ToString();
        }

        public string RunTwo()
        {
            inputs = System.IO.File.ReadAllLines(@"..\..\Data\2023\input02.txt").ToList();
            List<Game> allGames = new List<Game>();

            foreach (var game in inputs)
            {
                Game current = new Game();
                List<string> games = game.Split(':').ToList();
                current.ID = int.Parse(games[0].Split(' ')[1]);
                current.Sessions = new List<Session>();

                List<string> sessions = games[1].Split(';').ToList();

                foreach (var session in sessions)
                {
                    Session s = new Session();
                    List<string> round = session.Split(',').ToList();

                    foreach (var r in round)
                    {
                        List<string> roundInfo = r.Split(' ').ToList();
                        int number = int.Parse(roundInfo[1]);
                        string color = roundInfo[2];

                        if (color == "blue")
                        {
                            s.Blue += number;
                        }
                        else if (color == "green")
                        {
                            s.Green += number;
                        }
                        else if (color == "red")
                        {
                            s.Red += number;
                        }
                    }

                    current.Sessions.Add(s);
                }

                allGames.Add(current);
            }

            int addPowers = 0;

            foreach (var game in allGames)
            {
                int maximumRed = 0;
                int maximumGreen = 0;
                int maximumBlue = 0;

                foreach(var session in game.Sessions)
                {
                    if(session.Red > maximumRed)
                        maximumRed = session.Red;
                    if (session.Blue > maximumBlue)
                        maximumBlue = session.Blue;
                    if (session.Green > maximumGreen)
                        maximumGreen = session.Green;
                }

                addPowers += maximumBlue * maximumGreen * maximumRed;
            }

            return addPowers.ToString();
        }
    }
}
