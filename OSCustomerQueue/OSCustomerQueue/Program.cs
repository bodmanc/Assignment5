using OSCustomerQueue;

CommonParameters.MeanInterArrivalTime = InputReader.ReadInput("Mean inter-arrival time : ");
CommonParameters.MeanServiceTime = InputReader.ReadInput("Mean service time : ");
int tellerCount = InputReader.ReadInput("Number of tellers : ");
CommonParameters.TellerSemaphore = new Semaphore(tellerCount, tellerCount);
CommonParameters.SimulationLength = InputReader.ReadInput("Length of simulation : ");

Controller controller = new Controller();
controller.Start();
