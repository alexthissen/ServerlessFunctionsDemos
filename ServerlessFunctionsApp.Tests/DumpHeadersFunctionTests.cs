using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http;
using System.Web.Http;
using System.Threading.Tasks;

namespace ServerlessFunctionApp.Tests
{
    [TestClass]
    public class DumpHeadersFunctionTests
    {
        [TestMethod]
        public async Task GivenRequestHasHeaders_WhenRunIsCalled_ResponseShouldReflectHeaderValues()
        {
            // Arrange
            MockTraceWriter log = new MockTraceWriter();
            HttpConfiguration config = new HttpConfiguration();
            HttpRequestMessage request = new HttpRequestMessage();
            request.Headers.Add("custom", "AzureFunctions");
            request.SetConfiguration(config);

            // Act
            var response = DumpHeadersFunction.Run(request, log);

            // Assert
            Assert.AreEqual("\"custom='AzureFunctions',\"", await response.Content.ReadAsStringAsync());
        }
    }
}
