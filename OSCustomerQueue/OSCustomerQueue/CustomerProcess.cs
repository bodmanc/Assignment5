using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSCustomerQueue
{
    /// <summary>
    /// Class to represent a customer process
    /// </summary>
    internal class CustomerProcess
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">Id of this customer</param>
        /// <param name="arrivalTime">Time at which this customer arrives</param>
        /// <param name="serviceTime">Time needed to service this customer</param>
        public CustomerProcess(int id, int arrivalTime, int serviceTime)
        {
            this.ID = id;
            this.ArrivalTime = arrivalTime;
            this.ServiceTime = serviceTime;
            CustomerLogger.LogArrival(id, ArrivalTime);
        }

        /// <summary>
        /// Customer ID
        /// </summary>
        public int ID { get; private set; }

        /// <summary>
        /// Time at which this customer arrives
        /// </summary>
        public int ArrivalTime { get; private set; }

        /// <summary>
        /// Time needed to service this customer
        /// </summary>
        public int ServiceTime { get; private set; }

        /// <summary>
        /// This customer's request gets serviced
        /// </summary>
        public void Execute()
        {
            CommonParameters.TellerSemaphore.WaitOne();
            int currentTime = CommonParameters.ConversionFactor * Clock.GetTimestamp();
            CustomerLogger.TotalWaitTime += (currentTime - ArrivalTime);
            CustomerLogger.LogStartServing(ID, currentTime);
            TimeSpan duration = TimeSpan.FromMilliseconds((ServiceTime * CommonParameters.MillisecondsMultiplier) / CommonParameters.ConversionFactor);
            Thread.Sleep(duration);
            CustomerLogger.LogLeaving(ID, CommonParameters.ConversionFactor * Clock.GetTimestamp());
            CommonParameters.TellerSemaphore.Release();
        }
    }
}
