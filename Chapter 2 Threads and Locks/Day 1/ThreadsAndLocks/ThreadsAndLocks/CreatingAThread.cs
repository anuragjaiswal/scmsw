using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadsAndLocks
{
    public class CreatingAThread
    {
        public static int Main()
        {
            HelloWorld h = new HelloWorld();
            Thread oThread = new Thread(new ThreadStart(h.Run));
            // Start the thread
            oThread.Start();
            Thread.Yield();
            Console.WriteLine("Hello from main thread");
            oThread.Join();
            return 0;
        }
    }

    public class HelloWorld
    {
       public void Run()
       {
            Console.WriteLine("Hello from new thread");
       }
    };
}
