using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncAwait
{
    /// <summary>
    /// Simulates the action of a GUI event loop
    /// </summary>
    public abstract class EventLoop
    {
        public static void Main(string[] args)
        {
            new Version3().Loop();
        }

        /// <summary>
        /// Keeps track of elapsed time for display purposes
        /// </summary>
        private Stopwatch watch;

        /// <summary>
        /// List of events the event loop is supposed to execute
        /// </summary>
        private List<Event> eventList;

        /// <summary>
        /// Initializes the simulation
        /// </summary>
        public EventLoop()
        {
            watch = new Stopwatch();
            eventList = new List<Event>
            {
                new Event(1000, () => Report("One Second")),
                new Event(1500, () => G()),
                new Event(2000, () => Report("Two Seconds")),
                new Event(3000, () => Report("Three Seconds")),
                new Event(4000, null)
            };
        }

        /// <summary>
        /// A method that can be called by an event in the simulation
        /// </summary>
        protected abstract void G();

        /// <summary>
        /// Displays the string prefixed with the elapsed time
        /// </summary>
        protected void Report(String msg)
        {
            Console.WriteLine(watch.ElapsedMilliseconds + " " + msg);
        }

        /// <summary>
        /// The event loop simulator
        /// </summary>
        public void Loop()
        {
            watch.Start();

            while (true)
            {
                int i = 0;
                while (i < eventList.Count)
                {
                    Event e = eventList[i];
                    if (watch.ElapsedMilliseconds >= e.Time)
                    {
                        if (e.Action == null)
                        {
                            Report("Quit");
                            return;
                        }
                        else
                        {
                            e.Action();
                        }
                        eventList.RemoveAt(i);
                    }
                    else
                    {
                        i++;
                    }
                }

                Thread.Sleep(1);
            }
        }
    }
}
