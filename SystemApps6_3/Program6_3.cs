using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace SystemApps6_3
{
    class Program6_3
    {
        //static ManualResetEvent mEvent = new ManualResetEvent(true);
        static AutoResetEvent mEvent = new AutoResetEvent(true);
        const int threadsCount = 5;
        static void Main(string[] args)
        {
            Console.Title = "Test Event Sync";
            Test1Manual();

            Console.WriteLine("Press ENTER...");
            Console.ReadLine();
        }

        #region Test - ManualResetEvent
        static void Test1Manual()
        {
            Console.WriteLine("Start Test ManualResetEvent vs AutoResetEvent");
            Thread[] threads = new Thread[threadsCount];
            for (int i = 0; i < threads.Length; i++)
            {
                threads[i] = new Thread(Calculate);
                threads[i].IsBackground = true;
                threads[i].Start();
            }
            mEvent.Set();
            for (int i = 0; i < threads.Length; i++)
            {
                threads[i].Join();
            }
            Console.WriteLine($"\nManual Result X = {Counter.x}, Y = {Counter.y}");
        }
        static void Calculate()
        {
            Thread.Sleep(1000);
            Console.WriteLine($"Start Thread #{Thread.CurrentThread.ManagedThreadId}");
            mEvent.WaitOne();
            //mEvent.Reset(); // ManualResetEvent
            for (int i = 0; i < 1000000; i++)
            {
                ++Counter.x;
                if (Counter.x % 2 == 0)
                    ++Counter.y;
            }
            Console.WriteLine($"End Thread #{Thread.CurrentThread.ManagedThreadId}");

            mEvent.Set();
        }
        #endregion Test - ManualResetEvent
    }
    static class Counter
    {
        public static int x = 0;
        public static int y = 0;
    }
}
