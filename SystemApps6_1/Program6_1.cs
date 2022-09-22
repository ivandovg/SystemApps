using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace SystemApps6_1
{
    class Program6_1
    {
        static void Main(string[] args)
        {
            Console.Title = "Test Semaphore";
            for (int i = 0; i < 6; i++)
            {
                new Reader(i + 1);
            }
            Console.WriteLine("Press any key...");
            Console.ReadKey();
        }
    }
}
