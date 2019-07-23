using System;
using System.Collections.Generic;
using System.Text;
using CommonResources;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Services.Models;

namespace UnitTest.Models
{
    [TestClass]
    public class ShipDetailsModelTest
    {
        

        [TestMethod]
        public void NumericRange_ShouldReturnParsedValue_WhenRangeIsStringWIthNumericValue()
        {
            //arrange
            var shipDetail = new ShipDetailsModel()
            {
                Speed = "500"
            };

            //act
            var result = shipDetail.NumericSpeed;

            //assert
            Assert.AreEqual(500, result);
        }

        [TestMethod]
        public void NumericRange_ShouldReturnZero_WhenRangeIsNotNumeric()
        {
            //arrange
            var shipDetail = new ShipDetailsModel()
            {
                Speed = "aaa"
            };

            //act
            var result = shipDetail.NumericSpeed;

            //assert
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void NumericRange_ShouldReturnZero_WhenRangeLargerThanIn32Size()
        {
            //arrange
            var shipDetail = new ShipDetailsModel()
            {
                Speed = $"{Int32.MaxValue}0"
            };

            //act
            var result = shipDetail.NumericSpeed;

            //assert
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void NumberOfJumpsForDistance_ShouldReturnCorrectResult_WhenRangeAnsSuppliesAreValid()
        {
            //arrange
            var shipDetail = new ShipDetailsModel()
            {
                Speed = "10",
                HoursSuppliesLastFor = 50
            };

            long distance = 1000;

            //act
            var result = shipDetail.NumberOfJumpsForDistance(distance);

            //assert
            Assert.AreEqual("2", result);
        }

        [TestMethod]
        public void NumberOfJumpsForDistance_ShouldReturnInteger_WhenRangeAnsSuppliesAreValid()
        {
            //arrange
            var shipDetail = new ShipDetailsModel()
            {
                Speed = "10",
                HoursSuppliesLastFor = 49
            };

            long distance = 1000;

            //act
            var result = shipDetail.NumberOfJumpsForDistance(distance);

            //assert
            Assert.AreEqual("2", result);
        }

        [TestMethod]
        public void NumberOfJumpsForDistance_ShouldReturnIntegerRoundedDown_WhenRangeAnsSuppliesAreValid()
        {
            //arrange
            var shipDetail = new ShipDetailsModel()
            {
                Speed = "10",
                HoursSuppliesLastFor = 51
            };

            long distance = 1000;

            //act
            var result = shipDetail.NumberOfJumpsForDistance(distance);

            //assert
            Assert.AreEqual("1", result);
        }

        [TestMethod]
        public void NumberOfJumpsForDistance_ShouldReturnMessage_WhenRangeIsZero()
        {
            //arrange
            var shipDetail = new ShipDetailsModel()
            {
                Speed = "0",
                HoursSuppliesLastFor = 49
            };

            long distance = 1000;

            //act
            var result = shipDetail.NumberOfJumpsForDistance(distance);

            //assert
            Assert.AreEqual(StringResources.COULDNT_CALCULATE, result);
        }

        [TestMethod]
        public void NumberOfJumpsForDistance_ShouldReturnMessage_WhenSuppliesAreZero()
        {
            //arrange
            var shipDetail = new ShipDetailsModel()
            {
                Speed = "10",
                HoursSuppliesLastFor = 0
            };

            long distance = 1000;

            //act
            var result = shipDetail.NumberOfJumpsForDistance(distance);

            //assert
            Assert.AreEqual(StringResources.COULDNT_CALCULATE, result);
        }

        [TestMethod]
        public void NumberOfJumpsForDistance_ShouldReturnMessage_WhenRangeAndSuppliesAreZero()
        {
            //arrange
            var shipDetail = new ShipDetailsModel()
            {
                Speed = "0",
                HoursSuppliesLastFor = 0
            };

            long distance = 1000;

            //act
            var result = shipDetail.NumberOfJumpsForDistance(distance);

            //assert
            Assert.AreEqual(StringResources.COULDNT_CALCULATE, result);
        }
    }
}
