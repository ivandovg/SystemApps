using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace SystemApps6_1
{
    public class Reader
    {
        static Semaphore semaphore = new Semaphore(3, 3);
        Thread thread;
        int count = 3;
        public Reader(int i, bool startAtInitial = true)
        {
            thread = new Thread(Read);
            thread.IsBackground = true;
            thread.Name = "Reader " + i.ToString();
            if (startAtInitial)
                thread.Start();
        }
        public void Start() => thread.Start();
        private void Read()
        {
            while (count>0)
            {
                semaphore.WaitOne();
                Console.WriteLine($"{thread.Name} enter library");
                Console.WriteLine($"{thread.Name} read");
                Thread.Sleep(2000);
                Console.WriteLine($"{thread.Name} exit library");
                semaphore.Release();
                count--;
                Thread.Sleep(1500);
            }
        }
    }
}
