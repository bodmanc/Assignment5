using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSCustomerQueue
{
    /// <summary>
    /// Class to log the events in the lifecycle of the customer
    /// </summary>
    internal static class CustomerLogger
    {
        /// <summary>
        /// Total number of customers served till now
        /// </summary>
        public static int CustomersServed { get; set; }

        /// <summary>
        /// Total wait time for all customers till now
        /// </summary>
        public static double TotalWaitTime { get; set; }

        /// <summary>
        /// Logs the arrival of customer
        /// </summary>
        /// <param name="id">Id of customer</param>
        /// <param name="arrivalTime">Arrival time of customer</param>
        public static void LogArrival(int id, int arrivalTime)
        {
           Console.WriteLine("At time " + FormattedTime(arrivalTime) + ", customer " + FormattedId(id) + " arrives in line.");
        }

        /// <summary>
        /// Logs the start of serving of a customer
        /// </summary>
        /// <param name="id">Id of customer</param>
        /// <param name="servingStartTime">Time at which this customer starts getting served</param>
        public static void LogStartServing(int id, int servingStartTime)
        {
            Console.WriteLine("At time " + FormattedTime(servingStartTime) + ", customer " + FormattedId(id) + " starts being served.");
        }

        /// <summary>
        /// Logs the leaving of a customer after being served
        /// </summary>
        /// <param name="id">Id of customer</param>
        /// <param name="servingEndTime">Time at which this customer leaves the queue after getting served</param>
        public static void LogLeaving(int id, int servingEndTime)
        {
            ++CustomersServed;
            Console.WriteLine("At time " + FormattedTime(servingEndTime) + ", customer " + FormattedId(id) + " leaves the food court.");
        }

        /// <summary>
        /// Formats the customer id
        /// </summary>
        /// <param name="id">Id to be formatted</param>
        /// <returns>Formatted ID</returns>
        private static String FormattedId(int id)
        {
            return String.Format("{0,3}", id);
        }

        /// <summary>
        /// Formats the timestamp
        /// </summary>
        /// <param name="time">Time to be formatted</param>
        /// <returns>Formatted timestamp</returns>
        private static String FormattedTime(int time)
        {
            return String.Format("{0,5}", time);
        }
    }
}
