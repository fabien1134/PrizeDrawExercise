using PrizeDraw.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PrizeDraw.Entities
{
    //This class will be responsible for parsing and validating orders lines
    public class OrderInputParser : IOrderInputParser
    {   //Store the total orders made during the campaign
        private int m_totalOrderAmount = 0;
        //This Method Will Parse the command line argument into a more appropriate data structure
        public List<int>[] ParseOrders(string[] orderInput)
        {   //Will be used to provide the order processing section a signal to stop
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

            //Ensure Input is present
            const string errorMessage = "No Input Detected";
            if (orderInput == null)
            {
                throw new Exception(errorMessage);
            }
            else
            {
                if (orderInput.Length == 0)
                    throw new Exception(errorMessage);
            }

            //Parse campaignDurationDays and ensure this value is valid
            int campaignDurationDays = ParsePositiveInt(unParsedIntCharacter: orderInput[0], minAllowedValue: 1);
            if (campaignDurationDays < 1 || campaignDurationDays > 5000)
                throw new Exception("Invalid Campaign Duration- Campaign Duration Must Be Greater Then 0 And Less Then 5001");

            //Ensure the campaign duration matches the detail duration line sum
            if (campaignDurationDays != orderInput.Length - 1)
                throw new Exception("Campaign Day Amout Does Not Match Order Detail Lines Sum");

            //Add one to the amount as the first index will not be used to store orders
            List<int>[] parsedOrders = new List<int>[campaignDurationDays + 1];

            try
            {   //Validate and parse all order detail lines into a valid data structure
                Parallel.For(1, orderInput.Length, new ParallelOptions() { CancellationToken = cancellationTokenSource.Token }, (index) =>
                   {
                       //Will validate, parse and store all the order amounts of a detail line
                       List<int> parsedOrderDetailLine = new List<int>();
                       //Parse and validate the day order amount
                       string[] orderDetailLine = orderInput[index].Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);

                       //The day order amount is the first value on the order line
                       int dayOrderAmount = ParsePositiveInt(orderDetailLine.First(), 0);

                       //Ensure parsing processes  is signaled to stop when error found
                       if (dayOrderAmount < 0 || dayOrderAmount > 100000)
                           StopOrderParsingProcess(cancellationTokenSource, "Invalid Day Amount Detected - Ensure Each Day Amount Is Valid");

                       //Ensure the day amount matches the amount of orders present on the order detail line
                       if (dayOrderAmount != orderDetailLine.Length - 1)
                           throw new Exception("Day Order Amounts Do Not Match Number Detected");

                       //Process and store orders amounts
                       for (int i = 1; i < orderDetailLine.Length; i++)
                       {
                           int orderAmount = ParsePositiveInt(orderDetailLine[i], 0);
                           //ensure order amount is valid
                           if (orderAmount < 0 || orderAmount > 1000000)
                               //Ensure all parsing task processes are signaled to stop
                               StopOrderParsingProcess(cancellationTokenSource, "An order amount cannot exceed 1000000");

                           //Keep track of the total orders made during the whole campaign in a thread safe way
                           Interlocked.Add(ref m_totalOrderAmount, orderAmount);

                           parsedOrderDetailLine.Add(orderAmount);
                       }

                       //Ensure the parsed order detail line is stored in the correct order
                       parsedOrders[index] = parsedOrderDetailLine;
                   });
            }
            catch (OperationCanceledException)
            {//We can ignore cancellation exceptions
            }

            //Ensure the total amount of orders does not surpass the permitted amount
            if (m_totalOrderAmount > 1000000)
                throw new Exception("Total order amount cannot exceed 1000000");

            return parsedOrders;
        }

        //This method will be responsible for parsing a string to an int
        private int ParsePositiveInt(string unParsedIntCharacter, int minAllowedValue = 0)
        {
            if (!int.TryParse(unParsedIntCharacter, out int parsedInt) || !(parsedInt >= minAllowedValue))
                throw new Exception("Issue During Parsing Invalid Character Detected - Check Input");
            return parsedInt;
        }

        //This method will be raised when an exception had occurred and the parallel process must be signaled stopped
        private void StopOrderParsingProcess(CancellationTokenSource cancellationTokenSource, string errorMessage)
        {
            if (!cancellationTokenSource.IsCancellationRequested)
                cancellationTokenSource.Cancel();

            throw new Exception(errorMessage);
        }
    }
}
