using System;
using System.Threading;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            new Thread(() =>
            {
                for (int i = 0; i < 1000000; i++)
                {
                    Console.WriteLine(Thread.CurrentThread.ManagedThreadId + " " + i);

                }
            }).Start();

            new Thread(() =>
            {
                for (int i = 0; i < 1000000; i++)
                {
                    Console.WriteLine(Thread.CurrentThread.ManagedThreadId + " " + i);

                }
            }).Start(); 
            
            new Thread(() =>
            {
                for (int i = 0; i < 1000000; i++)
                {
                    Console.WriteLine(Thread.CurrentThread.ManagedThreadId + " " + i);

                }
            }).Start(); 
            
            new Thread(() =>
            {
                for (int i = 0; i < 1000000; i++)
                {
                    Console.WriteLine(Thread.CurrentThread.ManagedThreadId + " " + i);

                }
            }).Start();
            
            new Thread(() =>
            {
                for (int i = 0; i < 1000000; i++)
                {
                    Console.WriteLine(Thread.CurrentThread.ManagedThreadId + " " + i);

                }
            }).Start();
            Console.ReadKey();
        }
    }
}
