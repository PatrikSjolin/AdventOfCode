using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Days_2016
{
    public class Day18 : IPuzzle
    {
        public bool Active => false;

        public void GoOne()
        {
            string input = "^.^^^..^^...^.^..^^^^^.....^...^^^..^^^^.^^.^^^^^^^^.^^.^^^^...^^...^^^^.^.^..^^..^..^.^^.^.^.......";
            int rows = 40;

            List<string> rowsList = GetRows(input, rows);
            int count = 0;
            foreach(var s in rowsList)
            {
                foreach(var c in s)
                {
                    if(c == '.')
                    {
                        count++;
                    }
                }
            }
            
            Console.WriteLine(count);
        }

        private List<string> GetRows(string input, int rows)
        {
            List<string> listRows = new List<string>();
            string lastRow = input;
            listRows.Add(input);
            for(int i = 0; i < rows - 1; i++)
            {
                string nextRow = "";
                for(int j = 0; j < lastRow.Count(); j++)
                {
                    char tile = GetTile(j, lastRow);
                    nextRow += tile;
                }
                lastRow = nextRow;
                listRows.Add(nextRow);
            }
            return listRows;
        }

        private char GetTile(int j, string input)
        {
            char left;
            char middle;
            char right;
            if(j == 0)
            {
                left = '.';
            }
            else
            {
                left = input[j - 1];
            }

            if(j == input.Count() - 1)
            {
                right = '.';
            }
            else
            {
                right = input[j + 1];
            }

            middle = input[j];

            if(left == '^' && middle == '.' && right == '.')
            {
                return '^';
            }
            else if(left == '.' && middle == '.' && right == '^')
            {
                return '^';
            }
            else if(left == '^' && middle == '^' && right == '.')
            {
                return '^';
            }
            else if(left == '.' && middle == '^' && right == '^')
            {
                return '^';
            }
            return '.';
        }

        public void GoTwo()
        {
            string input = "^.^^^..^^...^.^..^^^^^.....^...^^^..^^^^.^^.^^^^^^^^.^^.^^^^...^^...^^^^.^.^..^^..^..^.^^.^.^.......";
            int rows = 400000;

            List<string> rowsList = GetRows(input, rows);
            int count = 0;
            foreach (var s in rowsList)
            {
                foreach (var c in s)
                {
                    if (c == '.')
                    {
                        count++;
                    }
                }
            }

            Console.WriteLine(count);
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
