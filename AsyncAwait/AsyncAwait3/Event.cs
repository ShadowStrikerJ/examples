using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncAwait
{
    public class Event
    {
        public Event (int time, Action action)
        {
            Time = time;
            Action = action;
        }
        public int Time { get; private set; }
        public Action Action { get; private set; }
    }
}
