using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Days_2019
{
    public class Day14 : IPuzzle
    {
        public bool Active => true;

        public string RunOne()
        {
            List<string> inputs = System.IO.File.ReadAllLines(@"..\..\Data\2019\input14.txt").ToList();

            Dictionary<Tuple<string, int>, List<Tuple<string, int>>> recipies = new Dictionary<Tuple<string, int>, List<Tuple<string, int>>>();

            Dictionary<string, int> resources = new Dictionary<string, int>();

            foreach(var i in inputs)
            {
                List<string> split = i.Split('=').ToList();

                List<string> needs = split[0].Split(',').ToList();

                List<string> result = split[1].Substring(2).Split(' ').ToList();

                

                Tuple<string, int> resultRecipie = new Tuple<string, int>(result[1], int.Parse(result[0]));

                List<Tuple<string, int>> needList = new List<Tuple<string, int>>();

                foreach(var need in needs)
                {
                    string theNeed = need;
                    if (need.StartsWith(" "))
                        theNeed = need.Substring(1);

                    needList.Add(new Tuple<string, int>(theNeed.Split(' ')[1], int.Parse(theNeed.Split(' ')[0])));
                }

                recipies.Add(resultRecipie, needList);
            }

            var fuel = recipies.Where(x => x.Key.Item1 == "FUEL").First();


            GetResources(fuel, resources, recipies, 1);

            int ore = 0;

            foreach(var r in resources)
            {
                var need = recipies.Where(x => x.Key.Item1 == r.Key).First();

                int buys = (int)Math.Ceiling((float)r.Value / need.Key.Item2);

                ore += buys * need.Value[0].Item2;

            }

            return ore.ToString();
        }


        Dictionary<string, long> extraResources = new Dictionary<string, long>();


        private void GetResources(KeyValuePair<Tuple<string, int>, List<Tuple<string, int>>> fuel, Dictionary<string, int> resources, Dictionary<Tuple<string, int>, List<Tuple<string, int>>> recipies, int amount)
        {
            foreach(var fuelNeed in fuel.Value)
            {
                int totalNeeded = amount * fuelNeed.Item2;

                var newFuel = recipies.Where(x => x.Key.Item1 == fuelNeed.Item1).First();

                if(newFuel.Value[0].Item1 == "ORE")
                {
                    if (!resources.ContainsKey(newFuel.Key.Item1))
                        resources.Add(newFuel.Key.Item1, totalNeeded);
                    else
                        resources[newFuel.Key.Item1] += totalNeeded;
                }
                else
                {
                    if (extraResources.ContainsKey(fuelNeed.Item1))
                    {
                        totalNeeded -= (int)extraResources[fuelNeed.Item1];

                        if (totalNeeded < 0)
                        {
                            extraResources[fuelNeed.Item1] = -totalNeeded;
                            continue;
                        }
                        extraResources[fuelNeed.Item1] = 0;
                    }

                        int buys = (int)Math.Ceiling((float)totalNeeded / newFuel.Key.Item2);

                    if(buys * newFuel.Key.Item2 > totalNeeded)
                    {
                        if(!extraResources.ContainsKey(newFuel.Key.Item1))
                        {
                            extraResources.Add(newFuel.Key.Item1, (buys * newFuel.Key.Item2) - totalNeeded);
                        }
                        else
                        {
                            extraResources[newFuel.Key.Item1] += (buys * newFuel.Key.Item2) - totalNeeded;
                        }
                    }

                    GetResources(newFuel, resources, recipies, buys);
                }
            }
        }

        private void GetResources2(KeyValuePair<Tuple<string, int>, List<Tuple<string, int>>> fuel, Dictionary<string, long> resources, Dictionary<Tuple<string, int>, List<Tuple<string, int>>> recipies, long amount)
        {
            foreach (var fuelNeed in fuel.Value)
            {
                long totalNeeded = amount * fuelNeed.Item2;

                var newFuel = recipies.Where(x => x.Key.Item1 == fuelNeed.Item1).First();

                if (newFuel.Value[0].Item1 == "ORE")
                {
                    if (!resources.ContainsKey(newFuel.Key.Item1))
                        resources.Add(newFuel.Key.Item1, totalNeeded);
                    else
                        resources[newFuel.Key.Item1] += totalNeeded;
                }
                else
                {
                    if (extraResources.ContainsKey(fuelNeed.Item1))
                    {
                        totalNeeded -= extraResources[fuelNeed.Item1];

                        if (totalNeeded < 0)
                        {
                            extraResources[fuelNeed.Item1] = -totalNeeded;
                            continue;
                        }
                        extraResources[fuelNeed.Item1] = 0;
                    }

                    long buys = (long)Math.Ceiling((float)totalNeeded / newFuel.Key.Item2);

                    if (buys * newFuel.Key.Item2 > totalNeeded)
                    {
                        if (!extraResources.ContainsKey(newFuel.Key.Item1))
                        {
                            extraResources.Add(newFuel.Key.Item1, (buys * newFuel.Key.Item2) - totalNeeded);
                        }
                        else
                        {
                            extraResources[newFuel.Key.Item1] += (buys * newFuel.Key.Item2) - totalNeeded;
                        }
                    }

                    GetResources2(newFuel, resources, recipies, buys);
                }
            }
        }


        public string RunTwo()
        {
            int test = 8845261;

            while (true)
            {
                extraResources.Clear();

                List<string> inputs = System.IO.File.ReadAllLines(@"..\..\Data\2019\input14.txt").ToList();

                Dictionary<Tuple<string, int>, List<Tuple<string, int>>> recipies = new Dictionary<Tuple<string, int>, List<Tuple<string, int>>>();

                Dictionary<string, long> resources = new Dictionary<string, long>();

                foreach (var i in inputs)
                {
                    List<string> split = i.Split('=').ToList();

                    List<string> needs = split[0].Split(',').ToList();

                    List<string> result = split[1].Substring(2).Split(' ').ToList();



                    Tuple<string, int> resultRecipie = new Tuple<string, int>(result[1], int.Parse(result[0]));

                    List<Tuple<string, int>> needList = new List<Tuple<string, int>>();

                    foreach (var need in needs)
                    {
                        string theNeed = need;
                        if (need.StartsWith(" "))
                            theNeed = need.Substring(1);

                        needList.Add(new Tuple<string, int>(theNeed.Split(' ')[1], int.Parse(theNeed.Split(' ')[0])));
                    }

                    recipies.Add(resultRecipie, needList);
                }

                var fuel = recipies.Where(x => x.Key.Item1 == "FUEL").First();

                GetResources2(fuel, resources, recipies, test);

                long ore = 0;

                foreach (var r in resources)
                {
                    var need = recipies.Where(x => x.Key.Item1 == r.Key).First();

                    long buys = (long)Math.Ceiling((float)r.Value / need.Key.Item2);

                    ore += buys * need.Value[0].Item2;

                }

                long diff = 1000000000000 - ore;
                return test.ToString();
                if (diff < 0)
                    test --;
                else
                {

                }

            }

            //return ore.ToString();
        }
    }
}
