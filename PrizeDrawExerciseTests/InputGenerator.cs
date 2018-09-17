using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrizeDrawExerciseTests
{
    //This class will be responsible for generating test input
    public class InputGenerator
    {
        public static string[] GenerateTestInput(string[] inputArray)
        {
            StringBuilder input = new StringBuilder();
            for (int i = 0; i < inputArray.Length; i++)
            {
                input.AppendLine(inputArray[i]);
            }
            return input.ToString().Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
        }


        public static string[] GenerateTestInput(int orderlineRow, int dayOrderColoumn, string orderAmount)
        {
            StringBuilder input = new StringBuilder();

            input.AppendLine(orderlineRow.ToString());
            for (int i = 0; i < orderlineRow; i++)
            {
                input.Append(dayOrderColoumn + " ");
                for (int f = 0; f < dayOrderColoumn; f++)
                {
                    input.Append(orderAmount + " ");
                }

                input.AppendLine();
            }

            return input.ToString().Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
        }

    }
}
