using Abstractions.Models;
using Abstractions.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Services.Services;

namespace UnitTest.Services
{
    [TestClass]
    public class InputValidationServiceTest
    {
        private IInputValidationService _validationService;

        [TestInitialize]
        public void TestSetup()
        {
            var printServieceMock = new Mock<IConsolePrintService>().SetupAllProperties();

            _validationService = new InputValidationService(printServieceMock.Object);
        }

        [TestMethod]
        public void ValidateInput_ShouldPass_WhenLineIsNumerical()
        {
            //assert
            long distance;

            //act
            var result = _validationService.ValidateDistance("123", out distance);

            //assert
            Assert.IsTrue(result);
            Assert.AreEqual(123, distance);
        }

        [TestMethod]
        public void ValidateInput_ShouldFail_WhenLineIsNegative()
        {
            //assert

            //act
            var result = _validationService.ValidateDistance("-123", out _);

            //assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ValidateInput_ShouldFail_WhenLineIsEmpty()
        {
            //assert

            //act
            var result = _validationService.ValidateDistance(string.Empty, out _);

            //assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ValidateInput_ShouldFail_WhenLineIsNotNumerical()
        {
            //assert

            //act
            var result = _validationService.ValidateDistance("qwert", out _);

            //assert
            Assert.IsFalse(result);
        }
    }
}
