using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Days_2019
{
    public class Day12 : IPuzzle
    {
        public bool Active => true;

        public string RunOne()
        {
            List<string> inputs = System.IO.File.ReadAllLines(@"..\..\Data\2019\input12.txt").ToList();

            List<Point3D> moonsPositions = new List<Point3D>();
            foreach (var i in inputs)
            {
                List<string> splits = i.Split(',').ToList();

                int x = int.Parse(splits[0].Split('=')[1]);
                int y = int.Parse(splits[1].Split('=')[1]);
                int z = int.Parse(splits[2].Split('=')[1].Replace(">", ""));
                moonsPositions.Add(new Point3D(x, y, z));
            }

            List<Point3D> moonsVelocities = new List<Point3D>();

            for (int i = 0; i < moonsPositions.Count; i++)
            {
                moonsVelocities.Add(new Point3D(0, 0, 0));
            }

            for (int i = 0; i < 1000; i++)
            {
                for (int j = 0; j < moonsPositions.Count; j++)
                {
                    for (int k = 0; k < moonsPositions.Count; k++)
                    {
                        if (j == k)
                            continue;

                        int change = UpdateMoonVelocity(moonsPositions[j].X, moonsPositions[k].X);
                        moonsVelocities[j].X += change;

                        change = UpdateMoonVelocity(moonsPositions[j].Y, moonsPositions[k].Y);
                        moonsVelocities[j].Y += change;

                        change = UpdateMoonVelocity(moonsPositions[j].Z, moonsPositions[k].Z);
                        moonsVelocities[j].Z += change;
                    }
                }

                for (int j = 0; j < moonsVelocities.Count; j++)
                {
                    moonsPositions[j].X += moonsVelocities[j].X;
                    moonsPositions[j].Y += moonsVelocities[j].Y;
                    moonsPositions[j].Z += moonsVelocities[j].Z;
                }
            }

            int energy = 0;

            for (int i = 0; i < moonsPositions.Count; i++)
            {
                int potential = Math.Abs(moonsPositions[i].X) + Math.Abs(moonsPositions[i].Y) + Math.Abs(moonsPositions[i].Z);
                int kinetic = Math.Abs(moonsVelocities[i].X) + Math.Abs(moonsVelocities[i].Y) + Math.Abs(moonsVelocities[i].Z);
                energy += potential * kinetic;
            }

            return energy.ToString();
        }

        private int UpdateMoonVelocity(int a, int b)
        {
            if (a > b)
                return -1;
            if (a < b)
                return 1;
            return 0;
        }

        public class State
        {
            public int Position1 { get; set; }
            public int Position2 { get; set; }
            public int Position3 { get; set; }
            public int Position4 { get; set; }


            public int Velocity1 { get; set; }
            public int Velocity2 { get; set; }
            public int Velocity3 { get; set; }
            public int Velocity4 { get; set; }

            public override bool Equals(object obj)
            {

                State s = (State)obj;
                return s.Position1 == Position1 &&
                       s.Position2 == Position2 &&
                       s.Position3 == Position3 &&
                       s.Position4 == Position4 &&
                       s.Velocity1 == Velocity1 &&
                       s.Velocity2 == Velocity2 &&
                       s.Velocity3 == Velocity3 &&
                       s.Velocity4 == Velocity4;
            }

            public override int GetHashCode()
            {
                return Position1 + Position2 + Position3 + Position4 + Velocity1 + Velocity2 + Velocity3 + Velocity4;
            }
        }

        public string RunTwo()
        {
            List<string> inputs = System.IO.File.ReadAllLines(@"..\..\Data\2019\input12.txt").ToList();

            List<Point3D> moonsPositions = new List<Point3D>();
            foreach (var i in inputs)
            {
                List<string> splits = i.Split(',').ToList();

                int x = int.Parse(splits[0].Split('=')[1]);
                int y = int.Parse(splits[1].Split('=')[1]);
                int z = int.Parse(splits[2].Split('=')[1].Replace(">", ""));

                moonsPositions.Add(new Point3D(x, y, z));
            }

            List<Point3D> moonsVelocities = new List<Point3D>();

            for (int i = 0; i < moonsPositions.Count; i++)
            {
                moonsVelocities.Add(new Point3D(0, 0, 0));
            }

            State state = new State
            {
                Position1 = moonsPositions[0].X,
                Position2 = moonsPositions[1].X,
                Position3 = moonsPositions[2].X,
                Position4 = moonsPositions[3].X,

                Velocity1 = moonsVelocities[0].X,
                Velocity2 = moonsVelocities[1].X,
                Velocity3 = moonsVelocities[2].X,
                Velocity4 = moonsVelocities[3].X,
            };

            long xRepeat = StepsUntilRepeat(state);

            state = new State
            {
                Position1 = moonsPositions[0].Y,
                Position2 = moonsPositions[1].Y,
                Position3 = moonsPositions[2].Y,
                Position4 = moonsPositions[3].Y,

                Velocity1 = moonsVelocities[0].Y,
                Velocity2 = moonsVelocities[1].Y,
                Velocity3 = moonsVelocities[2].Y,
                Velocity4 = moonsVelocities[3].Y,
            };

            long yRepeat = StepsUntilRepeat(state);

            state = new State
            {
                Position1 = moonsPositions[0].Z,
                Position2 = moonsPositions[1].Z,
                Position3 = moonsPositions[2].Z,
                Position4 = moonsPositions[3].Z,

                Velocity1 = moonsVelocities[0].Z,
                Velocity2 = moonsVelocities[1].Z,
                Velocity3 = moonsVelocities[2].Z,
                Velocity4 = moonsVelocities[3].Z,
            };

            long zRepeat = StepsUntilRepeat(state);

            List<int> primeFactorsX = GetPrimeFactors(xRepeat);
            List<int> primeFactorsY = GetPrimeFactors(yRepeat);
            List<int> primeFactorsZ = GetPrimeFactors(zRepeat);

            for (int i = 0; i < primeFactorsX.Count; i++)
            {
                if (primeFactorsY.Contains(primeFactorsX[i]) && primeFactorsZ.Contains(primeFactorsX[i]))
                {
                    primeFactorsY.Remove(primeFactorsX[i]);
                    primeFactorsZ.Remove(primeFactorsX[i]);
                    primeFactorsX.Remove(primeFactorsX[i]);
                }
            }

            primeFactorsX.AddRange(primeFactorsY);
            primeFactorsX.AddRange(primeFactorsZ);

            long sum = 1;

            for (int i = 0; i < primeFactorsX.Count; i++)
            {
                sum *= primeFactorsX[i];
            }

            return sum.ToString();
        }

        private long StepsUntilRepeat(State state)
        {
            int[] moonsPositions = new int[4];
            int[] moonsVelocities = new int[4];

            moonsPositions[0] = state.Position1;
            moonsPositions[1] = state.Position2;
            moonsPositions[2] = state.Position3;
            moonsPositions[3] = state.Position4;

            moonsVelocities[0] = state.Velocity1;
            moonsVelocities[1] = state.Velocity2;
            moonsVelocities[2] = state.Velocity3;
            moonsVelocities[3] = state.Velocity4;

            long xRepeat = 0;

            for (long i = 0; i < 4686774924; i++)
            {
                for (int j = 0; j < moonsPositions.Length; j++)
                {
                    for (int k = 0; k < moonsPositions.Length; k++)
                    {
                        if (j == k)
                            continue;

                        int change = UpdateMoonVelocity(moonsPositions[j], moonsPositions[k]);
                        moonsVelocities[j] += change; ;
                    }
                }

                for (int j = 0; j < moonsVelocities.Length; j++)
                {
                    moonsPositions[j] += moonsVelocities[j];
                }


                State moonstate = new State
                {
                    Position1 = moonsPositions[0],
                    Position2 = moonsPositions[1],
                    Position3 = moonsPositions[2],
                    Position4 = moonsPositions[3],

                    Velocity1 = moonsVelocities[0],
                    Velocity2 = moonsVelocities[1],
                    Velocity3 = moonsVelocities[2],
                    Velocity4 = moonsVelocities[3],
                };

                if (state.Equals(moonstate))
                {
                    xRepeat = i + 1;
                    break;
                }
            }

            return xRepeat;
        }

        private List<int> GetPrimeFactors(long xRepeat)
        {
            List<int> primes = new List<int>();
            int div = 2;

            while (true)
            {
                if (div == xRepeat)
                {
                    primes.Add(div);
                    return primes;
                }

                if (xRepeat % div == 0)
                {
                    primes.Add(div);
                    xRepeat = xRepeat / div;
                }
                else
                {
                    div++;
                }
            }
        }
    }
}
