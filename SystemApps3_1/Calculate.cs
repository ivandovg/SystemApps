using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace SystemApps3_1
{
    public class Calculate
    {
        private int n;
        public Calculate(int n)
        {
            this.n = n;
        }

        public void Factorial()
        {
            Thread.Sleep(1000);
            Console.WriteLine($"Start Thread {Thread.CurrentThread.ManagedThreadId}");
            int f = 1;
            for (int i = 0; i < n; i++)
            {
                f *= (i + 1);
                Thread.Sleep(100);
            }
            Console.WriteLine($"{Thread.CurrentThread.Name} {Thread.CurrentThread.ManagedThreadId}, {n}! = {f}");
        }
    }
}
