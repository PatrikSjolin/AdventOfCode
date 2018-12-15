using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Days
{
    public class Particle
    {
        public int Id { get; set; }

        public int PositionX { get; set; }
        public int PositionY { get; set; }
        public int PositionZ { get; set; }

        public int VelocityX { get; set; }
        public int VelocityY { get; set; }
        public int VelocityZ { get; set; }

        public int AccelerationX { get; set; }
        public int AccelerationY { get; set; }
        public int AccelerationZ { get; set; }

    }

    public class Day20 : IPuzzle
    {
        public bool Active { get => true; }

        List<Particle> particles;

        public string RunOne()
        {
            List<string> inputLines = System.IO.File.ReadAllLines(@"..\..\Data\2017\input20.txt").ToList();
            particles = new List<Particle>();
            int count = 0;
            foreach(var input in inputLines)
            {
                var split = input.Split('<');

                Particle p = new Particle { Id = count };
                count++;

                List<string> pos = split[1].Split(',').ToList();

                p.PositionX = int.Parse(pos[0].Trim());
                p.PositionY = int.Parse(pos[1].Trim());
                p.PositionZ = int.Parse(pos[2].Replace(">", "").Trim());

                List<string> vel = split[2].Split(',').ToList();
                p.VelocityX = int.Parse(vel[0].Trim());
                p.VelocityY = int.Parse(vel[1].Trim());
                p.VelocityZ = int.Parse(vel[2].Replace(">", "").Trim());

                List<string> acc = split[3].Split(',').ToList();
                p.AccelerationX = int.Parse(acc[0].Trim());
                p.AccelerationY = int.Parse(acc[1].Trim());
                p.AccelerationZ = int.Parse(acc[2].Split('>')[0].Trim());

                particles.Add(p);
            }

            int bestParticle = -1;
            int bestAcceleration = 999999999;

            foreach(var p in particles)
            {
                int acc = Math.Abs(p.AccelerationX) + Math.Abs(p.AccelerationY) + Math.Abs(p.AccelerationZ);

                if(acc < bestAcceleration)
                {
                    bestAcceleration = acc;
                    bestParticle = p.Id;
                }
            }

            return bestParticle.ToString();
        }

        public string RunTwo()
        {
            HashSet<int> collisions = new HashSet<int>();

            for(int i = 0; i < particles.Count; i++)
            {
                for(int j = i+1; j < particles.Count; j++)
                {
                    if(DoesCollide(CreateNew(particles[i]), CreateNew(particles[j])))
                    {
                        if (!collisions.Contains(particles[i].Id))
                            collisions.Add(particles[i].Id);

                        if (!collisions.Contains(particles[j].Id))
                            collisions.Add(particles[j].Id);
                    }
                }
            }

            return (particles.Count - collisions.Count).ToString();
        }

        Particle CreateNew(Particle p)
        {
            Particle p1 = new Particle();

            p1.Id = p.Id;

            p1.PositionX = p.PositionX;
            p1.PositionY = p.PositionY;
            p1.PositionZ = p.PositionZ;

            p1.VelocityX = p.VelocityX;
            p1.VelocityY = p.VelocityY;
            p1.VelocityZ = p.VelocityZ;

            p1.AccelerationX = p.AccelerationX;
            p1.AccelerationY = p.AccelerationY;
            p1.AccelerationZ = p.AccelerationZ;

            return p1;
        }

        private bool DoesCollide(Particle particle1, Particle particle2)
        {
            for(int i = 0; i < 40; i++)
            {
                if(particle1.PositionX == particle2.PositionX && particle1.PositionY == particle2.PositionY && particle1.PositionZ == particle2.PositionZ)
                {
                    return true;
                }

                particle1.VelocityX += particle1.AccelerationX;
                particle1.VelocityY += particle1.AccelerationY;
                particle1.VelocityZ += particle1.AccelerationZ;

                particle1.PositionX += particle1.VelocityX;
                particle1.PositionY += particle1.VelocityY;
                particle1.PositionZ += particle1.VelocityZ;

                particle2.VelocityX += particle2.AccelerationX;
                particle2.VelocityY += particle2.AccelerationY;
                particle2.VelocityZ += particle2.AccelerationZ;

                particle2.PositionX += particle2.VelocityX;
                particle2.PositionY += particle2.VelocityY;
                particle2.PositionZ += particle2.VelocityZ;
            }

            return false;
        }
    }
}
