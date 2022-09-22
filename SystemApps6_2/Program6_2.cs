using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace SystemApps6_2
{
    // Запретить запуск второй копии приложения
    class Program6_2
    {
        const string MyProgramName = "MyProgramName";
        static void Main(string[] args)
        {
            Console.Title = "Test One Copy!!!";
            //Test1();
            Test2();
        }

        static void Test1()
        {
            Mutex mutex = new Mutex(true, MyProgramName, out bool isNew);
            if (isNew == false)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("PROGRAM IS RUN!!!!!!!!");
                Console.ForegroundColor = ConsoleColor.White;
                Console.ReadKey();
                return;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Start normal!");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Press ENTER...");
                Console.ReadLine();
            }
        }
        static void Test2()
        {
            Mutex mutex = null;
            try
            {
                mutex = Mutex.OpenExisting(MyProgramName);
            }
            catch (WaitHandleCannotBeOpenedException){ }

            if (mutex != null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("PROGRAM IS RUN!!!!!!!!");
                Console.ForegroundColor = ConsoleColor.White;
                Console.ReadKey();
                return;
            }
            else
            {
                mutex = new Mutex(true, MyProgramName);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Start normal!");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Press ENTER...");
                Console.ReadLine();
            }
        }
    }
}
