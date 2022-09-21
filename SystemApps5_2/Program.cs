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
    class Program
    {
        static Random random = new Random();
        const string mutexName = "Summ";
        static void Main(string[] args)
        {
            int[] nums = new int[20];
            Mutex mutex = new Mutex(true, mutexName);
            Thread thread1 = new Thread(GenerateNums);
            Thread thread2 = new Thread(Summ);

            thread1.Start(nums);
            mutex.ReleaseMutex();
            thread2.Start(nums);


            thread1.Join();
            thread2.Join();
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
        }
    }
}
