using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Days_2020
{
    public class Field
    {
        public string Name { get; set; }
        public (int, int) Range1 { get; set; }
        public (int, int) Range2 { get; set; }
    }

    public class Day16 : IPuzzle
    {
        public bool Active => true;

        private List<string> inputs;

        Dictionary<string, Field> fieldRanges;
        List<int> ticket = new List<int>();
        Dictionary<int, List<int>> nearbyTickets;
        List<int> invalidTickets;


        public string RunOne()
        {
            inputs = System.IO.File.ReadAllLines(@"..\..\Data\2020\input16.txt").ToList();
            fieldRanges = new Dictionary<string, Field>();
            nearbyTickets = new Dictionary<int, List<int>>();
            invalidTickets = new List<int>();
            ticket = new List<int>();

            int index = 0;
            foreach (var row in inputs)
            {
                if (row == "")
                    break;
                var split = row.Split(':').ToList();

                string field = split[0];

                var values = split[1].Split(' ').ToList();
                string value1 = values[1];
                string value2 = values[3];
                int a = int.Parse(value1.Split('-')[0]);
                int b = int.Parse(value1.Split('-')[1]);

                int c = int.Parse(value2.Split('-')[0]);
                int d = int.Parse(value2.Split('-')[1]);

                fieldRanges.Add(field, new Field { Name = field, Range1 = (a, b), Range2 = (c, d) });
                index++;
            }
            index += 2;
            var ticketNumbers = inputs[index].Split(',');
            ticket = ticketNumbers.Select(x => int.Parse(x)).ToList();

            index += 3;

            for (int i = index; i < inputs.Count; i++)
            {
                var values = inputs[i].Split(',');
                nearbyTickets.Add(i, new List<int>(values.Select(x => int.Parse(x))));
            }
            List<int> invalidNumbers = new List<int>();

            foreach (var nt in nearbyTickets)
            {
                for (int i = 0; i < nt.Value.Count; i++)
                {
                    bool success = false;
                    foreach (var field in fieldRanges)
                    {
                        if ((field.Value.Range1.Item1 <= nt.Value[i] && field.Value.Range1.Item2 >= nt.Value[i]) || (field.Value.Range2.Item1 <= nt.Value[i] && field.Value.Range2.Item2 >= nt.Value[i]))
                        {
                            success = true;
                            break;
                        }
                    }
                    if (!success)
                    {
                        invalidNumbers.Add(nt.Value[i]);
                        invalidTickets.Add(nt.Key);
                    }
                }
            }

            return invalidNumbers.Sum().ToString();
        }

        public string RunTwo()
        {
            for (int i = 0; i < invalidTickets.Count; i++)
                nearbyTickets.Remove(invalidTickets[i]);

            Dictionary<int, string> fieldPositions = new Dictionary<int, string>();
            List<(string, int)> potential = new List<(string, int)>();
            while (fieldPositions.Count != fieldRanges.Count)
            {
                for (int column = 0; column < nearbyTickets.First().Value.Count; column++)
                {
                    if (fieldPositions.ContainsKey(column))
                        continue;
                    potential.Clear();
                    foreach (var field in fieldRanges.Values)
                    {
                        if (fieldPositions.ContainsValue(field.Name))
                            continue;
                        bool success = true;
                        foreach (var ticket in nearbyTickets.Values)
                        { 
                            int ticketNumber = ticket[column];
                            //If not contained within the two ranges
                            if (!((field.Range1.Item1 <= ticketNumber && field.Range1.Item2 >= ticketNumber) || (field.Range2.Item1 <= ticketNumber && field.Range2.Item2 >= ticketNumber)))
                            {
                                success = false;
                                break;
                            }
                        }
                        if (success)
                        {
                            potential.Add((field.Name, column));
                        }
                    }
                    if (potential.Count == 1)
                    {
                        fieldPositions.Add(potential[0].Item2, potential[0].Item1);
                    }
                }
            }

            long product = 1;
            foreach (var fp in fieldPositions)
            {
                if (fp.Value.StartsWith("departure"))
                {
                    product *= ticket[fp.Key];
                }
            }

            return product.ToString();
        }
    }
}
