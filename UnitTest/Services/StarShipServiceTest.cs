using Abstractions.Models;
using Abstractions.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Services.Models;
using Services.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UnitTest.Services
{
    [TestClass]
    public class StarShipServiceTest
    {
        private Mock<IAPICallerService> _apiServiceMock;
        private IStarShipService _starshipService;

        [TestInitialize]
        public void TestSetup()
        {
            _apiServiceMock = new Mock<IAPICallerService>().SetupAllProperties();

            _starshipService = new StarShipService(_apiServiceMock.Object);
        }

        [TestMethod]
        public void GetAllShipsInfoList_ShouldCallApiServiceWithDefaultUrl_WhenMakingFirstCall()
        {
            //arrange
            _apiServiceMock.Setup(x => x.CallAPI<ShipDetailsPageModel>(It.Is<string>(s => s == "https://swapi.co/api/starships")))
                .Returns(Task.FromResult(new ShipDetailsPageModel()
                {
                    Results = new List<ShipDetailsModel>()
                }));

            _starshipService = new StarShipService(_apiServiceMock.Object);

            //act
            _starshipService.GetAllShipsInfoList().Wait();

            //assert
            _apiServiceMock.Verify(x => x.CallAPI<ShipDetailsPageModel>(It.Is<string>(s => s == "https://swapi.co/api/starships")));
        }

        [TestMethod]
        public void GetAllShipsInfoList_ShouldCallApiService_WhenThereIsNextPageLink()
        {
            //arrange
            _apiServiceMock.Setup(x => x.CallAPI<ShipDetailsPageModel>(It.Is<string>(s => s == "https://swapi.co/api/starships")))
                .Returns(Task.FromResult(new ShipDetailsPageModel()
                {
                    Next = "nextUrl",
                    Results = new List<ShipDetailsModel>()
                }));

            _apiServiceMock.Setup(x => x.CallAPI<ShipDetailsPageModel>(It.Is<string>(s => s == "nextUrl")))
                .Returns(Task.FromResult(new ShipDetailsPageModel()
                {
                    Results = new List<ShipDetailsModel>()
                }));

            _starshipService = new StarShipService(_apiServiceMock.Object);

            //act
            _starshipService.GetAllShipsInfoList().Wait();

            //assert
            _apiServiceMock.Verify(x => x.CallAPI<ShipDetailsPageModel>(It.Is<string>(s => s == "nextUrl")));
        }

        [TestMethod]
        public void GetAllShipsInfoList_ShouldReturnListOfModel_WhenMakingFirstCall()
        {
            //arrange
            _apiServiceMock.Setup(x => x.CallAPI<ShipDetailsPageModel>(It.Is<string>(s => s == "https://swapi.co/api/starships")))
                .Returns(Task.FromResult(new ShipDetailsPageModel()
                {
                    Results = new List<ShipDetailsModel>()
                    {
                        new ShipDetailsModel()
                        {
                            Name = "Test1"
                        }
                    }
                }));

            _starshipService = new StarShipService(_apiServiceMock.Object);

            //act
            var result = _starshipService.GetAllShipsInfoList();
            result.Wait();

            //assert
            Assert.IsTrue(result.Result.Count() == 1);
            Assert.IsTrue(result.Result.Any(x => x.Name == "Test1"));
        }

        [TestMethod]
        public void GetAllShipsInfoList_ShouldReturnAppendedListOfModel_WhenMakingMultipleCallse()
        {
            //arrange
            _apiServiceMock.Setup(x => x.CallAPI<ShipDetailsPageModel>(It.Is<string>(s => s == "https://swapi.co/api/starships")))
                .Returns(Task.FromResult(new ShipDetailsPageModel()
                {
                    Next = "nextUrl",
                    Results = new List<ShipDetailsModel>()
                    {
                        new ShipDetailsModel()
                        {
                            Name = "Test1"
                        }
                    }
                }));

            _apiServiceMock.Setup(x => x.CallAPI<ShipDetailsPageModel>(It.Is<string>(s => s == "nextUrl")))
                .Returns(Task.FromResult(new ShipDetailsPageModel()
                {
                    Results = new List<ShipDetailsModel>()
                    {
                        new ShipDetailsModel()
                        {
                            Name = "Test2"
                        }
                    }
                }));

            _starshipService = new StarShipService(_apiServiceMock.Object);

            //act
            var result = _starshipService.GetAllShipsInfoList();
            result.Wait();

            //assert
            Assert.IsTrue(result.Result.Count() == 2);
            Assert.IsTrue(result.Result.Any(x => x.Name == "Test1"));
            Assert.IsTrue(result.Result.Any(x => x.Name == "Test2"));
        }

    }
}
