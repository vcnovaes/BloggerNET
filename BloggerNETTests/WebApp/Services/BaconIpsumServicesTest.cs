using Moq;
using Moq.Protected;

namespace BloggerNETTests.WebApp.Services;

public class BaconIpsumServicesTest
{
    private Mock<HttpClient> _httpClientMock;
    private Mock<HttpResponseMessage> _httpResponseMock;
    private Mock<HttpContent> _httpResponseContentMock;
    
    public BaconIpsumServicesTest()
    {
        _httpClientMock = new Mock<HttpClient>();
        _httpResponseMock = new Mock<HttpResponseMessage>();
        _httpResponseContentMock = new Mock<HttpContent>();
        _httpResponseMock.Object.Content = _httpResponseContentMock.Object;
    }
    
    [Fact]
    public async Task Should_Return_A_ListOfStrings()
    {
        _httpClientMock.Setup<Task<HttpResponseMessage>>(
                x => x.SendAsync(new HttpRequestMessage())
            )
            .ReturnsAsync(_httpResponseMock.Object);
        _httpResponseContentMock.Setup(x => x.ReadAsStringAsync()).ReturnsAsync("[\"success\"]");
        var mockedService = new BloggerNET.Services.BaconIpsumService(_httpClientMock.Object);

        var response = await mockedService.GetContent(CancellationToken.None); 
        
        Assert.Equal("success", response.FirstOrDefault());
    }
}