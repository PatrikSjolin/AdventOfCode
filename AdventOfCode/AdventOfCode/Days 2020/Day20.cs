﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Days_2020
{
    public class Day20 : IPuzzle
    {
        public bool Active => false;

        private List<string> inputs;

        public string RunOne()
        {
            inputs = System.IO.File.ReadAllLines(@"..\..\Data\2020\input20.txt").ToList();
            return "";
        }

        public string RunTwo()
        {
            return "";
        }
    }
}
