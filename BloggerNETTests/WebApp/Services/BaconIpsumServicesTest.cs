using Moq;
using Moq.Protected;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace BloggerNETTests.WebApp.Services
{
    public class BaconIpsumServicesTest
    {
        private Mock<HttpMessageHandler> _httpMessageHandlerMock;
        private HttpClient _httpClient;

        public BaconIpsumServicesTest()
        {
            _httpMessageHandlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            _httpClient = new HttpClient(_httpMessageHandlerMock.Object)
            {
                BaseAddress = new Uri("https://baconipsum.com/api/") // Set your base address
            };
        }

        [Fact]
        public async Task Should_Return_A_ListOfStrings()
        {
            // Arrange
            var expectedResponse = "[\"success\"]";
            var responseContent = new StringContent(expectedResponse);

            _httpMessageHandlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = responseContent,
                });

            var service = new BloggerNET.Services.BaconIpsumService(_httpClient);

            // Act
            var response = await service.GetContent(CancellationToken.None);

            // Assert
            Assert.Equal("success", response.FirstOrDefault());
        }
    }
}