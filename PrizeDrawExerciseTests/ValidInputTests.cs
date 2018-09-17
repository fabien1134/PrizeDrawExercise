using System;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PrizeDraw.Entities;

namespace PrizeDrawExerciseTests
{
    [TestClass]
    public class ValidInputTests
    {
        [TestMethod]
        public void ExpectedOutputTest()
        {
            //Arrange
            string[] processedInput = InputGenerator.GenerateTestInput(new string[] { "5" ,
                "3 1 2 3" ,
                "2 1 1" ,
                "4 10 5 5 1",
                "0" ,
                "1 2" });

            //Act
            OrderInputParser orderInputParser = new OrderInputParser();

            TotalPrizeMoneyCalculator totalPrizeMoneyCalculator = new TotalPrizeMoneyCalculator(orderInputParser.ParseOrders(processedInput));
            //Assert
            Assert.AreEqual(19, totalPrizeMoneyCalculator.CalculateTotal());
        }
    }
}
