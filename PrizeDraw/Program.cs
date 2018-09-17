using PrizeDraw.Entities;
using PrizeDraw.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PrizeDraw
{
    class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                //Collect Input
                string input = await Console.In.ReadToEndAsync();
                //Split input into lines
                string[] processedInput = input.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
                //Validate and parse command line arguments into a data structure containing orders
                IOrderInputParser orderInputParser = new OrderInputParser();
                List<int>[] parsedOrders = orderInputParser.ParseOrders(ordersInput: processedInput);
                //Calculate the sum of prize money passed out
                IPrizeMoneyCalculator prizeMoneyCalculator = new TotalPrizeMoneyCalculator(parsedOrders: parsedOrders);
                int totalGiveOutPrizeMoney = prizeMoneyCalculator.CalculateTotal();
                //Display Prize Money
                Console.WriteLine(totalGiveOutPrizeMoney);
            }
            catch (Exception ex)
            {   //Display error message
                if (ex.InnerException != default)
                {
                    Console.WriteLine(ex.InnerException.Message);
                }
                else
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
