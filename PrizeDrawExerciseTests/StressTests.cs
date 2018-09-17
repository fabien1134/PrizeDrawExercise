using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PrizeDraw.Entities;

namespace PrizeDrawExerciseTests
{
    [TestClass]
    public class StressTests
    {
        [TestMethod]
        public void StressTest()
        {
            bool passed = false;
            try
            {
                //Arrange
                string[] processedInput = InputGenerator.GenerateTestInput(1000, 1000, "1");

                //Act
                OrderInputParser orderInputParser = new OrderInputParser();
                TotalPrizeMoneyCalculator totalPrizeMoneyCalculator = new TotalPrizeMoneyCalculator(orderInputParser.ParseOrders(processedInput));
                int totalGivenPrizeMoeny = totalPrizeMoneyCalculator.CalculateTotal();

                passed = true;
            }
            catch (Exception ex)
            {
            }

            //Assert
            Assert.AreEqual(true, passed);
        }
    }
}
