using Bookss.Controllers;
using Bookss.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

public class BooksRatingControllerTests
{
    private readonly Mock<IBooksRatingService> _serviceMock = new();

    [Fact]
    public async Task Create_ValidRating_Redirects()
    {
        var controller = new BooksRatingController(_serviceMock.Object);

        var result = await controller.Create(1, 5);

        _serviceMock.Verify(s => s.AddRatingAsync(It.IsAny<object>()), Times.Once);

        var redirect = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal("Details", redirect.ActionName);
    }

    [Fact]
    public async Task Create_InvalidRating_ReturnsBadRequest()
    {
        var controller = new BooksRatingController(_serviceMock.Object);

        var result = await controller.Create(1, 10);

        Assert.IsType<BadRequestResult>(result);
    }
}