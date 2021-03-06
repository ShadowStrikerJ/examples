﻿using System;
using System.Threading;
using System.Threading.Tasks;

namespace Synchronization
{
    public class Synch
    {
        /// <summary>
        /// Demonstrates synchronization of Tasks.
        /// </summary>
        public static void Main()
        {
            new Synch().Compute(100000000);
            Console.ReadLine();
        }

        /// <summary>
        /// This variable is shared by the two tasks created below.
        /// </summary>
        private int Total { get; set; }

        /// <summary>
        /// Creates two tasks.  One increments Total n times, the other
        /// decrements Total n times.  The value of Total is displayed
        /// every 500 msec.
        /// </summary>
        public void Compute(int n)
        {
            Task t1 = new Task(() => this.Set(n, 0));
            Task t2 = new Task(() => this.Set(n, 1));
            t1.Start();
            t2.Start();

            while (!t1.IsCompleted || !t2.IsCompleted)
            {
                Console.WriteLine(Total);
                Thread.Sleep(500);
            }

            Console.WriteLine("Finished: " + Total);
            Console.ReadLine();
        }

        /// <summary>
        /// Sets Total to value n times
        /// </summary>
        public void Set(int n, int value)
        {
            while (n > 0)
            {
                Total = value;
                n--;
            }
            Console.WriteLine("Set " + value + " done");
        }
    }
}
