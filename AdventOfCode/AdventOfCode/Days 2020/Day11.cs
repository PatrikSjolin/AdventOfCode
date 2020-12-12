using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Days_2020
{
    public class Day11 : IPuzzle
    {
        public bool Active => true;

        private List<string> inputs;

        public string RunOne()
        {
            inputs = System.IO.File.ReadAllLines(@"..\..\Data\2020\input11.txt").ToList();

            int width = inputs[0].Length;
            int height = inputs.Count;

            char[,] grid = new char[height, width];

            int lastOccupied = 0;

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    grid[i, j] = inputs[i][j];
                    if (grid[i, j] == '#')
                        lastOccupied++;
                }
            }

            while (true)
            {
                char[,] newGrid = new char[height, width];
                int occupied = 0;

                for (int i = 0; i < height; i++)
                {
                    for (int j = 0; j < width; j++)
                    {
                        char state = GetSeatState(grid, i, j, width, height);
                        newGrid[i, j] = state;
                        if (state == '#')
                            occupied++;
                    }
                }

                grid = newGrid;

                if (occupied == lastOccupied)
                    return occupied.ToString();

                lastOccupied = occupied;
            }
        }

        private char GetSeatState(char[,] grid, int row, int column, int width, int height)
        {
            if (grid[row, column] == '.')
                return '.';

            if (grid[row, column] == 'L')
            {
                for (int i = -1; i <= 1; i++)
                {
                    for (int j = -1; j <= 1; j++)
                    {
                        if (i == 0 && j == 0)
                            continue;
                        char state = GetState(grid, row + i, column + j, width, height);
                        if (state == '#')
                            return 'L';
                    }
                }
                return '#';
            }
            else
            {
                int occupied = 0;
                for (int i = -1; i <= 1; i++)
                {
                    for (int j = -1; j <= 1; j++)
                    {
                        if (i == 0 && j == 0)
                            continue;
                        char state = GetState(grid, row + i, column + j, width, height);
                        if (state == '#')
                            occupied++;
                    }
                }
                if (occupied >= 4)
                    return 'L';
                else
                    return '#';
            }
        }

        private char GetState(char[,] grid, int i, int j, int width, int height)
        {
            if (i < 0)
                return '0';
            if (j < 0)
                return '0';
            if (i >= height)
                return '0';
            if (j >= width)
                return '0';

            return grid[i, j];
        }

        public string RunTwo()
        {
            int width = inputs[0].Length;
            int height = inputs.Count;

            char[,] grid = new char[height, width];

            int lastOccupied = 0;

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    grid[i, j] = inputs[i][j];
                    if (grid[i, j] == '#')
                        lastOccupied++;
                }
            }

            while (true)
            {
                char[,] newGrid = new char[height, width];
                int occupied = 0;

                for (int i = 0; i < height; i++)
                {
                    for (int j = 0; j < width; j++)
                    {
                        char state = GetSeatState2(grid, i, j, width, height);
                        newGrid[i, j] = state;
                        if (state == '#')
                            occupied++;
                    }
                }

                grid = newGrid;

                if (occupied == lastOccupied)
                    return occupied.ToString();

                lastOccupied = occupied;
            }
        }

        private char GetSeatState2(char[,] grid, int row, int column, int width, int height)
        {
            if (grid[row, column] == '.')
                return '.';

            if (grid[row, column] == 'L')
            {
                for (int i = -1; i <= 1; i++)
                {
                    for (int j = -1; j <= 1; j++)
                    {
                        if (i == 0 && j == 0)
                            continue;
                        int deltaRow = i;
                        int deltaColumn = j;

                        char state = GetState(grid, row + deltaRow, column + deltaColumn, width, height);
                        while (state == '.' && state != '0')
                        {
                            deltaRow += i;
                            deltaColumn += j;
                            state = GetState(grid, row + deltaRow, column + deltaColumn, width, height);
                        }
                        if (state == '#')
                            return 'L';
                    }
                }
                return '#';
            }
            else
            {
                int occupied = 0;
                for (int i = -1; i <= 1; i++)
                {
                    for (int j = -1; j <= 1; j++)
                    {
                        if (i == 0 && j == 0)
                            continue;

                        int deltaRow = i;
                        int deltaColumn = j;
                        char state = GetState(grid, row + deltaRow, column + deltaColumn, width, height);
                        while (state == '.' && state != '0')
                        {
                            deltaRow += i;
                            deltaColumn += j;
                            state = GetState(grid, row + deltaRow, column + deltaColumn, width, height);
                        }
                        if (state == '#')
                            occupied++;
                    }
                }
                if (occupied >= 5)
                    return 'L';
                else
                    return '#';
            }
        }
    }
}
