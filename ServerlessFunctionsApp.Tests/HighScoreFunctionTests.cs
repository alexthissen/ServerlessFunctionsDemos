using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http;
using System.Web.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moq;
using System.Net;
using System.Text;

namespace ServerlessFunctionsApp.Tests
{
    [TestClass]
    public class HighScoreFunctionTests
    {
        public readonly string playerName = "LX360";

        [TestMethod]
        public async Task GivenRequestHasInvalidScore_WhenRunIsCalled_BadRequestResponseShouldBeReturned()
        {
            // Arrange
            ILogger log = new Mock<ILogger>().Object;

            HttpConfiguration config = new HttpConfiguration();
            HttpRequestMessage request = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("http://localhost:7071/api/HighScores/player/"),
                Content = new StringContent("not1337", Encoding.UTF8, "application/json")
            };
            request.SetConfiguration(config);

            // Act
            var response = await HighScoreFunction.Run(request, playerName, log);

            // Assert
            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.AreEqual("\"Received invalid nickname and/or score!\"", await response.Content.ReadAsStringAsync());
        }
    }
}
