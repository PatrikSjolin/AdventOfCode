using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Days_2018
{
    public class Day14 : IPuzzle
    {
        public string RunOne()
        {
            LinkedList<int> nodes = new LinkedList<int>();
            LinkedListNode<int> firstElf = nodes.AddFirst(3);

            LinkedListNode<int> secondElf = nodes.AddAfter(firstElf, 7);

            int count = 2;
            int puzzleInput = 077201;

            //string "37";

            while (count <= puzzleInput + 10)
            {
                int first = firstElf.Value;
                int second = secondElf.Value;

                int sum = first + second;

                if (sum >= 10)
                {
                    nodes.AddLast(1);
                    nodes.AddLast(sum - 10);
                    count++;
                }
                else
                {
                    nodes.AddLast(sum);
                }

                int move = 0;
                //int first = firstElf.Value;
                while (move < first + 1)
                {
                    firstElf = firstElf.Next;
                    if (firstElf == null)
                    {
                        firstElf = nodes.First;
                    }
                    move++;
                }
                move = 0;
                //int second = secondElf.Value;
                while (move < second + 1)
                {
                    secondElf = secondElf.Next;
                    if (secondElf == null)
                    {
                        secondElf = nodes.First;
                    }
                    move++;
                }

                count++;
            }

            string result = "";
            LinkedListNode<int> node = nodes.First;
            for (int i = 0; i < 10 + puzzleInput - 1; i++)
            {
                node = node.Next;
                if (i > puzzleInput - 2)
                    result += node.Value;
            }

            return result;
        }

        public string RunTwo()
        {
            string puzzleInput = "077201";

            string result = GetRecipiesBeforeInput2(puzzleInput);

            return result.ToString();
        }

        private string GetRecipiesBeforeInput2(string puzzleInput)
        {
            List<int> nodes = new List<int>();
            nodes.Add(3);
            nodes.Add(7);

            int firstElf = 0;
            int secondElf = 1;

            string recipies = "37";

            while (!recipies.Contains(puzzleInput))
            {
                int first = nodes[firstElf];
                int second = nodes[secondElf];

                int sum = first + second;

                if (sum >= 10)
                {
                    nodes.Add(1);
                    nodes.Add(sum - 10);
                }
                else
                {
                    nodes.Add(sum);
                }

                recipies += sum;
                firstElf = (firstElf + first + 1) % nodes.Count();
                secondElf = (secondElf + second + 1) % nodes.Count();

                if (recipies.Length > 12)
                {
                    recipies = recipies.Substring(recipies.Length - 7, 7);
                }
            }
            int extra = 0;
            if (puzzleInput.Last().ToString() != nodes.Last().ToString())
                extra++;

            return (nodes.Count - puzzleInput.Length - extra).ToString();
        }

        private string GetRecipiesBeforeInput(string puzzleInput)
        {
            LinkedList<int> nodes = new LinkedList<int>();
            LinkedListNode<int> firstElf = nodes.AddFirst(3);

            LinkedListNode<int> secondElf = nodes.AddAfter(firstElf, 7);

            int count = 2;

            string recipies = "37";

            while (!recipies.Contains(puzzleInput))
            {
                int first = firstElf.Value;
                int second = secondElf.Value;

                int sum = first + second;

                if (sum >= 10)
                {
                    recipies += 1;
                    recipies += (sum - 10);
                    nodes.AddLast(1);
                    nodes.AddLast(sum - 10);
                    count++;
                }
                else
                {
                    recipies += sum;
                    nodes.AddLast(sum);
                }

                int move = 0;
                while (move < first + 1)
                {
                    firstElf = firstElf.Next;
                    if (firstElf == null)
                    {
                        firstElf = nodes.First;
                    }
                    move++;
                }
                move = 0;
                while (move < second + 1)
                {
                    secondElf = secondElf.Next;
                    if (secondElf == null)
                    {
                        secondElf = nodes.First;
                    }
                    move++;
                }

                count++;
                if (recipies.Length > 20)
                {
                    recipies = recipies.Substring(recipies.Length - 10, 10);
                }
            }

            if (puzzleInput.Last().ToString() != nodes.Last().ToString())
                count--;

            return "" + (count - puzzleInput.Length);
        }
    }
}
