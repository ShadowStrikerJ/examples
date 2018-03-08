using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncAwait
{
    /// <summary>
    /// Provides an implementation of the abstract method G
    /// </summary>
    public class Version3 : EventLoop
    {
        /// <summary>
        /// This simulates a method that calls some other method to obtain a task.  It must wait
        /// for the task to complete before returning itself.  For example, it might be calling
        /// PostAsync to obtain the task.
        /// </summary>
        protected override async void G()
        {
            Report("  G: Starting");
            Report("  G: Calling H");
            Task<int> task = H();
            Report("  G: H has returned a task");
            int result = await task;
            Report("  G: Return value of task is " + result);
            Report("  G: Completing");
        }

        /// <summary>
        /// This simulates a method that creates and returns a task that takes a long time
        /// to compute something.  For example, it might be PostAsync.
        protected Task<int> H()
        {
            Report("    H: Entering");
            Task<int> task =
                Task.Run(() =>
                {
                    Report("      Task: Starting");
                    Thread.Sleep(2000);
                    Report("      Task: Completing");
                    return 5;
                });
            Report("    H: Returning a task");
            return task;
        }
    }
}
