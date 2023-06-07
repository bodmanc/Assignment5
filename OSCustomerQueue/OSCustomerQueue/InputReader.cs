using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSCustomerQueue
{
    /// <summary>
    /// Class to read a user input
    /// </summary>
    internal static class InputReader
    {
        /// <summary>
        /// Prompts the user to enter an input and keeps looping till user has entered a valid value
        /// </summary>
        /// <param name="inputMessage">Input message to be displayed to the user</param>
        /// <returns>Integer data entered by user</returns>
        internal static int ReadInput(String inputMessage)
        {
            bool validData = false;
            String input;
            int data = -1;
            while (!validData)
            {
                Console.Write(inputMessage);
                try
                {
                    input = Console.ReadLine();
                    data = Int32.Parse(input);
                    if (data < 1)
                    {
                        validData = false;
                    }
                    else
                    {
                        validData = true;
                    }
                }
                catch (Exception)
                {
                    validData = false;
                }

                if(!validData)
                {
                    Console.WriteLine("Invalid input ! Please try again.");
                }
            }

            return data;
        }
    }
}
