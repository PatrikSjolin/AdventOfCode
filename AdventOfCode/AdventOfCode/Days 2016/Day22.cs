using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Days_2016
{
    public class Node
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Size { get; set; }
        public int Used { get; set; }
        public int Available { get { return Size - Used; } }
        public bool HasTheData { get; set; }
        public string Use { get { return (100 * (Used / (double)Size)) + "%"; } }

        public override bool Equals(object obj)
        {
            Node eq = (Node)obj;
            if(eq.X == X &&
                eq.Y == Y &&
                eq.Used == Used &&
                eq.Size == Size &&
                eq.HasTheData == HasTheData)
            {
                return true;
            }

            return false;
        }
    }

    public class State
    {
        public List<Node> Nodes { get; set; }
    }

    public class Day22 : IPuzzle
    {
        private int taskNumber = 22;

        private int goalX = 33;
        private int goalY = 0;

        public bool Active => false;

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
        public void GoOne()
        {
            return;
            List<string> inputList = ReadInput();
            List<Node> nodes = GetNodes(inputList);
            List<Tuple<Node, Node>> validPairNodes = GetValidPairs(nodes, new List<State>());
            Console.WriteLine(validPairNodes.Count);
        }

        private List<Tuple<Node, Node>> GetValidPairs3(List<State> states, List<Tuple<Tuple<int, int>, Tuple<int, int>>> path)
        {
            State lastState = states.Last();
            List<Tuple<Node, Node>> validPairNodes = new List<Tuple<Node, Node>>();

            for (int i = 0; i < lastState.Nodes.Count; i++)
            {
                Node candidate1 = lastState.Nodes.FirstOrDefault(x => x.X == lastState.Nodes[i].X && x.Y == lastState.Nodes[i].Y + 1);
                Node candidate2 = lastState.Nodes.FirstOrDefault(x => x.X == lastState.Nodes[i].X && x.Y == lastState.Nodes[i].Y - 1);
                Node candidate3 = lastState.Nodes.FirstOrDefault(x => x.X == lastState.Nodes[i].X + 1 && x.Y == lastState.Nodes[i].Y);
                Node candidate4 = lastState.Nodes.FirstOrDefault(x => x.X == lastState.Nodes[i].X - 1 && x.Y == lastState.Nodes[i].Y);

                List<Node> candidates = new List<Node> { candidate1, candidate2, candidate3, candidate4 };

                for (int j = 0; j < 4; j++)
                {
                    if (candidates[j] != null)
                    {
                        bool canGive = lastState.Nodes[i].Used <= candidates[j].Available && lastState.Nodes[i].Used > 0;

                        if (canGive)
                        {
                            bool hasGivenBefore = path.Contains(new Tuple<Tuple<int, int>, Tuple<int, int>>(new Tuple<int, int>(lastState.Nodes[i].X, lastState.Nodes[i].Y), new Tuple<int, int>(candidates[j].X, candidates[j].Y)));

                            if (!hasGivenBefore)
                            {
                                bool resultInExistingState = ResultInExistingState(new Tuple<Node, Node>(lastState.Nodes[i], candidates[j]), lastState.Nodes, states);

                                if (!resultInExistingState)
                                {
                                    validPairNodes.Add(new Tuple<Node, Node>(lastState.Nodes[i], candidates[j]));
                                }
                            }
                        }
                    }
                }
            }
            return validPairNodes;
        }

        private List<Tuple<Node, Node>> GetValidPairs2(List<State> states, List<Tuple<Tuple<int, int>, Tuple<int, int>>> path)
        {
            State lastState = states.Last();
            List<Tuple<Node, Node>> validPairNodes = new List<Tuple<Node, Node>>();

            for (int i = 0; i < lastState.Nodes.Count; i++)
            {
                for (int j = 0; j < lastState.Nodes.Count; j++)
                {
                    if (i == j)
                        continue;

                    bool canTransfer =
                        (lastState.Nodes[i].X == lastState.Nodes[j].X     && lastState.Nodes[i].Y == lastState.Nodes[j].Y + 1) ||
                        (lastState.Nodes[i].X == lastState.Nodes[j].X     && lastState.Nodes[i].Y == lastState.Nodes[j].Y - 1) ||
                        (lastState.Nodes[i].X == lastState.Nodes[j].X + 1 && lastState.Nodes[i].Y == lastState.Nodes[j].Y) ||
                        (lastState.Nodes[i].X == lastState.Nodes[j].X - 1 && lastState.Nodes[i].Y == lastState.Nodes[j].Y);

                    if (canTransfer)
                    {
                        bool canGive = lastState.Nodes[i].Used <= lastState.Nodes[j].Available && lastState.Nodes[i].Used > 0;

                        if (canGive)
                        {
                            bool hasGivenBefore = path.Contains(new Tuple<Tuple<int, int>, Tuple<int, int>>(new Tuple<int, int>(lastState.Nodes[i].X, lastState.Nodes[i].Y), new Tuple<int, int>(lastState.Nodes[j].X, lastState.Nodes[j].Y)));

                            if (!hasGivenBefore)
                            {
                                bool resultInExistingState = ResultInExistingState(new Tuple<Node, Node>(lastState.Nodes[i], lastState.Nodes[j]), lastState.Nodes, states);

                                if (!resultInExistingState)
                                {
                                    validPairNodes.Add(new Tuple<Node, Node>(lastState.Nodes[i], lastState.Nodes[j]));
                                }
                            }
                        }
                    }
                }
            }
            return validPairNodes;
        }

        private List<Tuple<Node, Node>> GetValidPairs(List<Node> nodes, List<State> states)
        {
            List<Tuple<Node, Node>> validPairNodes = new List<Tuple<Node, Node>>();

            for (int i = 0; i < nodes.Count; i++)
            {
                for (int j = 0; j < nodes.Count; j++)
                {
                    if (i == j)
                        continue;

                    if (nodes[i].Used <= nodes[j].Available && nodes[i].Used > 0 && !ResultInExistingState(new Tuple<Node, Node>(nodes[i], nodes[j]), nodes, states))
                    {
                        validPairNodes.Add(new Tuple<Node, Node>(nodes[i], nodes[j]));
                    }
                }
            }
            return validPairNodes;
        }

        private bool ResultInExistingState(Tuple<Node, Node> tuple, List<Node> nodes, List<State> states)
        {
            List<Node> copyNodes = new List<Node>();
            foreach(Node n in nodes)
            {
                copyNodes.Add(new Node
                {
                    HasTheData = n.HasTheData,
                    Size = n.Size,
                    Used = n.Used,
                    X = n.X,
                    Y = n.Y
                });
            }
            int used = tuple.Item1.Used;

            var node1 = copyNodes.First(x => x.X == tuple.Item1.X && x.Y == tuple.Item1.Y);
            var node2 = copyNodes.First(x => x.X == tuple.Item2.X && x.Y == tuple.Item2.Y);

            node1.Used = 0;
            node2.Used += used;

            if (node1.HasTheData)
            {
                node2.HasTheData = true;
                node1.HasTheData = false;
            }

            foreach (var state in states)
            {
                bool eq = false;
                foreach(var node in state.Nodes)
                {
                    Node node3 = copyNodes.First(x => x.X == node.X && x.Y == node.Y);
                    if (node3.Equals(node))
                    {
                        eq = true;
                    }
                    else
                    {
                        eq = false;
                        break;
                    }
                }
                if (eq)
                {
                    return true;
                }
            }
            return false;
        }

        private List<Node> GetNodes(List<string> inputList)
        {
            inputList.RemoveAt(0);
            inputList.RemoveAt(0);
            List<Node> nodes = new List<Node>();
            foreach (var s in inputList)
            {
                List<string> split = s.Split(' ').ToList();
                split = split.Where(x => x != "").ToList();

                int X = int.Parse(split[0].Split('-')[1].Substring(1).ToString());
                int Y = int.Parse(split[0].Split('-')[2].Substring(1).ToString());
                
                nodes.Add(new Node
                {
                    X = X,
                    Y = Y,
                    Size = int.Parse(split[1].Substring(0, split[1].Count() - 1)),
                    Used = int.Parse(split[2].Substring(0, split[2].Count() - 1)),
                    HasTheData = Y == 0 && X == goalX
                });
            }

            return nodes;
        }

        public void GoTwo()
        {
            List<string> inputList = ReadInput();
            List<Node> nodes = GetNodes(inputList);

            List<State> states = new List<State>();

            states = UpdateState(states);

            State state = new State { Nodes = new List<Node>() };

            foreach (var lastNode in nodes)
            {
                state.Nodes.Add(new Node
                {
                    X = lastNode.X,
                    Y = lastNode.Y,
                    HasTheData = lastNode.HasTheData,
                    Size = lastNode.Size,
                    Used = lastNode.Used
                });
            }
            states.Add(state);

            List<Tuple<Tuple<int, int>, Tuple<int, int>>> path = new List<Tuple<Tuple<int, int>, Tuple<int, int>>>();

            CalculateMap(states, path);
        }

        private int CalculateMap(List<State> states, List<Tuple<Tuple<int, int>, Tuple<int, int>>> path)
        {
            List<Tuple<Node, Node>> validPairNodes = new List<Tuple<Node, Node>>(GetValidPairs3(states, path));

            foreach (var tuple in validPairNodes)
            {
                if (path.Count == 0)
                {

                }

                State state = new State { Nodes = new List<Node>() };

                State lastState = states.Last();

                foreach(var lastNode in lastState.Nodes)
                {
                    state.Nodes.Add(new Node
                    {
                        X = lastNode.X,
                        Y = lastNode.Y,
                        HasTheData = lastNode.HasTheData,
                        Size = lastNode.Size,
                        Used = lastNode.Used
                    });
                }

                int used = tuple.Item1.Used;
                var node1 = state.Nodes.First(x => x.X == tuple.Item1.X && x.Y == tuple.Item1.Y);
                var node2 = state.Nodes.First(x => x.X == tuple.Item2.X && x.Y == tuple.Item2.Y);

                node1.Used = 0;
                if(node2.Available < used)
                {
                    continue;
                }
                node2.Used += used;
                if (node1.HasTheData)
                {
                    node2.HasTheData = true;
                    node1.HasTheData = false;
                }

                List<Tuple<Tuple<int, int>, Tuple<int, int>>> newPath = new List<Tuple<Tuple<int, int>, Tuple<int, int>>>(path);

                newPath.Add(new Tuple<Tuple<int, int>, Tuple<int, int>>(new Tuple<int, int>(node1.X, node1.Y), new Tuple<int, int>(node2.X, node2.Y)));
                
                List<State> newStates = UpdateState(states);
                newStates.Add(state);
                //states = UpdateState(states);
                                
                if(node2.HasTheData && node2.X == 0 && node2.Y == 0)
                {
                    return newPath.Count;
                }

                CalculateMap(newStates, newPath);
            }

            return 0;
        }

        private List<State> UpdateState(List<State> states)
        {
            List<State> statescopy = new List<State>();

            foreach(var st in states)
            {
                State t;
                statescopy.Add(t = new State { Nodes = new List<Node>() });
                foreach(var n in st.Nodes)
                {
                    t.Nodes.Add(new Node
                    {
                        X = n.X,
                        Y = n.Y,
                        HasTheData = n.HasTheData,
                        Size = n.Size,
                        Used = n.Used
                    });
                }
            }

            return statescopy;
        }

        private void DrawMap(List<Node> nodes)
        {
            for(int i = 0; i < nodes.Count; i++)
            {
                if(nodes[i].X == 0)
                {
                    Console.WriteLine();
                }
                else
                {
                    Console.Write(nodes[i]);
                }
            }
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
