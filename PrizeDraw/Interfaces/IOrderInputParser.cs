using System.Collections.Generic;

namespace PrizeDraw.Interfaces
{   //Provides the interface for an instance that will parse ASCII characters into an int data structure
    public interface IOrderInputParser
    {
        //Indicates to the calling method if the parsed argument is valid
        List<int>[] ParseOrders(string[] ordersInput);
    }
}
