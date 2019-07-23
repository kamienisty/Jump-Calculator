using Abstractions.Models;
using Abstractions.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Services.Services;

namespace UnitTest.Services
{
    [TestClass]
    public class HoursSuppliesLastServiceTest
    {
        private Mock<IShipDetailsModel> _shipDetailsMock;
        private IHoursSuppliesLastService _suppliesService;

        [TestInitialize]
        public void TestSetup()
        {
            _shipDetailsMock = new Mock<IShipDetailsModel>();
            _shipDetailsMock.Setup(x => x.Supplies).Returns("1 day");
            _shipDetailsMock.SetupAllProperties();

            _suppliesService = new HoursSuppliesLastService();
        }

        [TestMethod]
        public void FillSuppliesHoursForShip_ShouldSetHoursSuppliesLastForToZero_WhenSuppliesAreSingleWord()
        {
            //arrange
            _shipDetailsMock.Setup(x => x.Supplies).Returns("qwert");
            var shipDetails = _shipDetailsMock.Object;

            //act
            _suppliesService.FillSuppliesHoursForShip(shipDetails);

            //assert
            Assert.AreEqual(0, shipDetails.HoursSuppliesLastFor);
        }

        [TestMethod]
        public void FillSuppliesHoursForShip_ShouldSetHoursSuppliesLastForToZero_WhenSuppliesHaveNotSupportedTimeUnit()
        {
            //arrange
            _shipDetailsMock.Setup(x => x.Supplies).Returns("3 dubdubs");
            var shipDetails = _shipDetailsMock.Object;

            //act
            _suppliesService.FillSuppliesHoursForShip(shipDetails);

            //assert
            Assert.AreEqual(0, shipDetails.HoursSuppliesLastFor);
        }

        [TestMethod]
        public void FillSuppliesHoursForShip_ShouldSetHoursSuppliesLastForToZero_WhenSuppliesHaveMoreThanTwoSpaces()
        {
            //arrange
            _shipDetailsMock.Setup(x => x.Supplies).Returns("1 day day");
            var shipDetails = _shipDetailsMock.Object;

            //act
            _suppliesService.FillSuppliesHoursForShip(shipDetails);

            //assert
            Assert.AreEqual(0, shipDetails.HoursSuppliesLastFor);
        }

        [TestMethod]
        public void FillSuppliesHoursForShip_ShouldSetHoursSuppliesLastForToCorrectValue_WhenSuppliesAreIneDay()
        {
            //arrange
            _shipDetailsMock.Setup(x => x.Supplies).Returns("1 day");
            var shipDetails = _shipDetailsMock.Object;

            //act
            _suppliesService.FillSuppliesHoursForShip(shipDetails);

            //assert
            Assert.AreEqual(24, shipDetails.HoursSuppliesLastFor);
        }

        [TestMethod]
        public void FillSuppliesHoursForShip_ShouldSetHoursSuppliesLastForToCorrectValue_WhenSuppliesAreInDays()
        {
            //arrange
            _shipDetailsMock.Setup(x => x.Supplies).Returns("2 days");
            var shipDetails = _shipDetailsMock.Object;
            //act
            _suppliesService.FillSuppliesHoursForShip(shipDetails);

            //assert
            Assert.AreEqual(48, shipDetails.HoursSuppliesLastFor);
        }

        [TestMethod]
        public void FillSuppliesHoursForShip_ShouldSetHoursSuppliesLastForToCorrectValue_WhenSuppliesAreInWeek()
        {
            //arrange
            _shipDetailsMock.Setup(x => x.Supplies).Returns("1 week");
            var shipDetails = _shipDetailsMock.Object;

            //act
            _suppliesService.FillSuppliesHoursForShip(shipDetails);

            //assert
            Assert.AreEqual(168, shipDetails.HoursSuppliesLastFor);
        }

        [TestMethod]
        public void FillSuppliesHoursForShip_ShouldSetHoursSuppliesLastForToCorrectValue_WhenSuppliesAreInWeeks()
        {
            //arrange
            _shipDetailsMock.Setup(x => x.Supplies).Returns("2 weeks");
            var shipDetails = _shipDetailsMock.Object;

            //act
            _suppliesService.FillSuppliesHoursForShip(shipDetails);

            //assert
            Assert.AreEqual(336, shipDetails.HoursSuppliesLastFor);
        }

        [TestMethod]
        public void FillSuppliesHoursForShip_ShouldSetHoursSuppliesLastForToCorrectValue_WhenSuppliesAreInMonth()
        {
            //arrange
            _shipDetailsMock.Setup(x => x.Supplies).Returns("1 month");
            var shipDetails = _shipDetailsMock.Object;

            //act
            _suppliesService.FillSuppliesHoursForShip(shipDetails);

            //assert
            Assert.AreEqual(720, shipDetails.HoursSuppliesLastFor);
        }

        [TestMethod]
        public void FillSuppliesHoursForShip_ShouldSetHoursSuppliesLastForToCorrectValue_WhenSuppliesAreInMonths()
        {
            //arrange
            _shipDetailsMock.Setup(x => x.Supplies).Returns("2 months");
            var shipDetails = _shipDetailsMock.Object;

            //act
            _suppliesService.FillSuppliesHoursForShip(shipDetails);

            //assert
            Assert.AreEqual(1440, shipDetails.HoursSuppliesLastFor);
        }

        [TestMethod]
        public void FillSuppliesHoursForShip_ShouldSetHoursSuppliesLastForToCorrectValue_WhenSuppliesAreInYear()
        {
            //arrange
            _shipDetailsMock.Setup(x => x.Supplies).Returns("1 year");
            var shipDetails = _shipDetailsMock.Object;

            //act
            _suppliesService.FillSuppliesHoursForShip(shipDetails);

            //assert
            Assert.AreEqual(8640, shipDetails.HoursSuppliesLastFor);
        }

        [TestMethod]
        public void FillSuppliesHoursForShip_ShouldSetHoursSuppliesLastForToCorrectValue_WhenSuppliesAreInYears()
        {
            //arrange
            _shipDetailsMock.Setup(x => x.Supplies).Returns("2 years");
            var shipDetails = _shipDetailsMock.Object;

            //act
            _suppliesService.FillSuppliesHoursForShip(shipDetails);

            //assert
            Assert.AreEqual(17280, shipDetails.HoursSuppliesLastFor);
        }
    }
}
