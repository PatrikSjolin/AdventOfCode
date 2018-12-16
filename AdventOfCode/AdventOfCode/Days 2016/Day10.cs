using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Days_2016
{
    public class Bot
    {
        public int Id { get; set; }

        public int RobotLowId { get; set; }
        public bool OutputLow { get; set; }

        public int RobotHighId { get; set; }
        public bool OutputHigh { get; set; }
        public int? Value1 { get; set; }

        public int? Value2 { get; set; }

        public bool HasGiven { get; set; }
    }

    public class Day10 : IPuzzle
    {
        private int taskNumber = 10;

        public bool Active => false;

        public void GoOne()
        {
            List<string> inputList = ReadInput();
            GetBotInstructions(inputList);
            Console.WriteLine();
        }

        private void GetBotInstructions(List<string> inputList)
        {
            SortedDictionary<int, Bot> bots = new SortedDictionary<int, Bot>();
            //List<int> values = new List<int>();
            SortedDictionary<int, List<int>> output = new SortedDictionary<int, List<int>>();

            foreach (string s in inputList)
            {
                if (s.StartsWith("bot"))
                {
                    List<string> split = s.Split(' ').ToList();
                    int botId = int.Parse(split[1]);
                    Bot bot;
                    if (bots.TryGetValue(botId, out bot))
                    {
                        if (split[5] == "output")
                        {
                            bot.OutputLow = true;
                        }
                        bot.RobotLowId = int.Parse(split[6]);

                        if (split[10] == "output")
                        {
                            bot.OutputHigh = true;
                        }
                        bot.RobotHighId = int.Parse(split[11]);
                    }
                    else
                    {
                        bot = new Bot { Id = botId };
                        if (split[5] == "output")
                        {
                            bot.OutputLow = true;
                        }
                        bot.RobotLowId = int.Parse(split[6]);
                        if (split[10] == "output")
                        {
                            bot.OutputHigh = true;
                        }
                        bot.RobotHighId = int.Parse(split[11]);
                        bots.Add(botId, bot);
                    }
                }
                else
                {
                    List<string> split = s.Split(' ').ToList();
                    int botId = int.Parse(split[5]);
                    int value = int.Parse(split[1]);
                    //values.Add(value);
                    Bot bot;
                    if (bots.TryGetValue(botId, out bot))
                    {
                        if (bot.Value1.HasValue)
                        {
                            bot.Value2 = value;
                        }
                        else
                        {
                            bot.Value1 = value;
                        }
                    }
                    else
                    {
                        bot = new Bot { Id = botId, Value1 = value };
                        bots.Add(botId, bot);
                    }
                }
            }

            while (true)
            {
                CalculateBotActions(bots, output);

                bool allFinished = true;

                foreach(var bot in bots)
                {
                    if (!bot.Value.HasGiven)
                    {
                        allFinished = false;
                        break;
                    }
                }

                if (allFinished)
                {
                    break;
                }
            }

            foreach(var b in bots)
            {
                if(b.Value.Value1.Value == 61 && b.Value.Value2.Value == 17)
                {
                    Console.WriteLine(b.Key);
                }
            }

            int product = 1;

            for (int i = 0; i < 3; i++)
            {
                foreach(var element in output[i])
                {
                    product *= element;
                }
            }

            Console.WriteLine(product);
        }

        private void CalculateBotActions(SortedDictionary<int, Bot> bots, SortedDictionary<int, List<int>> output)
        {
            foreach (var bot in bots)
            {
                if (bot.Value.Value1.HasValue && bot.Value.Value2.HasValue && !bot.Value.HasGiven)
                {
                    bot.Value.HasGiven = true;
                    int lowValue = Math.Min(bot.Value.Value1.Value, bot.Value.Value2.Value);
                    int maxValue = Math.Max(bot.Value.Value1.Value, bot.Value.Value2.Value);

                    if (!bot.Value.OutputLow)
                    {
                        var botLow = bots[bot.Value.RobotLowId];

                        if (botLow.Value1.HasValue && !botLow.Value2.HasValue)
                        {
                            botLow.Value2 = lowValue;
                        }
                        else if (!botLow.Value1.HasValue)
                        {
                            botLow.Value1 = lowValue;
                        }
                    }
                    else
                    {
                        List<int> listOfOutputs;
                        if (output.TryGetValue(bot.Value.RobotLowId, out listOfOutputs))
                        {
                            listOfOutputs.Add(lowValue);
                        }
                        else
                        {
                            listOfOutputs = new List<int> { lowValue };
                            output.Add(bot.Value.RobotLowId, listOfOutputs);
                        }
                    }

                    if (!bot.Value.OutputHigh)
                    {
                        var botHigh = bots[bot.Value.RobotHighId];

                        if (botHigh.Value1.HasValue && !botHigh.Value2.HasValue)
                        {
                            botHigh.Value2 = maxValue;
                        }
                        else if (!botHigh.Value1.HasValue)
                        {
                            botHigh.Value1 = maxValue;
                        }
                    }
                    else
                    {
                        List<int> listOfOutputs;
                        if (output.TryGetValue(bot.Value.RobotHighId, out listOfOutputs))
                        {
                            listOfOutputs.Add(maxValue);
                        }
                        else
                        {
                            listOfOutputs = new List<int> { maxValue };
                            output.Add(bot.Value.RobotHighId, listOfOutputs);
                        }
                    }
                }

                //if ((bot.Value.Value1 == 17 && bot.Value.Value2 == 61) ||
                //    (bot.Value.Value1 == 61 && bot.Value.Value2 == 17))
                //{
                //    break;
                //}
            }
        }

        public void GoTwo()
        {

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
