using Abstractions.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Services.Services;
using System;
using System.Collections.Generic;
using System.IO;

namespace UnitTest.Services
{
    [TestClass]
    public class ConsolePrintServiceTest
    {
        private ConsolePrintService _printService;

        [TestInitialize]
        public void TestSetup()
        {
            _printService = new ConsolePrintService();
        }


        [TestMethod]
        public void PrintService_ShouldPrintOnlyHeader_WhenGivenEmptyShipList()
        {
            //arrange
            var shipList = new List<IShipDetailsModel>();
            long distance = 123;
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            //act
            _printService.PrintNumberOfJumpsForShips(shipList, distance);            

            //assert
            Assert.AreEqual("Number of jumps to travel 123 MGLT:", stringWriter.ToString().Trim());
        }

        [TestMethod]
        public void PrintService_ShouldPrintCorrectShipNameAndJumpsNumber_WhenGivenNonEMptyShipsList()
        {
            //arrange
            var shipList = new List<IShipDetailsModel>();

            var shipDetailMock = new Mock<IShipDetailsModel>();
            shipDetailMock.Setup(x => x.Name).Returns("TestShip");
            shipDetailMock.Setup(x => x.NumberOfJumpsForDistance(It.IsAny<long>())).Returns("5");
            shipList.Add(shipDetailMock.Object);

            long distance = 123;

            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            //act
            _printService.PrintNumberOfJumpsForShips(shipList, distance);

            //assert
            Assert.IsTrue(stringWriter.ToString().Contains("TestShip \nJumps needed: 5"));
        }
    }
}
