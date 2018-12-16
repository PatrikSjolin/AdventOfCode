using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Days_2016
{
    public class Day17 : IPuzzle
    {
        public bool Active => false;

        public void GoOne()
        {
            string puzzleInput = "qtetzkpl";
            List<string> paths = new List<string>();
            GetPath(0, 0, puzzleInput, "", paths);

            string bestPath = "";
            int length = int.MaxValue;

            foreach(var path in paths)
            {
                if(path.Count() < length)
                {
                    length = path.Count();
                    bestPath = path;
                }
            }

            Console.WriteLine(bestPath);
        }

        private bool CanPass(char c, int x, int y)
        {
            return x >= 0 && y >= 0 && x < 4 && y < 4 && (c == 'b' || c == 'c' || c == 'd' || c == 'e' || c == 'f');
        }

        private void GetPath(int x, int y, string puzzleInput, string path, List<string> paths)
        {
            if(x == 3 && y == 3)
            {
                paths.Add(path);
                return;
            }

            string md5 = CalculateMD5Hash(puzzleInput + path).ToLower();

            if(CanPass(md5[0], x, y - 1))
            {
                GetPath(x, y - 1, puzzleInput, path + "U", paths);
            }
            if (CanPass(md5[1], x, y + 1))
            {
                GetPath(x, y + 1, puzzleInput, path + "D", paths);
            }
            if (CanPass(md5[2], x - 1, y))
            {
                GetPath(x - 1, y, puzzleInput, path + "L", paths);
            }
            if (CanPass(md5[3], x + 1, y))
            {
                GetPath(x + 1, y, puzzleInput, path + "R", paths);
            }
            return;
            //return "Not possible";
        }

        public void GoTwo()
        {
            string puzzleInput = "qtetzkpl";
            List<string> paths = new List<string>();
            GetPath(0, 0, puzzleInput, "", paths);

            int length = int.MaxValue;

            length = int.MinValue;

            foreach (var path in paths)
            {
                if (path.Count() > length)
                {
                    length = path.Count();
                }
            }
            Console.WriteLine(length);
        }

        public string CalculateMD5Hash(string input)
        {
            // step 1, calculate MD5 hash from input

            MD5 md5 = System.Security.Cryptography.MD5.Create();

            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);

            byte[] hash = md5.ComputeHash(inputBytes);

            // step 2, convert byte array to hex string

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }

            return sb.ToString();
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
