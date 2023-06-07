using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSCustomerQueue
{
    /// <summary>
    /// Common parameters for this simulation
    /// </summary>
    internal static class CommonParameters
    {
        /// <summary>
        /// Mean inter-arrival time between arrival of customers
        /// </summary>
        public static int MeanInterArrivalTime { get; set; }

        /// <summary>
        /// Mean time to service a customer
        /// </summary>
        public static int MeanServiceTime { get; set; }

        /// <summary>
        /// Sempahore to control number of customers being served concurrently
        /// </summary>
        public static Semaphore? TellerSemaphore { get; set; }

        /// <summary>
        /// Length of the total simulation
        /// </summary>
        public static int SimulationLength { get; set; }

        /// <summary>
        /// The time at which this simulation starts
        /// </summary>
        public static DateTime SimulationStartTime { get; set; }

        /// <summary>
        /// Conversion factor to map simulator seconds to real world seconds
        /// A factor of 10 means that 10 seconds of simulator maps to 1 second of real world clock time
        /// </summary>
        public const int ConversionFactor = 10;

        /// <summary>
        /// Multiplier to convert seconds to milliseconds
        /// </summary>
        public const int MillisecondsMultiplier = 1000;

        /// <summary>
        /// To maintain consistency of average expected times
        /// simulate a variation of +/-DeltaPercentage % on the mean values
        /// </summary>
        public const int DeltaPercentage = 30;
    }
}
