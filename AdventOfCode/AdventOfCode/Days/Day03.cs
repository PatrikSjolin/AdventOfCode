using System;

namespace AdventOfCode.Days
{
    public class Day03 : IPuzzle
    {
        public string RunOne()
        {
            int input = 265149;

            int sum = 1;
            int steps = 0;
            for (int i = 8; ; i += 8)
            {
                steps++;

                //Found circle containing input (steps = number of moves to edge)
                if (sum + i + 8 > input)
                {
                    int stepsLeftInNewCircle = input - sum;
                    //Circle is divided in 8 sections where steps is the number of steps in each section
                    int stepsFromEdge = stepsLeftInNewCircle % steps;

                    int answer = steps + stepsFromEdge;

                    return answer.ToString();
                }

                sum += i;
            }
        }

        public string RunTwo()
        {
            int input = 265149;
            int gridSize = 16;

            int[,] grid = new int[gridSize, gridSize];

            int x = gridSize / 2;
            int y = gridSize / 2;

            grid[x, y] = 1;
            grid[x + 1, y] = 1;
            x += 1;

            while (true)
            {
                Tuple<int, int> tuple = GetNext(x, y, grid);
                x = tuple.Item1;
                y = tuple.Item2;

                int sum = GetSum(x, y, grid);
                if (sum > input)
                {
                    return sum.ToString();
                }
                grid[x, y] = sum;
            }
        }

        bool goLeft = false;
        bool goUp = true;
        bool goRight = false;
        bool goDown = false;

        Tuple<int, int> GetNext(int x, int y, int[,] grid)
        {
            if (goUp)
            {
                if (grid[x - 1, y] == 0)
                {
                    goLeft = true;
                    goUp = false;
                    return new Tuple<int, int>(x - 1, y);
                }
                else
                {
                    return new Tuple<int, int>(x, y + 1);
                }
            }
            else if (goLeft)
            {
                if (grid[x, y - 1] == 0)
                {
                    goLeft = false;
                    goDown = true;
                    return new Tuple<int, int>(x, y - 1);
                }
                else
                {
                    return new Tuple<int, int>(x - 1, y);
                }
            }
            else if (goDown)
            {
                if (grid[x + 1, y] == 0)
                {
                    goDown = false;
                    goRight = true;
                    return new Tuple<int, int>(x + 1, y);
                }
                else
                {
                    return new Tuple<int, int>(x, y - 1);
                }
            }
            else if (goRight)
            {
                if (grid[x, y + 1] == 0)
                {
                    goRight = false;
                    goUp = true;
                    return new Tuple<int, int>(x, y + 1);
                }
                else
                {
                    return new Tuple<int, int>(x + 1, y);
                }
            }

            return new Tuple<int, int>(1, 1);
        }

        public int GetSum(int x, int y, int[,] grid)
        {
            int sum = 0;

            sum += grid[x + 1, y];
            sum += grid[x, y + 1];
            sum += grid[x - 1, y];
            sum += grid[x, y - 1];
            sum += grid[x + 1, y + 1];
            sum += grid[x - 1, y - 1];
            sum += grid[x + 1, y - 1];
            sum += grid[x - 1, y + 1];

            return sum;
        }
    }
}
