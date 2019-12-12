using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Days_2019
{
    public class Day12 : IPuzzle
    {
        public bool Active => false;

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

        //public class State
        //{
        //    public int moon1x { get; set; }
        //    public int moon1y { get; set; }
        //    public int moon1z { get; set; }

        //    public int moon1xv { get; set; }
        //    public int moon1yv { get; set; }
        //    public int moon1zv { get; set; }

        //    public int moon2x { get; set; }
        //    public int moon2y { get; set; }
        //    public int moon2z { get; set; }

        //    public int moon2xv { get; set; }
        //    public int moon2yv { get; set; }
        //    public int moon2zv { get; set; }

        //    public int moon3x { get; set; }
        //    public int moon3y { get; set; }
        //    public int moon3z { get; set; }
        //    public int moon3xv { get; set; }
        //    public int moon3yv { get; set; }
        //    public int moon3zv { get; set; }

        //    public int moon4x { get; set; }
        //    public int moon4y { get; set; }
        //    public int moon4z { get; set; }
        //    public int moon4xv { get; set; }
        //    public int moon4yv { get; set; }
        //    public int moon4zv { get; set; }

        //    public override bool Equals(object obj)
        //    {

        //        State s = (State)obj;
        //        return s.moon1x == moon1x &&
        //               s.moon1xv == moon1xv &&
        //               s.moon1y == moon1y &&
        //               s.moon1yv == moon1yv &&
        //               s.moon1z == moon1z &&
        //               s.moon1zv == moon1zv &&
        //               s.moon2x == moon2x &&
        //               s.moon2xv == moon2xv &&
        //               s.moon2y == moon2y &&
        //               s.moon2yv == moon2yv &&
        //               s.moon2z == moon2z &&
        //               s.moon2zv == moon2zv &&
        //               s.moon3x == moon3x &&
        //               s.moon3xv == moon3xv &&
        //               s.moon3y == moon3y &&
        //               s.moon3yv == moon3yv &&
        //               s.moon3z == moon3z &&
        //               s.moon3zv == moon3zv &&
        //               s.moon4x == moon4x &&
        //               s.moon4xv == moon4xv &&
        //               s.moon4y == moon4y &&
        //               s.moon4yv == moon4yv &&
        //               s.moon4z == moon4z &&
        //               s.moon4zv == moon4zv;
        //    }

        //    public override int GetHashCode()
        //    {
        //        return moon1x + moon1xv + moon1y + moon1yv + moon1z + moon1zv + moon2x + moon2xv + moon2y + moon2yv + moon2z + moon2zv + moon3x + moon3xv + moon3y + moon3yv + moon3z + moon3zv + moon4x + moon4xv + moon4y + moon4yv + moon4z + moon4zv;
        //    }
        //}

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

            HashSet<State> states = new HashSet<State>();

            for (int i = 0; i < moonsPositions.Count; i++)
            {
                moonsVelocities.Add(new Point3D(0, 0, 0));
            }

            states.Add(new State
            {
                Position1 = moonsPositions[0].X,
                Position2 = moonsPositions[1].X,
                Position3 = moonsPositions[2].X,
                Position4 = moonsPositions[3].X,

                Velocity1 = moonsVelocities[0].X,
                Velocity2 = moonsVelocities[1].X,
                Velocity3 = moonsVelocities[2].X,
                Velocity4 = moonsVelocities[3].X,
            });

            long xRepeat = 0;

            for (long i = 0; i < 4686774924; i++)
            {
                for (int j = 0; j < moonsPositions.Count; j++)
                {
                    for (int k = 0; k < moonsPositions.Count; k++)
                    {
                        if (j == k)
                            continue;

                        int change = UpdateMoonVelocity(moonsPositions[j].X, moonsPositions[k].X);
                        moonsVelocities[j].X += change;;
                    }
                }

                for (int j = 0; j < moonsVelocities.Count; j++)
                {
                    moonsPositions[j].X += moonsVelocities[j].X;
                }


                State moonstate = new State
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

                if (states.Contains(moonstate))
                {
                    xRepeat = i + 1;
                    break;
                }
            }

            states.Clear();
            states.Add(new State
            {
                Position1 = moonsPositions[0].Y,
                Position2 = moonsPositions[1].Y,
                Position3 = moonsPositions[2].Y,
                Position4 = moonsPositions[3].Y,

                Velocity1 = moonsVelocities[0].Y,
                Velocity2 = moonsVelocities[1].Y,
                Velocity3 = moonsVelocities[2].Y,
                Velocity4 = moonsVelocities[3].Y,
            });

            long yRepeat = 0;

            for (long i = 0; i < 4686774924; i++)
            {
                for (int j = 0; j < moonsPositions.Count; j++)
                {
                    for (int k = 0; k < moonsPositions.Count; k++)
                    {
                        if (j == k)
                            continue;

                        int change = UpdateMoonVelocity(moonsPositions[j].Y, moonsPositions[k].Y);
                        moonsVelocities[j].Y += change; ;
                    }
                }

                for (int j = 0; j < moonsVelocities.Count; j++)
                {
                    moonsPositions[j].Y += moonsVelocities[j].Y;
                }


                State moonstate = new State
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

                if (states.Contains(moonstate))
                {
                    yRepeat = i + 1;
                    break;
                }
            }

            states.Clear();
            states.Add(new State
            {
                Position1 = moonsPositions[0].Z,
                Position2 = moonsPositions[1].Z,
                Position3 = moonsPositions[2].Z,
                Position4 = moonsPositions[3].Z,

                Velocity1 = moonsVelocities[0].Z,
                Velocity2 = moonsVelocities[1].Z,
                Velocity3 = moonsVelocities[2].Z,
                Velocity4 = moonsVelocities[3].Z,
            });

            long zRepeat = 0;

            for (long i = 0; i < 4686774924; i++)
            {
                for (int j = 0; j < moonsPositions.Count; j++)
                {
                    for (int k = 0; k < moonsPositions.Count; k++)
                    {
                        if (j == k)
                            continue;

                        int change = UpdateMoonVelocity(moonsPositions[j].Z, moonsPositions[k].Z);
                        moonsVelocities[j].Z += change; ;
                    }
                }

                for (int j = 0; j < moonsVelocities.Count; j++)
                {
                    moonsPositions[j].Z += moonsVelocities[j].Z;
                }


                State moonstate = new State
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

                if (states.Contains(moonstate))
                {
                    zRepeat = i + 1;
                    break;
                }
            }

            for(long i = 100000000000000; i < 2701771299153472; i++)
            {
                if(i % xRepeat == 0 && i % yRepeat == 0 && i % zRepeat == 0)
                {
                    return i.ToString();
                }
            }

            return "";
        }
    }
}
