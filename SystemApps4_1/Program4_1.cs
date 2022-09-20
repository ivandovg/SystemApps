using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace SystemApps4_1
{
    class Program4_1
    {
        static Random random = new Random();
        static void Main(string[] args)
        {
            Console.Title = "SystemApps4_1";
            Console.WriteLine("\nStart Main()");
            ShowThreadPoolInfo();
            //Test1();
            //Test2();
            Test3();
            Console.WriteLine("\nContinue Main()");
            Console.WriteLine("\npress any key...");
            Console.ReadKey();
        }

        static void ShowThreadPoolInfo()
        {
            ThreadPool.GetAvailableThreads(out int wt, out int ct);
            Console.WriteLine($"WorkerThreads: {wt}, CompletionPortThreads: {ct}");

            ThreadPool.GetMaxThreads(out wt, out ct);
            Console.WriteLine($"Max WorkerThreads: {wt}, Max CompletionPortThreads: {ct}");

            ThreadPool.GetMinThreads(out wt, out ct);
            Console.WriteLine($"Min WorkerThreads: {wt}, Min CompletionPortThreads: {ct}");
        }
        static void Test2()
        {
            Console.WriteLine("\nStart Test2()");

            Action<object> action = Factorial;
            //List<IAsyncResult> results = new List<IAsyncResult>();
            for (int i = 0; i < 10; i++)
            {
                //ThreadPool.QueueUserWorkItem(Factorial, random.Next(4, 10));
                //var ar = action.BeginInvoke(random.Next(4, 10), null, null);
                //results.Add(ar);
                action.BeginInvoke(random.Next(4, 10), FactorialCallback, null);
            }

            //while (results.Count > 0)
            //{
            //    if (results[0].IsCompleted)
            //        results.RemoveAt(0);
            //    Thread.Sleep(100);
            //}
            Console.WriteLine("Test2: All Completed!!!");
        }

        static void FactorialCallback(IAsyncResult result)
        {
            Console.WriteLine($"End Callback Thread #{Thread.CurrentThread.ManagedThreadId}");
        }
        static void Test1()
        {
            Console.WriteLine("\nStart Test1()");

            for (int i = 0; i < 10; i++)
            {
                //ThreadPool.QueueUserWorkItem(RunFromPool, i);
                ThreadPool.QueueUserWorkItem(Factorial, random.Next(4, 10));
            }
        }
        static void RunFromPool(object state)
        {
            Console.WriteLine($"Start Trhead #{Thread.CurrentThread.ManagedThreadId}, value = {state}");
            Thread.Sleep(1000);
            Console.WriteLine($"End Trhead #{Thread.CurrentThread.ManagedThreadId}, value = {state}");
        }
        static void Factorial(object n)
        {
            Thread.Sleep(1000);
            Console.WriteLine($"Start Thread #{Thread.CurrentThread.ManagedThreadId}");
            int m = (int)n, f = 1;
            for (int i = 0; i < m; i++)
            {
                f *= (i + 1);
                Thread.Sleep(100);
            }
            Console.WriteLine($"End Thread #{Thread.CurrentThread.ManagedThreadId}, {m}! = {f}");
        }

        #region Test3
        public delegate int FactorialDelegate(int n);
        static int FactorialInt(int n)
        {
            Thread.CurrentThread.Name = n.ToString();
            Thread.Sleep(1000);
            Console.WriteLine($"Start Thread #{Thread.CurrentThread.ManagedThreadId}");
            int f = 1;
            for (int i = 0; i < n; i++)
            {
                f *= (i + 1);
                Thread.Sleep(100);
            }
            //Console.WriteLine($"End Thread #{Thread.CurrentThread.ManagedThreadId}, {n}! = {f}");
            return f;
        }
        static void FactorialIntCallback(IAsyncResult result)
        {
            FactorialDelegate factorial = result.AsyncState as FactorialDelegate;
            if (factorial == null)
                Console.WriteLine($"End Callback, result = null");
            else
            {
                int res = factorial.EndInvoke(result);
                Console.WriteLine($"End Thread #{Thread.CurrentThread.ManagedThreadId}, {Thread.CurrentThread.Name}! = {res}");
            }
        }

        static int threadCount = 0;
        static void Test3()
        {
            FactorialDelegate factorial = FactorialInt;
            threadCount = 20;
            for (int i = 0; i < 20; i++)
            {
                factorial.BeginInvoke(random.Next(4, 10), FactorialIntCallback, factorial);
            }
        }
        #endregion
    }
}
