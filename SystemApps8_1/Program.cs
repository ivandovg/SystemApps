using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace SystemApps8_1
{
    class Program
    {
        static void Main(string[] args)
        {
            //Test1ParallelInvoke();
            //Test2ParallelFor();
            //Test3ParallelForEachToken();
            Test4ParallelAsParallel();

            Console.WriteLine("\npress any key...");
            Console.ReadKey();
        }

        static void Test1ParallelInvoke()
        {
            Parallel.Invoke(
                () => 
                { 
                    Thread.Sleep(1500); 
                    Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} >> Hello"); 
                },
                () =>
                {
                    Thread.Sleep(1500); 
                    Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} >> Date {DateTime.Now.ToShortDateString()}");
                },
                () =>
                {
                    Thread.Sleep(1500); 
                    Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} >> Time {DateTime.Now.ToLongTimeString()}");
                },
                Calculate
                );
        }

        static void Calculate()
        {
            Thread.Sleep(1000);
            Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} >> Calculate()");
            for (int i = 0; i < 10; i++)
            {
                Console.Write($"{i} ");
            }
        }

        static void Test2ParallelFor()
        {
            List<int> nums = new List<int>() { 1, 2, 3, 7, 8, 6, 8, 9 };
            ParallelLoopResult result = Parallel.For(0, nums.Count,
                i =>
                {
                    Thread.Sleep(500);
                    Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} >> {i} ^ 2 = {i * i}");
                }
                );

            ParallelLoopResult result1 = Parallel.ForEach(nums, Factorial);
            if (result1.IsCompleted)
            {
                Console.WriteLine("Parallel.ForEach Completed");
            }
            else
            {
                Console.WriteLine("Parallel.ForEach break on " + result1.LowestBreakIteration.ToString());
            }
        }

        static object lc = new object();
        static void Factorial(int n, ParallelLoopState pls)
        {
            Thread.Sleep(1000);
            lock (lc)
            {
                if (n == 0) pls.Break();
                //if (n == 3) pls.Break();
                //if (n == 7) pls.Stop();

                int f = 1;
                for (int i = 1; i <= n; i++)
                {
                    f *= i;
                }
                Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} >> {n}! = {f}");
            }
        }

        static void Test3ParallelForEachToken()
        {
            List<int> nums = new List<int>() { 1, 2, 3, 7, 8, 6, 8, 9 };

            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            CancellationToken token = cancellationTokenSource.Token;

            Task.Factory.StartNew(() =>
            {
                Thread.Sleep(1000);
                cancellationTokenSource.Cancel();
            });

            try
            {
                Parallel.ForEach(nums, new ParallelOptions() { CancellationToken = token }, Factorial);
            }
            catch(OperationCanceledException ex)
            {
                Console.WriteLine("Cancel!!! " + ex.Message);
            }
            catch(Exception ex)
            {
                Console.WriteLine("ERROR! " + ex.Message);
            }
            finally
            {
                cancellationTokenSource.Dispose();
            }
        }

        static Random random = new Random();
        static void Test4ParallelAsParallel()
        {
            List<int> nums = new List<int>();
            for (int i = 0; i < 20; i++)
            {
                nums.Add(random.Next(10, 1000));
            }

            //var result = nums.AsParallel().Select(n => n * n);
            //result.ForAll(n => Console.WriteLine($"{n} "));

            //var result = from n in nums.AsParallel()
            //             select (n * n);

            (from n in nums.AsParallel()
             select (n * n)).ForAll(n => Console.WriteLine($"{n} "));


            //Parallel.ForEach(result, n => Console.WriteLine($"{n} "));
        }
    }
}
