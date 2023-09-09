using BloggerNET.Controllers;
using BloggerNET.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace BloggerNETTests.WebApp.Controllers
{
    public class HomeControllerTest
    {
        [Fact]
        public async Task Index_ReturnsViewWithContent()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<HomeController>>();
            var contentServiceMock = new Mock<IContentService>();

            var controller = new HomeController(loggerMock.Object, contentServiceMock.Object);
            var token = CancellationToken.None;

            // Mock content provider to return sample content
            var sampleContent = new List<string> { "Content1", "Content2", "Content3" };
            contentServiceMock.Setup(service => service.GetContent(token)).ReturnsAsync(sampleContent);

            // Act
            var result = await controller.Index(token) as ViewResult;

            // Assert
            Assert.NotNull(result);

            // Verify ViewData content
            Assert.NotNull(result.ViewData["index0"]);
            Assert.NotNull(result.ViewData["index1"]);
            Assert.NotNull(result.ViewData["index2"]);

            var viewDataContent0 = result.ViewData["index0"] as string;
            var viewDataContent1 = result.ViewData["index1"] as string;
            var viewDataContent2 = result.ViewData["index2"] as string;

            Assert.Equal("Content1", viewDataContent0);
            Assert.Equal("Content2", viewDataContent1);
            Assert.Equal("Content2", viewDataContent2);
        }

        // Add more tests for other action methods if needed
    }
}