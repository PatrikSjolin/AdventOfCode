using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AdventOfCode.Days_2019
{
    public class Message
    {
        public bool Ready { get; set; }
        public long X { get; set; }
        public long Y { get; set; }
        public int Address { get; set; }
        public bool PartiallyRead { get; set; }
        public int From { get; set; }

        public override bool Equals(object obj)
        {
            Message m = ((Message)obj);
            return m.Ready == Ready && m.X == X && m.Y == Y && m.Address == Address && m.PartiallyRead == PartiallyRead && m.From == From;
        }
    }

    public class Day23 : IPuzzle
    {
        public bool Active => false;

        public long natX { get; set; }
        public long natY { get; set; }
        public bool natReady { get; set; }

        public string RunOne()
        {
            List<long> inputs = System.IO.File.ReadAllLines(@"..\..\Data\2019\input23.txt")[0].Split(',').Select(x => long.Parse(x)).ToList();

            for (int i = 0; i < 10000; i++)
            {
                inputs.Add(0);
            }

            for(int i = 0; i < 50; i++)
            {
                messages.Add(i, new Queue<Message>());
            }

            for(int i = 0; i < 50; i++)
            {
                Computer c = new Computer(i) { ID = -1 };
                c.InputEvent += C_InputEvent;
                c.OutputEvent += C_OutputEvent;
                new Thread(() => c.Compute(inputs.ToArray())).Start();
            }


            while(true)
            {
                //Thread.Sleep(5);
                bool allIdle = true;

                for(int i = 0; i < 50; i++)
                {
                    allIdle = allIdle && messages[i].Count == 0;
                }

                if (allIdle && natReady)
                {
                    messages[0].Enqueue(new Message { Address = 0, From = 255, Ready = true, X = natX, Y = natY });
                    natReady = false;
                }
                if (second != 0)
                    break;
            }

            return first.ToString();
        }

        List<Message> ongoingMessages = new List<Message>();

        Dictionary<int, Queue<Message>> messages = new Dictionary<int, Queue<Message>>();

        private void C_OutputEvent(object sender, EventArgs e)
        {
            Computer c = ((Computer)sender);

            if (c.IsWriting)
            {
                Message m = null;
                lock (someListLock)
                {
                    m = ongoingMessages.First(x => x.From == c.ID);
                }
                if(m.X == int.MinValue)
                {
                    m.X = c.Output;
                }
                else
                {
                    if (m.Address == 255)
                    {
                        natX = m.X;
                        natY = c.Output;
                        //Console.WriteLine(c.Output);
                        natReady = true;
                        c.IsWriting = false;
                        lock (someListLock)
                        {
                            ongoingMessages.Remove(m);
                        }
                    }
                    else
                    {
                        m.Y = c.Output;
                        m.Ready = true;
                        c.IsWriting = false;
                        messages[m.Address].Enqueue(m);
                        lock (someListLock)
                        {
                            ongoingMessages.Remove(m);
                        }
                    }
                }
            }
            else
            {
                Message m = new Message { Address = (int)c.Output, X = int.MinValue, From = c.ID };
                lock (someListLock)
                {
                    ongoingMessages.Add(m);
                }
                c.IsWriting = true;
            }
        }
        private Object someListLock = new Object(); // only once

        int first;
        int second;

        List<long> ySentToZero = new List<long>();

        private void C_InputEvent(object sender, EventArgs e)
        {

            Computer c = ((Computer)sender);


            if (second != 0)
            {
                c.Input = 0;
                return;
            }

            if (c.ID == -1)
            {
                c.ID = (int)c.Input;
            }
            else
            {
                var queue = messages[c.ID];

                if(queue.Count > 0 && queue.Peek().Ready)
                {
                    var msg = queue.Peek();
                    if (msg.PartiallyRead)
                    {
                        if(c.ID == 0)
                        {
                            if(msg.From == 255)
                            {
                                if(first == 0)
                                {
                                    first = (int)msg.Y;
                                }
                                if(ySentToZero.Count > 0 && ySentToZero.Last() == msg.Y)
                                {
                                    second = (int)msg.Y;
                                }
                                ySentToZero.Add(msg.Y);
                            }
                        }

                        c.Input = msg.Y;
                        queue.Dequeue();
                    }
                    else
                    {
                        c.Input = msg.X;
                        msg.PartiallyRead = true;
                    }
                }
                else
                {
                    c.Input = -1;
                }
            }
        }

        public string RunTwo()
        {
            return second.ToString();
        }
    }
}
