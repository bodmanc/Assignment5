using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSCustomerQueue
{
    /// <summary>
    /// The master controller
    /// </summary>
    internal class Controller
    {
        /// <summary>
        /// Random number generator
        /// </summary>
        private Random random;

        /// <summary>
        /// Counter to handle customer id
        /// </summary>
        private int customerId;

        /// <summary>
        /// Constructor
        /// </summary>
        public Controller()
        {
            CustomerQueue = new BlockingCollection<Thread>();
            this.random = new Random();
            this.customerId = 0;
            CustomerLogger.CustomersServed = 0;
            CustomerLogger.TotalWaitTime = 0.0;
        }

        /// <summary>
        /// Queue of customers
        /// </summary>
        public BlockingCollection<Thread> CustomerQueue { get; private set; }

        /// <summary>
        /// Starts creaing customers
        /// </summary>
        private void StartCustomerCreation()
        {
            while (true)
            {
                int arrivalTimeMilliseconds = (CommonParameters.MeanInterArrivalTime * CommonParameters.MillisecondsMultiplier) / CommonParameters.ConversionFactor;
                int arrivalDelta = (CommonParameters.DeltaPercentage * arrivalTimeMilliseconds) / 100;

                arrivalTimeMilliseconds = this.random.Next(arrivalTimeMilliseconds - arrivalDelta, arrivalTimeMilliseconds + arrivalDelta);

                int serviceTimeDelta = (CommonParameters.MeanServiceTime * CommonParameters.DeltaPercentage) / 100;
                int serviceTime = this.random.Next(CommonParameters.MeanServiceTime - serviceTimeDelta, CommonParameters.MeanServiceTime + serviceTimeDelta);

                int currentTime = Clock.GetTimestamp();
                int remainingTime = CommonParameters.SimulationLength - CommonParameters.ConversionFactor * currentTime;
                if (remainingTime >= serviceTime + ((arrivalTimeMilliseconds * CommonParameters.ConversionFactor) / CommonParameters.MillisecondsMultiplier))
                {
                    Thread.Sleep(TimeSpan.FromMilliseconds(arrivalTimeMilliseconds));
                    ++this.customerId;
                    CustomerProcess customerProcess = new CustomerProcess(this.customerId, CommonParameters.ConversionFactor * Clock.GetTimestamp(), serviceTime);
                    CustomerQueue.Add(new Thread(customerProcess.Execute));
                }
                else
                {
                    // Do not allow a customer if it is close to end time
                    break;
                }
            }
        }

        /// <summary>
        /// Start the simulator
        /// </summary>
        public void Start()
        {
            CommonParameters.SimulationStartTime = DateTime.Now;

            // launch the thread which keps spawning new customers
            new Thread(StartCustomerCreation).Start();

            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            TimeSpan stopTime = TimeSpan.FromMilliseconds((CommonParameters.MillisecondsMultiplier * CommonParameters.SimulationLength) / CommonParameters.ConversionFactor);

            while (stopWatch.Elapsed < stopTime)
            {
                Thread? currentCustomer = null;
                if (CustomerQueue.TryTake(out currentCustomer))
                {
                    // take a customer and enqueue it
                    currentCustomer.Start();
                }
            }

            // Wait till ALL customers created till now have been served
            while (CustomerLogger.CustomersServed < customerId)
            {
                Thread.Sleep(100);
            }

            stopWatch.Stop();

            Console.WriteLine();
            Console.WriteLine("Sometimes, this simulation may run for slightly longer than expected Length.");
            Console.WriteLine("Simulation terminated after " + CustomerLogger.CustomersServed + " customers served.");

            double averageWaitTime = CustomerLogger.TotalWaitTime / (double)CustomerLogger.CustomersServed;
            Console.WriteLine("Average waiting time = " + Math.Round((Double)averageWaitTime, 2).ToString());
        }
    }
}
