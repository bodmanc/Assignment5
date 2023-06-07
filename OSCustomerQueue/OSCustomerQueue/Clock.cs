using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSCustomerQueue
{
    /// <summary>
    /// Timer related utilities
    /// </summary>
    internal static class Clock
    {
        public static int GetTimestamp()
        {
            DateTime now = DateTime.Now;
            TimeSpan span = now.Subtract(CommonParameters.SimulationStartTime);
            return (int)Math.Round(span.TotalMilliseconds / 1000.0);
        }
    }
}
