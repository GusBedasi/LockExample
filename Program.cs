using System;
using System.Threading;

namespace LockTest
{
    class Program
    {
        static readonly object _lock = new object();
        static int _total = 0;

        static void PlusFive()
        {
            for(int i = 0; i < 5; i++)
            {
                //Lock resource to have a consistent result avoinding race condition
                lock(_lock)
                {
                    _total++;
                    Console.WriteLine($"Total is {_total}, updated by {Thread.CurrentThread.Name}");
                }
            }
        }

        static void Main(string[] args)
        {
            // One thread manage the three instructions
            // PlusFive();
            // PlusFive();
            // PlusFive();

            //Three threads to create parallelims
            Thread t1 = new Thread(PlusFive) { Name = "Thread 1"};
            Thread t2 = new Thread(PlusFive) { Name = "Thread 2"};
            Thread t3 = new Thread(PlusFive) { Name = "Thread 3"};

            t1.Start();
            t2.Start();
            t3.Start();

            t1.Join();
            t2.Join();
            t3.Join();

            Console.WriteLine($"The total is {_total}");
            Console.ReadKey();
        }
    }
}
