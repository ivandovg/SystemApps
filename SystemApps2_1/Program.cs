using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace SystemApps2_1
{
    class Program
    {
        static void Main(string[] args)
        {
            PrintAllProcesses();

            Console.WriteLine("Press any key...");
            Console.ReadKey();
        }

        private static void PrintAllProcesses()
        {
            var procs = Process.GetProcesses();
            Console.WriteLine("Handle\tId\tProcessName\tThreads.Count");
            foreach (Process p in procs)
            {
                try
                {
                    Console.WriteLine($"{p.Handle}\t{p.Id}\t{p.ProcessName}\t{p.Threads.Count}");
                }
                catch (Exception)
                {
                    Console.WriteLine("\t\t" + p.ProcessName);
                }
            }
        }
    }
}
