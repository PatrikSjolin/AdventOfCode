using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Days_2018
{

    public class Node
    {
        public int MetaDataSize { get; set; }

        public List<int> MetaData { get; set; }

        public List<Node> Children { get; set; }
    }

    public class Day08 : IPuzzle
    {
        Node root;
        int sum = 0;

        public string RunOne()
        {
            List<string> inputLines = System.IO.File.ReadAllLines(@"..\..\Data\2018\input08.txt").ToList();

            List<string> split = inputLines[0].Split(' ').ToList();

            root = new Node();

            BuildTree(split, root);

            return sum.ToString();
        }

        private void BuildTree(List<string> split, Node n)
        {
            int childrenCount = int.Parse(split[0]);
            int metaDataSize = int.Parse(split[1]);

            split.RemoveAt(0);
            split.RemoveAt(0);

            n.MetaDataSize = metaDataSize;
            n.Children = new List<Node>();

            if (childrenCount > 0)
            {
                for (int i = 0; i < childrenCount; i++)
                {
                    Node c = new Node();
                    n.Children.Add(c);
                    BuildTree(split, c);
                }

                n.MetaData = new List<int>();
                for (int i = 0; i < n.MetaDataSize; i++)
                {
                    int data = int.Parse(split[0]);
                    sum += data;
                    n.MetaData.Add(data);
                    split.RemoveAt(0);
                }
            }
            else
            {
                n.MetaData = new List<int>();
                for (int i = 0; i < n.MetaDataSize; i++)
                {
                    int data = int.Parse(split[0]);
                    sum += data;
                    n.MetaData.Add(data);
                    split.RemoveAt(0);
                }
            }
        }

        public string RunTwo()
        {
            int test = GetRootSum(root, 0);
            return rootSum.ToString();
        }

        int rootSum = 0;

        private int GetRootSum(Node root, int summ)
        {
            if(root.Children.Count == 0)
            {
                foreach(var m in root.MetaData)
                {
                    rootSum += m;
                    summ += m;
                }
            }
            else
            {
                foreach (var v in root.MetaData)
                {
                    if (root.Children.Count >= v)
                    {
                        Node c = root.Children[v - 1];

                        summ += GetRootSum(c, 0);
                    }
                }
            }
            return summ;
        }
    }
}
