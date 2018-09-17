using PrizeDraw.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PrizeDraw.Entities
{
    //This class will be responsible for calculating the total prize money handed out
    public class TotalPrizeMoneyCalculator : IPrizeMoneyCalculator
    {
        private List<int>[] m_parsedOrders = default;

        public TotalPrizeMoneyCalculator(List<int>[] parsedOrders)
        {
            if (parsedOrders.Length == 0)
                throw new Exception("No parsed Orders Detected - Ensure valid orders have been provided");
            m_parsedOrders = parsedOrders;
        }

        public int CalculateTotal()
        {   //Keep track of the total amount of prize money handed out
            int totalGivenPrize = 0;
            //Will store the collection of customers orders that could be nominated for the prize draw
            List<int> prizeDrawOrderCollection = new List<int>();

            //Process all order line details
            for (int i = 1; i < m_parsedOrders.Length; i++)
            {
                //Continuously add orders to the prize draw order collection 
                List<int> parsedOrderDetailLine = m_parsedOrders[i];
                prizeDrawOrderCollection.AddRange(parsedOrderDetailLine);


                if (prizeDrawOrderCollection.Count() > 1)
                {   //Ensure order items are sorted in order to find and remove the smallest and largest numbers easier
                    prizeDrawOrderCollection.Sort();
                    //Perform calculation to calculate the order prize for the day
                    totalGivenPrize += prizeDrawOrderCollection.Max() - prizeDrawOrderCollection.Min();
                    //Remove the largest and smallest orders
                    prizeDrawOrderCollection.RemoveAt(prizeDrawOrderCollection.Count() - 1);
                    prizeDrawOrderCollection.RemoveAt(0);
                }
                else
                {   //The order item can be added to the total prize
                    totalGivenPrize += prizeDrawOrderCollection.First();
                }
            }

            return totalGivenPrize;
        }
    }
}
