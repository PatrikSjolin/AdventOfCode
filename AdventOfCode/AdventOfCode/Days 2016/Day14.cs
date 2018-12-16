using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Days_2016
{
    public class Day14 : IPuzzle
    {
        public bool Active => false;

        public void GoOne()
        {
            string salt = "cuanljph";
            int index = 0;
            string hash = "";

            int keysFound = 0;

            for (int i = 0; ; i++)
            {
                 hash = CalculateMD5Hash(salt + i);

                for(int j = 0; j < hash.Length - 2; j++)
                {
                    if(hash[j] == hash[j+1] && hash[j+1] == hash[j + 2])
                    {
                        char c = hash[j];
                        for(int k = 1; k <= 1000; k++)
                        {
                            hash = CalculateMD5Hash(salt + (i + k));

                            for (int l = 0; l < hash.Length - 4; l++) { 

                                if (hash[l] == c && hash[l + 1] == c && hash[l+2] == c && hash[l+3] == c && hash[l+4] == c)
                                {
                                    keysFound++;
                                    break;
                                }
                            }
                        }
                        break;
                    }
                }

                if(keysFound == 64)
                {
                    index = i;
                    break;
                }
            }

            Console.WriteLine(index);
        }

        public void GoTwo()
        {
            string salt = "cuanljph";
            int index = 0;
            string hash = "";

            int keysFound = 0;

            Dictionary<int, string> cache = new Dictionary<int, string>();

            for (int i = 0; ; i++)
            {
                if (!cache.TryGetValue(i, out hash))
                {
                    hash = salt + i;
                    for (int r = 0; r <= 2016; r++)
                    {
                        hash = CalculateMD5Hash(hash).ToLower();
                    }
                }

                for (int j = 0; j < hash.Length - 2; j++)
                {
                    if (hash[j] == hash[j + 1] && hash[j + 1] == hash[j + 2])
                    {
                        char c = hash[j];
                        bool found = false;
                        for (int k = 1; k <= 1000; k++)
                        {
                            if(!cache.TryGetValue(i+k, out hash))
                            {
                                hash = salt + (i + k);
                                for (int r = 0; r <= 2016; r++)
                                {
                                    hash = CalculateMD5Hash(hash).ToLower();
                                }
                                cache.Add(i + k, hash);
                            }

                            for (int l = 0; l < hash.Length - 4; l++)
                            {
                                if (hash[l] == c && hash[l + 1] == c && hash[l + 2] == c && hash[l + 3] == c && hash[l + 4] == c)
                                {
                                    found = true;
                                    keysFound++;
                                    break;
                                }
                            }
                            if (found) break;
                        }
                        break;
                    }
                }

                if (keysFound == 64)
                {
                    index = i;
                    break;
                }
            }

            Console.WriteLine(index);
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
