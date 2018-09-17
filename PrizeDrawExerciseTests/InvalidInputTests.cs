using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PrizeDraw.Entities;

namespace PrizeDrawExerciseTests
{
    [TestClass]
    public class InvalidInputTests
    {
        [TestMethod]
        public void NoInputTest()
        {
            //Arrange
            string[] processedInput = default;
            //Act
            try
            {
                //Act
                OrderInputParser orderInputParser = new OrderInputParser();
                TotalPrizeMoneyCalculator totalPrizeMoneyCalculator = new TotalPrizeMoneyCalculator(orderInputParser.ParseOrders(processedInput));
                totalPrizeMoneyCalculator.CalculateTotal();
            }
            catch (Exception ex)
            {
                Assert.AreEqual("No Input Detected", ex.Message);
            }
        }

        [TestMethod]
        public void InvalidCampaignDay()
        {
            //Arrange
            string[] processedInput = InputGenerator.GenerateTestInput(new string[] { "0" ,
                "3 1 2 3"});

            //Act
            try
            {
                //Act
                OrderInputParser orderInputParser = new OrderInputParser();
                TotalPrizeMoneyCalculator totalPrizeMoneyCalculator = new TotalPrizeMoneyCalculator(orderInputParser.ParseOrders(processedInput));
                totalPrizeMoneyCalculator.CalculateTotal();
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Issue During Parsing Invalid Character Detected - Check Input", ex.Message);
            }
        }

        [TestMethod]
        public void CampaignDayAmountOrderlineMisMatch1()
        {
            //Arrange
            string[] processedInput = InputGenerator.GenerateTestInput(new string[] { "1" ,
                "3 1 2 3",
                "3 8 2 2" });

            //Act
            try
            {
                //Act
                OrderInputParser orderInputParser = new OrderInputParser();
                TotalPrizeMoneyCalculator totalPrizeMoneyCalculator = new TotalPrizeMoneyCalculator(orderInputParser.ParseOrders(processedInput));
                totalPrizeMoneyCalculator.CalculateTotal();
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Campaign Day Amout Does Not Match Order Detail Lines Sum", ex.Message);
            }
        }

        [TestMethod]
        public void CampaignDayAmountOrderlineMisMatch2()
        {
            //Arrange
            string[] processedInput = InputGenerator.GenerateTestInput(new string[] { "2" ,
                "3 1 2 3" });

            //Act
            try
            {
                //Act
                OrderInputParser orderInputParser = new OrderInputParser();
                TotalPrizeMoneyCalculator totalPrizeMoneyCalculator = new TotalPrizeMoneyCalculator(orderInputParser.ParseOrders(processedInput));
                totalPrizeMoneyCalculator.CalculateTotal();
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Campaign Day Amout Does Not Match Order Detail Lines Sum", ex.Message);
            }
        }


        [TestMethod]
        public void NegativeDayOrderAmount()
        {
            //Arrange
            string[] processedInput = InputGenerator.GenerateTestInput(new string[] { "1" ,
                "-2 4  3" });

            //Act
            try
            {
                //Act
                OrderInputParser orderInputParser = new OrderInputParser();
                TotalPrizeMoneyCalculator totalPrizeMoneyCalculator = new TotalPrizeMoneyCalculator(orderInputParser.ParseOrders(processedInput));
                totalPrizeMoneyCalculator.CalculateTotal();
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Issue During Parsing Invalid Character Detected - Check Input", ex.InnerException.Message);
            }
        }

        [TestMethod]
        public void GreaterThenValidDayOrderAmount()
        {
            //Arrange
            string[] processedInput = InputGenerator.GenerateTestInput(new string[] { "1" ,
                "100001 4  3" });

            //Act
            try
            {
                //Act
                OrderInputParser orderInputParser = new OrderInputParser();
                TotalPrizeMoneyCalculator totalPrizeMoneyCalculator = new TotalPrizeMoneyCalculator(orderInputParser.ParseOrders(processedInput));
                totalPrizeMoneyCalculator.CalculateTotal();
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Invalid Day Amount Detected - Ensure Each Day Amount Is Valid", ex.InnerException.Message);
            }
        }

        [TestMethod]
        public void NegativeOrderAmount()
        {
            //Arrange
            string[] processedInput = InputGenerator.GenerateTestInput(new string[] { "1" ,
                "2 -4  3" });

            //Act
            try
            {
                //Act
                OrderInputParser orderInputParser = new OrderInputParser();
                TotalPrizeMoneyCalculator totalPrizeMoneyCalculator = new TotalPrizeMoneyCalculator(orderInputParser.ParseOrders(processedInput));
                totalPrizeMoneyCalculator.CalculateTotal();
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Issue During Parsing Invalid Character Detected - Check Input", ex.InnerException.Message);
            }
        }


        [TestMethod]
        public void GreaterThenValidOrderAmount()
        {
            //Arrange
            string[] processedInput = InputGenerator.GenerateTestInput(new string[] { "1" ,
                "2 1000001  3" });

            //Act
            try
            {
                //Act
                OrderInputParser orderInputParser = new OrderInputParser();
                TotalPrizeMoneyCalculator totalPrizeMoneyCalculator = new TotalPrizeMoneyCalculator(orderInputParser.ParseOrders(processedInput));
                totalPrizeMoneyCalculator.CalculateTotal();
            }
            catch (Exception ex)
            {
                Assert.AreEqual("An order amount cannot exceed 1000000", ex.InnerException.Message);
            }
        }


        [TestMethod]
        public void GreaterThenValidTotalOrderAmount()
        {
            //Arrange
            string[] processedInput = InputGenerator.GenerateTestInput(new string[] { "1" ,
                "10 100000 100000 100000 100000 100000 100000 100000 100000 100000 100001" });

            //Act
            try
            {
                //Act
                OrderInputParser orderInputParser = new OrderInputParser();
                TotalPrizeMoneyCalculator totalPrizeMoneyCalculator = new TotalPrizeMoneyCalculator(orderInputParser.ParseOrders(processedInput));
                totalPrizeMoneyCalculator.CalculateTotal();
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Total order amount cannot exceed 1000000", ex.Message);
            }
        }
    }
}
