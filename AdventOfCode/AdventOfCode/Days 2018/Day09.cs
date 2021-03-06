﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Days_2018
{
    public class Day09 : IPuzzle
    {
        public bool Active { get => true; }

        public string RunOne()
        {
            int numPlayers = 426;
            int lastMarbleScore = 72058;

            Dictionary<long, long> playerScores = GetPlayerScores2(numPlayers, lastMarbleScore);

            long highest = 0;

            foreach (var v in playerScores)
            {
                if (v.Value > highest)
                {
                    highest = v.Value;
                }
            }

            return highest.ToString();
        }

        public string RunTwo()
        {
            int numPlayers = 426;
            int lastMarbleScore = 72058 * 100;

            Dictionary<long, long> playerScores = GetPlayerScores2(numPlayers, lastMarbleScore);

            long highest = 0;

            foreach (var v in playerScores)
            {
                if (v.Value > highest)
                {
                    highest = v.Value;
                }
            }

            return highest.ToString();
        }

        private Dictionary<long, long> GetPlayerScores(int numPlayers, int lastMarbleScore)
        {
            Dictionary<long, long> playerScores = new Dictionary<long, long>();

            for (int i = 0; i < numPlayers; i++)
            {
                playerScores.Add(i, 0);
            }

            int score = 0;
            int marble = 0;
            int playerTurn = 0;

            LinkedList<int> marbles = new LinkedList<int>();
            LinkedListNode<int> current = marbles.AddFirst(marble);
            marble++;

            while (marble != lastMarbleScore)
            {
                var nextNode = current.Next;
                if (nextNode == null)
                {
                    nextNode = marbles.First;
                }
                current = marbles.AddAfter(nextNode, marble);
                marble++;
                playerTurn++;
                playerTurn = playerTurn % numPlayers;

                if (marble % 23 == 0)
                {
                    score = marble;
                    for (int i = 0; i < 7; i++)
                    {
                        current = current.Previous;
                        if (current == null)
                        {
                            current = marbles.Last;
                        }
                    }

                    int m = current.Value;
                    nextNode = current.Next;
                    marbles.Remove(current);
                    current = nextNode;
                    score += m;
                    playerScores[playerTurn] += score;
                    marble++;
                    playerTurn++;
                    playerTurn = playerTurn % numPlayers;
                }
            }

            return playerScores;
        }

        public class LinkedNode
        {
            public LinkedNode Previous { get; set; }
            public LinkedNode Next { get; set; }
            public int Value { get; set; }
        }

        public static class LinkedNodeList
        {
            public static LinkedNode AddFirst(int value)
            {
                LinkedNode ln = new LinkedNode();

                ln.Value = value;
                ln.Previous = ln;
                ln.Next = ln;

                return ln;
            }

            public static LinkedNode AddAfter(LinkedNode n, int value)
            {
                LinkedNode next = n.Next;
                LinkedNode newNode = new LinkedNode();
                newNode.Value = value;

                LinkedNode previous = n;

                previous.Next = newNode;
                newNode.Next = next;
                next.Previous = newNode;
                newNode.Previous = previous;

                return newNode;
            }

            public static void Remove(LinkedNode node)
            {
                node.Previous.Next = node.Next;
                node.Next.Previous = node.Previous;
                node = null;
            }
        }

        private Dictionary<long, long> GetPlayerScores2(int numPlayers, int lastMarbleScore)
        {
            Dictionary<long, long> playerScores = new Dictionary<long, long>();

            for (int i = 0; i < numPlayers; i++)
            {
                playerScores.Add(i, 0);
            }

            int score = 0;
            int marble = 0;
            int playerTurn = 0;

            LinkedNode current = LinkedNodeList.AddFirst(marble);
            marble++;

            while (marble != lastMarbleScore)
            {
                LinkedNode nextNode = current.Next;
                current = LinkedNodeList.AddAfter(nextNode, marble);
                marble++;
                playerTurn++;
                playerTurn = playerTurn % numPlayers;

                if (marble % 23 == 0)
                {
                    for (int i = 0; i < 7; i++)
                    {
                        current = current.Previous;
                    }

                    score = marble + current.Value;
                    nextNode = current.Next;
                    LinkedNodeList.Remove(current);
                    current = nextNode;

                    playerScores[playerTurn] += score;
                    marble++;
                    playerTurn++;
                    playerTurn = playerTurn % numPlayers;
                }
            }

            return playerScores;
        }
    }
}
