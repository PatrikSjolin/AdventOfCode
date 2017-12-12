using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Days
{
    public class Day07 : IPuzzle
    {
        string root = "";
        Dictionary<string, List<string>> tree;
        Dictionary<string, int> weight;

        public string RunOne()
        {
            List<string> inputLines = System.IO.File.ReadAllLines(@"..\..\input7.txt").ToList();

            tree = new Dictionary<string, List<string>>();
            weight = new Dictionary<string, int>();
            string child = "";
            foreach(string s in inputLines)
            {
                string test = s.Replace("->", ".");
                List<string> nodes = test.Split('.').ToList();


                if(nodes.Count > 1)
                {
                    string parent = nodes[0].Split(' ')[0];


                    if (!weight.ContainsKey(nodes[0]))
                    {
                        string split = nodes[0].Split(' ')[1];
                        split = split.Replace("(", "");
                        split = split.Replace(")", "");
                        weight.Add(parent, int.Parse(split));
                    }

                    nodes[1] = nodes[1].Replace(" ", "");
                    List<string> children = nodes[1].Split(',').ToList();
                    child = children[0];
                    tree.Add(parent, children);
                }
                else
                {

                    string parent = nodes[0].Split(' ')[0];
                    string split = nodes[0].Split(' ')[1];
                    split = split.Replace("(", "");
                    split = split.Replace(")", "");
                    weight.Add(parent, int.Parse(split));
                }
            }

            string p = child;
            while (true)
            {
                string lastParent = p;
                p = GetParent(p, tree);
                if(p == null)
                {
                    root = lastParent;
                    return root;
                }
            }
        }

        string GetParent(string child, Dictionary<string, List<string>> tree)
        {
            foreach (var node in tree)
            {
                if (node.Value.Contains(child))
                {
                    return node.Key;
                }
            }

            return null;
        }

        int GetSum3(Dictionary<string, List<string>> tree, string parent)
        {
            int sum = 0;
            List<string> children = new List<string>(tree[parent]);
            while (children.Count > 0)
            {
                List<string> newlist = new List<string>(children);
                children.Clear();
                foreach (var c in newlist)
                {
                    sum += weight[c];
                    if (tree.ContainsKey(c))
                    {
                        foreach (var hej in tree[c])
                        {
                            children.Add(hej);
                        }
                    }
                }
            }

            return sum;
        }

        int FindIncorrectSum(Dictionary<string, int> sums)
        {
            Dictionary<int, bool> tests = new Dictionary<int, bool>();
            foreach (var sum in sums)
            {
                if (tests.ContainsKey(sum.Value))
                {
                    tests[sum.Value] = true;
                }
                else
                {
                    tests.Add(sum.Value, false);
                }
            }

            return tests.FirstOrDefault(x => !x.Value).Key;
        }

        public string RunTwo()
        {
            Dictionary<string, int> sums = new Dictionary<string, int>();
            List<string> path = new List<string>();

            string needsBalancing = root;

            while (true)
            {
                foreach (var c in tree[needsBalancing])
                {
                    sums.Add(c, weight[c] + GetSum3(tree, c));
                }

                int incorrectWeight = FindIncorrectSum(sums);
                var hej = sums.FirstOrDefault(x => x.Value == incorrectWeight);
                
                    needsBalancing = sums.FirstOrDefault(x => x.Value == incorrectWeight).Key;
                path.Add(needsBalancing);

                if(string.IsNullOrEmpty(needsBalancing))
                {
                    sums.Clear();
                    foreach (var c in tree[path[path.Count - 3]])
                    {
                        sums.Add(c, weight[c] + GetSum3(tree, c));
                    }
                    int incorrectWeight2 = FindIncorrectSum(sums);
                    var hej2 = sums.FirstOrDefault(x => x.Value != incorrectWeight2);
                    var badNode = sums.FirstOrDefault(x => x.Value == incorrectWeight2);

                    return (weight[badNode.Key] - (incorrectWeight2 - sums.FirstOrDefault(x => x.Value != incorrectWeight2).Value)).ToString();
                }
                sums.Clear();
            }
        }
    }
}
