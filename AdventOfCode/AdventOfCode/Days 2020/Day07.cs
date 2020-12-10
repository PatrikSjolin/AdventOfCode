using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Days_2020
{
    public class Day07 : IPuzzle
    {
        public bool Active => true;

        private List<string> inputs;

        public string RunOne()
        {
            inputs = System.IO.File.ReadAllLines(@"..\..\Data\2020\input07.txt").ToList();

            Dictionary<string, List<string>> bagColors = new Dictionary<string, List<string>>();
            
            foreach (var input in inputs)
            {
                List<string> split = input.Split(' ').ToList();
                bool first = true;
                string firstBag = "";

                for (int i = 0; i < split.Count; i++)
                {
                    if (split[i].StartsWith("bag"))
                    {
                        string bag = split[i - 2] + " " + split[i - 1];

                        if (first)
                        {
                            firstBag = bag;
                            first = false;
                        }
                        else
                        {
                            if(!bagColors.ContainsKey(bag))
                            {
                                bagColors.Add(bag, new List<string> { firstBag });
                            }
                            else
                            {
                                bagColors[bag].Add(firstBag);
                            }
                        }

                    }
                }
            }

            CountBags(bagColors, "shiny gold");

            return colors.Count.ToString();
        }

        List<string> colors = new List<string>();
        private void CountBags(Dictionary<string, List<string>> bagColors, string color)
        {
            List<string> bags;
            bagColors.TryGetValue(color, out bags);
            if (bags == null) return;

            foreach(var b in bags)
            {
                if (!colors.Contains(b))
                    colors.Add(b);
                CountBags(bagColors, b);
            }
        }

        public string RunTwo()
        {
            Dictionary<string, List<string>> bagColors = new Dictionary<string, List<string>>();
            foreach (var input in inputs)
            {
                List<string> split = input.Split(' ').ToList();
                bool first = true;
                string firstBag = "";

                for (int i = 0; i < split.Count; i++)
                {
                    if (split[i].StartsWith("bag"))
                    {
                        string bag = split[i - 2] + " " + split[i - 1];

                        if (first)
                        {
                            firstBag = bag;
                            first = false;
                        }
                        else
                        {
                            if (!split[i - 3].Contains("contain"))
                            {
                                if (!bagColors.ContainsKey(firstBag))
                                {
                                    bagColors.Add(firstBag, new List<string> { split[i - 3] + ";" + bag });
                                }
                                else
                                {
                                    bagColors[firstBag].Add(split[i - 3] + ";" + bag);
                                }
                            }
                        }
                    }
                }
            }

            CountBagsTwo(bagColors, 1, "shiny gold");

            return count.ToString();
        }

        int count = 0;
        private void CountBagsTwo(Dictionary<string, List<string>> bagColors, int amount, string color)
        {
            List<string> bags;
            bagColors.TryGetValue(color, out bags);

            if (bags == null) return;

            foreach(string bag in bags)
            {
                List<string> split = bag.Split(';').ToList();
                int number = int.Parse(split[0]);
                string newColor = split[1];

                count += amount * number;
                CountBagsTwo(bagColors, number * amount, newColor);
            }
        }
    }
}
