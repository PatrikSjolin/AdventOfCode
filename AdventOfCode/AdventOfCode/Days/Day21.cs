using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Days
{
    public class Rule
    {
        public int[,] Input { get; set; }

        public override bool Equals(object obj)
        {
            int[,] test = ((Rule)obj).Input;

            int sizeX = test.GetLength(0);
            int sizeY = test.GetLength(1);

            if (sizeX != Input.GetLength(0) || sizeY != Input.GetLength(1))
                return false;

            for (int i = 0; i < sizeX; i++)
            {
                for (int j = 0; j < sizeY; j++)
                {
                    if (test[i, j] != Input[i, j])
                        return false;
                }
            }

            return true;
        }
    }

    public class Day21 : IPuzzle
    {
        Dictionary<Rule, int[,]> ruleMap;

        public string RunOne()
        {
            List<string> rules = System.IO.File.ReadAllLines(@"..\..\input21.txt").ToList();
            ruleMap = new Dictionary<Rule, int[,]>();

            foreach (var r in rules)
            {
                string hej = r.Replace("=>", "|").Replace(" ", "");

                var split = hej.Split('|').ToList();
                var input = split[0].Split('/').ToList();
                var output2 = split[1].Split('/').ToList();

                List<int[,]> keys = new List<int[,]>();

                int[,] key = new int[input.Count, input.Count];
                int[,] value = new int[output2.Count, output2.Count];

                for (int i = 0; i < input.Count; i++)
                {
                    for (int j = 0; j < input.Count; j++)
                    {
                        key[i, j] = input[i][j] == '#' ? 1 : 0;
                    }
                }

                keys.Add(key);

                AddOtherKeys(keys, key);

                for (int i = 0; i < output2.Count; i++)
                {
                    for (int j = 0; j < output2.Count; j++)
                    {
                        value[i, j] = output2[i][j] == '#' ? 1 : 0;
                    }
                }

                foreach (var k in keys)
                {
                    Rule rule = new Rule();

                    rule.Input = new int[k.GetLength(0), k.GetLength(1)];
                    for (int q = 0; q < k.GetLength(0); q++)
                    {
                        for (int t = 0; t < k.GetLength(0); t++)
                        {
                            rule.Input[q, t] = k[q, t];
                        }
                    }
                    ruleMap.Add(rule, value);
                }
            }

            int[,] output = new int[3, 3]
            {
                { 0, 1, 0 },
                { 0, 0, 1 },
                { 1, 1, 1 }
            };
            int pixelsOn = 0;

            for (int i = 0; i < 5; i++)
            {
                int size = output.GetLength(0);

                if (size % 2 == 0)
                {
                    int grids = (size / 2) * (size / 2);

                    int rows = size / 2;
                    List<Rule> outputs = GetOutputs(output, grids, 2);

                    output = new int[(size / 2) * 3, (size / 2) * 3];

                    for (int j = 0; j < outputs.Count; j++)
                    {
                        int[,] input = GetOutputFromRule(outputs[j], ruleMap);

                        int numberOfItems = input.GetLength(0);

                        for (int r = 0; r < input.GetLength(0); r++)
                        {
                            for (int c = 0; c < input.GetLength(0); c++)
                            {
                                output[(j / rows) * numberOfItems + r, (j % rows) * numberOfItems + c] = input[r, c];
                            }
                        }

                    }
                }
                else if (size % 3 == 0)
                {
                    int grids = (size / 3) * (size / 3);

                    int rows = size / 3;

                    List<Rule> outputs = GetOutputs(output, grids, 3);

                    output = new int[(size / 3) * 4, (size / 3) * 4];

                    for (int j = 0; j < outputs.Count; j++)
                    {
                        int[,] input = GetOutputFromRule(outputs[j], ruleMap);

                        int numberOfItems = input.GetLength(0);

                        for (int r = 0; r < input.GetLength(0); r++)
                        {
                            for (int c = 0; c < input.GetLength(0); c++)
                            {
                                output[(j / rows) * numberOfItems + r, (j % rows) * numberOfItems + c] = input[r, c];
                            }
                        }

                    }
                }
            }
            pixelsOn = CountPixelsOn(output);

            return pixelsOn.ToString();
        }

        public string RunTwo()
        {
            int[,] output = new int[3, 3]
   {
                { 0, 1, 0 },
                { 0, 0, 1 },
                { 1, 1, 1 }
   };
            int pixelsOn = 0;

            for (int i = 0; i < 18; i++)
            {
                int size = output.GetLength(0);

                if (size % 2 == 0)
                {
                    int grids = (size / 2) * (size / 2);

                    int rows = size / 2;
                    List<Rule> outputs = GetOutputs(output, grids, 2);

                    output = new int[(size / 2) * 3, (size / 2) * 3];

                    for (int j = 0; j < outputs.Count; j++)
                    {
                        int[,] input = GetOutputFromRule(outputs[j], ruleMap);

                        int numberOfItems = input.GetLength(0);

                        for (int r = 0; r < input.GetLength(0); r++)
                        {
                            for (int c = 0; c < input.GetLength(0); c++)
                            {
                                output[(j / rows) * numberOfItems + r, (j % rows) * numberOfItems + c] = input[r, c];
                            }
                        }

                    }
                }
                else if (size % 3 == 0)
                {
                    int grids = (size / 3) * (size / 3);

                    int rows = size / 3;

                    List<Rule> outputs = GetOutputs(output, grids, 3);

                    output = new int[(size / 3) * 4, (size / 3) * 4];

                    for (int j = 0; j < outputs.Count; j++)
                    {
                        int[,] input = GetOutputFromRule(outputs[j], ruleMap);

                        int numberOfItems = input.GetLength(0);

                        for (int r = 0; r < input.GetLength(0); r++)
                        {
                            for (int c = 0; c < input.GetLength(0); c++)
                            {
                                output[(j / rows) * numberOfItems + r, (j % rows) * numberOfItems + c] = input[r, c];
                            }
                        }

                    }
                }
            }
            pixelsOn = CountPixelsOn(output);

            return pixelsOn.ToString();
        }

        private int CountPixelsOn(int[,] output)
        {
            int on = 0;

            for (int i = 0; i < output.GetLength(0); i++)
            {
                for (int j = 0; j < output.GetLength(1); j++)
                {
                    on += output[i, j];
                }
            }

            return on;
        }

        private void AddOtherKeys(List<int[,]> keys, int[,] key)
        {
            int size = key.GetLength(0);
            int[,] flip = new int[size, size];

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    flip[i, j] = key[i, size - j - 1];
                }
            }

            keys.Add(flip);

            int[,] previous = (int[,])key.Clone();

            for (int i = 0; i < 3; i++)
            {
                int[,] rot = new int[size, size];
                for (int j = 0; j < size; j++)
                {
                    for (int k = 0; k < size; k++)
                    {
                        rot[j, k] = previous[size - k - 1, j];
                    }
                }
                keys.Add(rot);
                previous = (int[,])rot.Clone();
            }

            previous = (int[,])flip.Clone();
            for (int i = 0; i < 3; i++)
            {
                int[,] rot = new int[size, size];
                for (int j = 0; j < size; j++)
                {
                    for (int k = 0; k < size; k++)
                    {
                        rot[j, k] = previous[size - k - 1, j];
                    }
                }
                keys.Add(rot);
                previous = (int[,])rot.Clone();
            }
        }

        private int[,] GetOutputFromRule(Rule o, Dictionary<Rule, int[,]> ruleMap)
        {
            foreach (var rule in ruleMap)
            {
                if (rule.Key.Equals(o))
                {
                    return rule.Value;
                }
            }

            return null;
        }

        private List<Rule> GetOutputs(int[,] output, int grids, int div)
        {
            List<Rule> list = new List<Rule>();

            int size = output.GetLength(0);
            int curSize = div;

            int rows = size / div;

            for (int i = 0; i < grids; i++)
            {
                Rule r = new Rule();
                r.Input = new int[curSize, curSize];

                for (int j = 0; j < curSize; j++)
                {
                    for (int k = 0; k < curSize; k++)
                    {
                        r.Input[j, k] = output[(i / rows) * div + j, (i % rows) * div + k];
                    }
                }
                list.Add(r);
            }

            return list;
        }
    }
}
