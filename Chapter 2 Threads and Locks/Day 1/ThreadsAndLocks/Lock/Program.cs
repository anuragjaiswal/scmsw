using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Lock
{
    class Program
    {
        static private Counter _counter { get; set; }
        static private SafeCounter _safeCounter { get; set; }

        static private bool _answerReady { get; set; }
        static private int _answer { get; set; }

        static void Main(string[] args)
        {
            _counter = new Counter();
            Thread oThread1 = new Thread(Run);
            Thread oThread2 = new Thread(Run);
            oThread1.Start();
            oThread2.Start();
            oThread1.Join();
            oThread2.Join();
            Console.WriteLine(_counter.count);

            _safeCounter = new SafeCounter();

            Thread oThread3 = new Thread(SafeRun);
            Thread oThread4 = new Thread(SafeRun);
            oThread3.Start();
            oThread4.Start();
            oThread3.Join();
            oThread4.Join();
            Console.WriteLine(_safeCounter.count);

            Thread oThread5 = new Thread(AnswerReady);
            Thread oThread6 = new Thread(CheckAnswer);
            oThread5.Start();
            oThread6.Start();
            oThread5.Join();
            oThread6.Join();

        }

        static public void Run()
        {
            for (int i = 0; i < 10000; i++)
			{
			    _counter.increment();
			}
        }

        static public void SafeRun()
        {
            for (int i = 0; i < 10000; i++)
            {
                _safeCounter.increment();
            }
        }

        static public void AnswerReady()
        {
            _answer = 42;
            _answerReady = true;
        }

        static public void CheckAnswer()
        {
            if(_answerReady)
            {
                Console.WriteLine("The meaning of Life is " + _answer);
            }
            else 
            {
                Console.WriteLine("I dont know the answer");
            }
        }
    }

    class Counter
    {
        public int count { get; private set;}

        public Counter()
        {
            count = 0;
        }

        public void increment()
        {
            ++count;
        }
    }

    class SafeCounter
    {
        public int count { get; private set; }

        public SafeCounter()
        {
            count = 0;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void increment()
        {
            ++count;
        }
    }
}
