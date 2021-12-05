using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Days_2021
{
    public class Day04 : IPuzzle
    {
        public bool Active => true;
        private List<string> inputs;

        public string RunOne()
        {
            inputs = System.IO.File.ReadAllLines(@"..\..\Data\2021\input04.txt").ToList();

            List<int> numbers = inputs[0].Split(',').Select(x => int.Parse(x)).ToList();

            Dictionary<int, List<List<int>>> boards = new Dictionary<int, List<List<int>>>();

            List<int> board = new List<int>();
            int index = -1;
            for(int i = 1; i < inputs.Count; i++)
            {
                if(inputs[i] == "")
                {
                    index++;
                    boards.Add(index, new List<List<int>>());
                }
                else
                {                    
                    List<string> split = inputs[i].Split(' ').ToList();
                    split = split.Where(x => x != "").ToList();
                    for(int j = 0; j < 5; j++)
                    {
                        board.Add(int.Parse(split[j]));
                    }
                    boards[index].Add(board);
                    board = new List<int>();
                }
            }

            
            foreach(var b in boards)
            {
                for(int i = 0; i < 5; i++)
                {
                    board = new List<int>();

                    for (int j = 0; j < 5; j++)
                    {
                        board.Add(b.Value[j][i]);
                    }

                    b.Value.Add(board);
                }                
            }

            HashSet<int> drawnNumbers = new HashSet<int>();

            for(int i = 0; i < numbers.Count; i++)
            {
                int number = numbers[i];
                drawnNumbers.Add(number);

                foreach(var b in boards)
                {
                    foreach(var line in b.Value)
                    {
                        bool won = true;
                        for (int l = 0; l < 5; l++)
                        {
                            if (!drawnNumbers.Contains(line[l]))
                                won = false;
                        }

                        if (won)
                        {
                            int sum = 0;
                            for(int m = 0; m < 5; m++)
                            {
                                for(int n = 0; n < 5; n++)
                                {
                                    if (!drawnNumbers.Contains(b.Value[m][n]))
                                        sum += b.Value[m][n];
                                }
                            }

                            int result = sum * number;

                            return result.ToString();
                        }
                    }
                }

            }

            return "";
        }

        public string RunTwo()
        {
            List<int> numbers = inputs[0].Split(',').Select(x => int.Parse(x)).ToList();

            Dictionary<int, List<List<int>>> boards = new Dictionary<int, List<List<int>>>();

            List<int> victoriousBoards = new List<int>();

            List<int> board = new List<int>();
            int index = -1;
            for (int i = 1; i < inputs.Count; i++)
            {
                if (inputs[i] == "")
                {
                    index++;
                    boards.Add(index, new List<List<int>>());
                }
                else
                {
                    List<string> split = inputs[i].Split(' ').ToList();
                    split = split.Where(x => x != "").ToList();
                    for (int j = 0; j < 5; j++)
                    {
                        board.Add(int.Parse(split[j]));
                    }
                    boards[index].Add(board);
                    board = new List<int>();
                }
            }


            foreach (var b in boards)
            {
                for (int i = 0; i < 5; i++)
                {
                    board = new List<int>();

                    for (int j = 0; j < 5; j++)
                    {
                        board.Add(b.Value[j][i]);
                    }

                    b.Value.Add(board);
                }
            }

            HashSet<int> drawnNumbers = new HashSet<int>();

            for (int i = 0; i < numbers.Count; i++)
            {
                int number = numbers[i];
                drawnNumbers.Add(number);

                foreach (var b in boards)
                {
                    if (victoriousBoards.Contains(b.Key))
                        continue;
                    foreach (var line in b.Value)
                    {
                        bool won = true;
                        for (int l = 0; l < 5; l++)
                        {
                            if (!drawnNumbers.Contains(line[l]))
                                won = false;
                        }

                        if (won)
                        {
                            if (!victoriousBoards.Contains(b.Key))
                            {
                                victoriousBoards.Add(b.Key);
                            }

                            if (victoriousBoards.Count == boards.Count)
                            {
                                int sum = 0;
                                for (int m = 0; m < 5; m++)
                                {
                                    for (int n = 0; n < 5; n++)
                                    {
                                        if (!drawnNumbers.Contains(b.Value[m][n]))
                                            sum += b.Value[m][n];
                                    }
                                }

                                int result = sum * number;

                                return result.ToString();
                            }
                        }
                    }
                }

            }

            return "";
        }
    }
}
