using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VenCenter
{
   public static class SharedData
    {
        public static ConcurrentDictionary<IntPtr, Process> _processRunning =
                    new ConcurrentDictionary<IntPtr, Process>();
    }
}
