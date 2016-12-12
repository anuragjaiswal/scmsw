using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace More_Locks
{
    class Program
    {
        static void Main(string[] args)
        {
            #region InterruptThread
            //object o1 = new object();
            //object o2 = new object();

            //Thread t1 = new Thread(() => {
            //                    try
            //                    {
            //                        lock (o1)
            //                        {
            //                            Thread.Sleep(1000);
            //                            lock (o2)
            //                            {
            //                            }
            //                        }
            //                    }
            //                    catch(Exception ex)
            //                    {
            //                        Console.Write("Caught : " +  ex.ToString());
            //                    }

            //                });
            //Thread t2 = new Thread(() => {
            //                    try
            //                    {
            //                        lock (o2)
            //                        {
            //                            Thread.Sleep(1000);
            //                            lock (o1)
            //                            {
            //                            }
            //                        }
            //                    }
            //                    catch (Exception ex)
            //                    {
            //                        Console.Write("Caught : " + ex.ToString());
            //                    }
            //                });
            //t1.Start();
            //t2.Start();
            //Thread.Sleep(2000);
            //t1.Interrupt();
            //t2.Interrupt();
            //t1.Join();
            //t2.Join();
            #endregion

            #region timedout philosopher
            ChopStick c1 = new ChopStick(1);
            ChopStick c2 = new ChopStick(2);
            ChopStick c3 = new ChopStick(3);
            ChopStick c4 = new ChopStick(4);

            Philosopher p1 = new Philosopher(1, c1, c2);
            Philosopher p2 = new Philosopher(2, c2, c3);
            Philosopher p3 = new Philosopher(3, c3, c4);
            Philosopher p4 = new Philosopher(4, c4, c1);

            Thread oThread1 = new Thread(p1.Run);
            Thread oThread2 = new Thread(p2.Run);
            Thread oThread3 = new Thread(p3.Run);
            Thread oThread4 = new Thread(p4.Run);

            oThread1.Start();
            oThread2.Start();
            oThread3.Start();
            oThread4.Start();

            #endregion

            oThread1.Join();
            oThread2.Join();
            oThread3.Join();
            oThread4.Join();
        }
    }

    class Philosopher
    {
        private int id;
        private ChopStick left, right;
        private Random random;
        public Philosopher(int id, ChopStick left, ChopStick right)
        {
            this.id = id;
            this.left = left;
            this.right = right;
            this.random = new Random();
        }

        public void Run()
        {
            while (true)
            {
                Thread.Sleep(random.Next(1000)); //THINK
                bool leftLocked = Monitor.TryEnter(left, 3000);
                try
                {
                    Thread.Sleep(random.Next(2000)); //WAIT (uncommenting this will deadlock the code faster)
                    bool rightLocked = Monitor.TryEnter(right, 5000);
                    try
                    {
                        Console.WriteLine("[" + Thread.CurrentThread.ManagedThreadId + "] Philosopher " + this.id + " is Eating");
                        Thread.Sleep(random.Next(1000)); //EAT
                    }
                    finally
                    {
                        if (rightLocked)
                        {
                            Monitor.Exit(right);
                        }
                    }
                }
                finally
                {
                    if (leftLocked)
                    {
                        Monitor.Exit(left);
                    }
                }
            }
        }
    }

    class ChopStick
    {
        public int Id { get; set; }
        public ChopStick(int id)
        {
            this.Id = id;
        }
    }
}
