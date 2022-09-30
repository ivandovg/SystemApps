using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace SystemApps5_2
{
    // Создаем два потока:
    // 1) заполняет массив числами
    // 2) выводит на экран массив и сумму
    class Program5_2
    {
        static Random random = new Random();
        const string mutexName = "Summ";
        const string mutexName2 = "Summ2";
        static void Main(string[] args)
        {
            int[] nums = new int[20];
            Mutex mutex = new Mutex(true, mutexName);
            Thread thread1 = new Thread(GenerateNums);
            Thread thread2 = new Thread(Summ);
            Thread thread3 = new Thread(Avg);

            thread1.Start(nums);
            mutex.ReleaseMutex();
            thread2.Start(nums);
            thread3.Start(nums);


            thread1.Join();
            thread2.Join();
            thread3.Join();
            Console.WriteLine("\nPress any key...");
            Console.ReadKey();
        }

        static void GenerateNums(object nums)
        {
            Mutex mutex = Mutex.OpenExisting(mutexName);
            mutex.WaitOne();
            Console.WriteLine("Generate array");
            int[] a = nums as int[];
            if (a == null)
                throw new ArgumentNullException();

            for (int i = 0; i < a.Length; i++)
            {
                a[i] = random.Next(10, 101);
            }
            mutex.ReleaseMutex();
        }
        static void Summ(object nums)
        {
            Mutex mutex = Mutex.OpenExisting(mutexName);
            mutex.WaitOne();
            Console.WriteLine("Calculate summ");
            int[] a = nums as int[];
            if (a == null)
                throw new ArgumentNullException();

            int s = 0;
            for (int i = 0; i < a.Length; i++)
            {
                s += a[i];
                Console.Write($"\t{a[i]}");
            }

            Console.WriteLine($"\nSumm = {s}");
            mutex.ReleaseMutex();
            Mutex mutex2 = new Mutex(false, mutexName2);
        }

        static void Avg(object nums)
        {
            Mutex mutex2;
            while (!Mutex.TryOpenExisting(mutexName2, out mutex2))
            {
                Thread.Sleep(10);
            }
            mutex2.WaitOne();
            Console.WriteLine("Calculate Avg");
            int[] a = nums as int[];
            if (a == null)
                throw new ArgumentNullException();

            int s = 0;
            for (int i = 0; i < a.Length; i++)
            {
                s += a[i];
            }

            Console.WriteLine($"\nAvg = {s / a.Length}");
            mutex2.ReleaseMutex();
        }
    }
}
