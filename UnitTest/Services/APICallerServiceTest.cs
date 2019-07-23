using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Moq.Protected;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace UnitTest.Services
{
    [TestClass]
    public class APICallerServiceTest
    {
        private HttpClient _client;
        private Mock<HttpMessageHandler> _handlerMock;

        [TestInitialize]
        public void TestSetup()
        {
            _handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
        }

        private void SetupHandlerMockAndClient(HttpResponseMessage responseMessage)
        {
            _handlerMock.Protected()
                        .Setup<Task<HttpResponseMessage>>(
                              "SendAsync",
                              ItExpr.IsAny<HttpRequestMessage>(),
                              ItExpr.IsAny<CancellationToken>()
                           )
                        .ReturnsAsync(responseMessage)
                        .Verifiable();

            _client = new HttpClient(_handlerMock.Object);
        }

        [TestMethod]
        public void CallAPI_ShouldReturnParsedValue_WhenResponseStatusOk()
        {
            //arrange
            var responseMessage = new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent("[1, 2, 3]"),
            };

            SetupHandlerMockAndClient(responseMessage);
            var testService = new APICallerService(_client);

            //act
            var result = testService.CallAPI<int[]>("https://testUrl");
            result.Wait();
            //assert
            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Result.Length);
            Assert.IsTrue(result.Result[0] == 1 && result.Result[1] == 2 && result.Result[2] == 3);
        }

        [TestMethod]
        public void CallAPI_ShouldReturnThrowError_WhenRequestTimesout()
        {
            //arrange
            var responseMessage = new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.RequestTimeout
            };

            SetupHandlerMockAndClient(responseMessage);
            var testService = new APICallerService(_client);

            //act
            var ex = Assert.ThrowsException<AggregateException>(() => testService.CallAPI<int[]>("https://testUrl").Wait());

            //assert
            Assert.AreEqual(1, ex.InnerExceptions.Count);
            Assert.AreEqual(
                "One or more errors occurred. (Response status code does not indicate success: 408 (Request Timeout).)",
                ex.Message);
        }

    }
}
