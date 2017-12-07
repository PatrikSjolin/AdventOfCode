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

        public void RunOne()
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
                    return;
                }
            }


            string answer = "";

            Console.WriteLine(answer);
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

        long sum = 0;
        long GetSum(Dictionary<string, List<string>> tree, string parent)
        {
            if (!tree.ContainsKey(parent))
            {
                return weight[parent];
            }

            List<string> children = tree[parent];

            long sum2 = 0;
            
            foreach(string c in children)
            {
                sum2 += GetSum(tree, c);
            }
            sum += sum2;
            return sum2;
        }

        //int GetSumOfChildren(List<string> children)
        //{

        //}

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
                    //children.Add(tree[c]);
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


        long GetSum2(Dictionary<string, List<string>> tree, string parent)
        {
            if (!tree.ContainsKey(parent))
            {
                return 0;
            }


            List<string> children = tree[parent];

            foreach(var v in children)
            {
                sum += weight[v] + GetSum2(tree, v);
            }

            return weight[parent];
        }

        public void RunTwo()
        {
            Dictionary<string, long> sums = new Dictionary<string, long>();

            long tot = GetSum3(tree, root);

            foreach(var c in tree[root])
            {
                sum = 0;
                sums.Add(c, weight[c] + GetSum3(tree, c));
            }
            sums.Clear();
            foreach(var c in tree["onnfacs"])
            {
                sum = 0;
                sums.Add(c, weight[c] + GetSum3(tree, c));
            }
            sums.Clear();
            foreach(var c in tree["ftaxy"])
            {

                sum = 0;
                sums.Add(c, weight[c] + GetSum3(tree, c));
            }
            sums.Clear();
            foreach(var c in tree["gexwzw"])
            {

                sum = 0;
                sums.Add(c, weight[c] + GetSum3(tree, c));
            }
        }
    }
}
