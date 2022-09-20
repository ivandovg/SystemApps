using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace SystemApps3_1
{
    class Program3_1
    {
        static Random random = new Random();
        static void Main(string[] args)
        {
            Demo1();
            //Demo2();
            //Demo3();

            Console.WriteLine("\nContinue Main()");
            Console.WriteLine("\npress any key...");
            Console.ReadKey();
        }
        static void Demo3()
        {
            Thread thread = new Thread(Summ);
            thread.IsBackground = true;
            thread.Name = "CalcSumm";
            thread.Start(random.Next(10, 20));
            thread.Join();
            Console.WriteLine($"Calculated summ = {summ}");
        }
        static int summ = 0;
        static void Summ(object n)
        {
            Thread.Sleep(1000);
            Console.WriteLine($"Start ThreadSumm {Thread.CurrentThread.ManagedThreadId}");
            int m = (int)n, f = 0;
            for (int i = 0; i < m; i++)
            {
                f += (i + 1);
                Thread.Sleep(100);
            }
            summ = f;
            //Console.WriteLine($"{Thread.CurrentThread.Name} {Thread.CurrentThread.ManagedThreadId}, {m}! = {f}");
        }

        static void Demo1()
        {
            Console.WriteLine("Demo 1");
            //(new Thread(() => Demo2())).Start();
            
            Console.WriteLine("(D1)Create threads");
            Thread[] threads = new Thread[10];
            
            for (int i = 0; i < 10; i++)
            {
                threads[i] = new Thread(Factorial);
                threads[i].Name = $"(D1) Thread {Thread.CurrentThread.ManagedThreadId}";
                threads[i].IsBackground = true;
            }
            Console.WriteLine("(D1)Start threads");
            for (int i = 0; i < 10; i++)
            {
                threads[i].Start(random.Next(5, 15));
            }
        }
        static void Demo2()
        {
            Console.WriteLine("Demo 2");

            Console.WriteLine("(D2)Create threads");
            Thread[] threads = new Thread[10];
            
            for (int i = 0; i < 10; i++)
            {
                Calculate calculate = new Calculate(random.Next(5, 15));
                threads[i] = new Thread(calculate.Factorial);
                threads[i].Name = $"(D2) Thread {Thread.CurrentThread.ManagedThreadId}";
                threads[i].IsBackground = true;
            }
            Console.WriteLine("(D2)Start threads");
            for (int i = 0; i < 10; i++)
            {
                threads[i].Start();
            }
        }

        static void Factorial(object n)
        {
            Thread.Sleep(1000);
            Console.WriteLine($"Start Thread {Thread.CurrentThread.ManagedThreadId}");
            int m = (int)n, f = 1;
            for (int i = 0; i < m; i++)
            {
                f *= (i + 1);
                Thread.Sleep(100);
            }
            Console.WriteLine($"{Thread.CurrentThread.Name} {Thread.CurrentThread.ManagedThreadId}, {m}! = {f}");
        }
    }
}
