using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SystemApps7_1
{
    class Program7_1
    {
        static void Main(string[] args)
        {
            Console.Title = "Test TPL";
            //Test1();
            //Test2();
            //Test3();
            //Test4();
            //Test5();
            Test6();

            Console.WriteLine("press any key...");
            Console.ReadKey();
        }

        static void Test1()
        {
            // v1
            //Task task = new Task(PrintTime);
            //task.Start();

            // v2
            Task task1 = Task.Run(PrintTime);
            
            // v3
            Task task2 = Task.Factory.StartNew(PrintTime);
        }

        static void PrintTime()
        {
            int count = 5;
            while (--count > 0)
            {
                Console.WriteLine($"Task {Task.CurrentId} {DateTime.Now.ToLongTimeString()}");
                System.Threading.Thread.Sleep(1000);
            }
        }
        static void Test2()
        {
            Task<double> task = new Task<double>(CalculateAvg);
            Console.WriteLine($"Task {task.Id}, Status = {task.Status}");
            // async start
            task.Start();
            //sync start
            //task.RunSynchronously();
            
            Console.WriteLine($"Task {task.Id}, Status = {task.Status}");
            System.Threading.Thread.Sleep(100);
            Console.WriteLine($"Task {task.Id}, Status = {task.Status}");

            while (!task.IsCompleted)
            {
                Console.Write(".");
                System.Threading.Thread.Sleep(100);
            }
            Console.WriteLine($"Result = {task.Result}, Status = {task.Status}");
        }

        static double CalculateAvg()
        {
            Console.WriteLine($"Start CalculateAvg");
            System.Threading.Thread.Sleep(700);
            List<int> n = new List<int>();
            Random random = new Random();
            Console.WriteLine("Fill array");
            System.Threading.Thread.Sleep(600);
            for (int j = 0; j < 10; j++)
            {
                n.Add(random.Next(10, 100));
            }
            Console.WriteLine("Calculate AVG");
            System.Threading.Thread.Sleep(700);
            int s = 0, i = 0;
            foreach (int x in n)
            {
                ++i;
                s += x;
            }

            Console.WriteLine("End CalculateAvg");
            return s / i;
        }

        static Random rnd = new Random();
        static void Test3()
        {
            Task[] tasks = new Task[5];
            Console.WriteLine("Create tasks");
            for (int i = 0; i < tasks.Length; i++)
            {
                tasks[i] = new Task(() => {
                    int m = 1, n = rnd.Next(5, 15);
                    for (int j = 1; j < n; j++)
                    {
                        m *= j;
                        System.Threading.Thread.Sleep(20);
                    }
                    Console.WriteLine($"Task = {Task.CurrentId}, N = {n}, Result = {m}");
                });
            }
            Console.WriteLine("Run tasks");
            foreach (Task task in tasks)
            {
                task.Start();
            }

            Console.WriteLine("Waiting all task finish");
            Task.WaitAll(tasks);
        }

        static int[] numbers = new int[20];
        static void Test4()
        {
            Task task1 = new Task(() => {
                Console.WriteLine($"Task {Task.CurrentId}, Fill numbers");
                System.Threading.Thread.Sleep(500);
                for (int i = 0; i < numbers.Length; i++)
                {
                    numbers[i] = rnd.Next(10, 100);
                }
            });

            Task task2 = task1.ContinueWith(t =>
            {
                Console.WriteLine($"Task {t.Id} finish, Start task {Task.CurrentId}");
                int max = numbers[0];
                for (int i = 1; i < numbers.Length; i++)
                {
                    if (max < numbers[i])
                        max = numbers[i];
                }

                Console.WriteLine($"Max Result = {max}");
            });

            Task task3 = task2.ContinueWith(_ =>
            {
                Console.WriteLine($"Task {Task.CurrentId}, Calculate summ");
                int s = 0;
                for (int i = 0; i < numbers.Length; i++)
                {
                    s += numbers[i];
                }

                Console.WriteLine($"Summ Result = {s}");
            });

            task1.Start();

            Console.WriteLine("Wait tasks...");
            task3.Wait();
        }

        static void Test5()
        {
            Task task = new Task(()=> {
                Console.WriteLine("Parent Task");

                Task childTask = new Task(PrintTime, TaskCreationOptions.AttachedToParent);
                childTask.Start();
                Console.WriteLine("End Parent task");
            });

            task.Start();
            Console.WriteLine($"Task Status = {task.Status}");
            System.Threading.Thread.Sleep(100);
            Console.WriteLine($"Task Status = {task.Status}");
            //while (!task.IsCompleted)
            //{
            //    Console.Write(".");
            //    System.Threading.Thread.Sleep(100);
            //}
            bool working = true;
            while (working)
            {
                working = !Task.WaitAll(new Task[] { task }, 10);
                Console.Write(".");
                System.Threading.Thread.Sleep(100);
            }

            Console.WriteLine($"Task Status = {task.Status}");
        }

        static void Test6()
        {
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            CancellationToken token = cancellationTokenSource.Token;

            Task task = Task.Factory.StartNew(PrintTime2, token, token);

            Console.WriteLine("\nPress ENTER to cancel...");
            Console.ReadLine();
            cancellationTokenSource.Cancel();
            Console.WriteLine("Task canceled!");
        }
        static void PrintTime2(object state)
        {
            CancellationToken token;
            try
            {
                token = (CancellationToken)state;
            }
            catch (Exception)
            {
                return;
            }

            while (true)
            {
                if (token.IsCancellationRequested)
                {
                    Console.WriteLine("IsCancellationRequested");
                    break;
                }
                Console.WriteLine($"Task {Task.CurrentId} {DateTime.Now.ToLongTimeString()}");
                Thread.Sleep(1000);
            }
        }
    }
}
