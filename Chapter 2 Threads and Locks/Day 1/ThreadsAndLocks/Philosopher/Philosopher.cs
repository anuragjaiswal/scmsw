using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Philosopher
{
    class Program
    {
        static void Main(string[] args)
        {
#region Deadlocked philosopher
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

#region Global Order philosopher
            ChopStick cg1 = new ChopStick(1);
            ChopStick cg2 = new ChopStick(2);
            ChopStick cg3 = new ChopStick(3);
            ChopStick cg4 = new ChopStick(4);

            GlobalOrderPhilosopher pg1 = new GlobalOrderPhilosopher(1, cg1, cg2);
            GlobalOrderPhilosopher pg2 = new GlobalOrderPhilosopher(2, cg2, cg3);
            GlobalOrderPhilosopher pg3 = new GlobalOrderPhilosopher(3, cg3, cg4);
            GlobalOrderPhilosopher pg4 = new GlobalOrderPhilosopher(4, cg4, cg1);

            Thread oThreadg1 = new Thread(pg1.Run);
            Thread oThreadg2 = new Thread(pg2.Run);
            Thread oThreadg3 = new Thread(pg3.Run);
            Thread oThreadg4 = new Thread(pg4.Run);

            oThreadg1.Start();
            oThreadg2.Start();
            oThreadg3.Start();
            oThreadg4.Start();

           
#endregion
            oThread1.Join();
            oThread2.Join();
            oThread3.Join();
            oThread4.Join();

            oThreadg1.Join();
            oThreadg2.Join();
            oThreadg3.Join();
            oThreadg4.Join();
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
            while(true)
            {
                Thread.Sleep(random.Next(1000)); //THINK
                lock(left)
                {
                    Thread.Sleep(random.Next(1000)); //WAIT (uncommenting this will deadlock the code faster)
                    lock(right)
                    {
                        Console.WriteLine("[" + Thread.CurrentThread.ManagedThreadId + "] Philosopher " + this.id + " is Eating");
                        Thread.Sleep(random.Next(1000)); //EAT
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

    class GlobalOrderPhilosopher
    {
        private int id;
        private ChopStick first, second;
        private Random random;
        public GlobalOrderPhilosopher(int id, ChopStick left, ChopStick right)
        {
            if(left.Id < right.Id)
            {
                this.first = left;
                this.second = right;
            }
            else
            {
                this.first = right;
                this.second = left;
            }
            this.id = id;
            this.random = new Random();
        }

        public void Run()
        {
            while (true)
            {
                Thread.Sleep(random.Next(1000)); //THINK
                lock (first)
                {
                    Thread.Sleep(random.Next(1000)); //WAIT (Unlike Philosopher class, uncommenting this will have no affect on deadlock)
                    lock (second)
                    {
                        Console.WriteLine("[" + Thread.CurrentThread.ManagedThreadId + "] GlobalOrderPhilosopher " + this.id + " is Eating");
                        Thread.Sleep(random.Next(1000)); //EAT
                    }
                }
            }
        }
    }
}
