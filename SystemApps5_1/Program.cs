using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;

namespace SystemApps5_1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Test Sync";
            Test1SyncNet();
            //Test2SyncNet();

            Console.WriteLine("Press any key...");
            Console.ReadKey();
        }
        #region Test1SyncNet
        static void Test1SyncNet()
        {
            Console.Title = "Test Lock";
            Stopwatch stopwatch = Stopwatch.StartNew();

            Thread[] threads = new Thread[5];
            Console.WriteLine("Start Threads");
            for (int i = 0; i < threads.Length; i++)
            {
                threads[i] = new Thread(CalculateLock);
                threads[i].Start();
            }
            Console.WriteLine("Wait Threads");
            for (int i = 0; i < threads.Length; i++)
            {
                threads[i].Join();
            }
            stopwatch.Stop();
            Console.WriteLine($"(lock)Result X = {Counter.x}, Y = {Counter.y}, Elapsed = {stopwatch.ElapsedMilliseconds}");
        }
        static object lockObject = new object();
        static void CalculateLock()
        {
            Console.WriteLine($"Start Thread #{Thread.CurrentThread.ManagedThreadId}");

            lock (lockObject)
            {
                for (int i = 0; i < 1000000; i++)
                {
                    ++Counter.x;
                    if (Counter.x % 2 == 0)
                        ++Counter.y;
                }
            };
            Console.WriteLine($"End Thread #{Thread.CurrentThread.ManagedThreadId}");
        }
        static void Calculate()
        {
            Thread.Sleep(1000);
            for (int i = 0; i < 1000000; i++)
            {
                //++Counter.x; // v1 - no sync

                //Interlocked.Increment(ref Counter.x); // v2 - sync Interlocked
                //if (Counter.x % 2 == 0)
                //    Interlocked.Increment(ref Counter.y);

                // v3 - Monitor
                //Monitor.Enter(this); - ЭТО ПЛОХО!!!
                //Monitor.Enter(typeof(StaticClass)); - для статических классов
                //Monitor.Enter(lockObject);
                //try
                //{
                //    ++Counter.x;
                //    if (Counter.x % 2 == 0)
                //        ++Counter.y;
                //}
                //finally
                //{
                //    Monitor.Exit(lockObject);
                //}

                // v4 - lock
                //lock(this); - ЭТО ПЛОХО!!!
                //lock(typeof(StaticClass)); - для статических классов
                lock (lockObject)
                {
                    ++Counter.x;
                    if (Counter.x % 2 == 0)
                        ++Counter.y;
                };
            }
        }
        #endregion

        #region Test2SyncNet
        static Mutex mutex;
        static void Test2SyncNet()
        {
            Console.Title = "Test Mutex";
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            Thread[] threads = new Thread[5];
            Console.WriteLine("Start Threads");
            
            //mutex = new Mutex(true);
            mutex = new Mutex(false);
            
            for (int i = 0; i < threads.Length; i++)
            {
                threads[i] = new Thread(Calculate2);
                threads[i].Start();
            }
            //mutex.ReleaseMutex();

            Console.WriteLine("Wait Threads");
            for (int i = 0; i < threads.Length; i++)
            {
                threads[i].Join();
            }
            stopwatch.Stop();
            Console.WriteLine($"(mutex)Result X = {Counter.x}, Y = {Counter.y}, Elapsed = {stopwatch.ElapsedMilliseconds}");
        }

        static void Calculate2()
        {
            mutex.WaitOne();
            Console.WriteLine($"Start Thread #{Thread.CurrentThread.ManagedThreadId}");
            //try
            //{
                for (int i = 0; i < 1000000; i++)
                {
                    ++Counter.x;
                    if (Counter.x % 2 == 0)
                        ++Counter.y;
                }
            //}
            //finally
            //{
                Console.WriteLine($"End Thread #{Thread.CurrentThread.ManagedThreadId}");
                mutex.ReleaseMutex();
            //}
        }
        #endregion
    }

    static class Counter
    {
        public static int x = 0;
        public static int y = 0;
    }
}
