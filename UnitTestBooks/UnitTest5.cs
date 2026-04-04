using Bookss.Controllers;
using Bookss.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using System.Security.Claims;

public class FavoriteBooksControllerTests
{
    private readonly Mock<IFavoriteBooksService> _serviceMock = new();

    private FavoriteBooksController CreateController()
    {
        var controller = new FavoriteBooksController(_serviceMock.Object);

        var user = new ClaimsPrincipal(new ClaimsIdentity(new[]
        {
            new Claim(ClaimTypes.NameIdentifier, "user1")
        }));

        controller.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext { User = user }
        };

        return controller;
    }

    [Fact]
    public async Task Create_AddsFavorite()
    {
        var controller = CreateController();

        var result = await controller.Create(1);

        _serviceMock.Verify(s => s.AddToFavoritesAsync(It.IsAny<object>()), Times.Once);

        Assert.IsType<RedirectToActionResult>(result);
    }

    [Fact]
    public async Task Index_ReturnsView()
    {
        _serviceMock.Setup(s => s.GetUserFavoritesAsync("user1"))
            .ReturnsAsync(new List<object>());

        var controller = CreateController();

        var result = await controller.Index();

        Assert.IsType<ViewResult>(result);
    }
}