using Bookss.Controllers;
using Bookss.Models;
using Bookss.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

public class AuthorsControllerTests
{
    private readonly Mock<IAuthorsService> _serviceMock = new();

    private AuthorsController CreateController()
        => new AuthorsController(_serviceMock.Object);

    [Fact]
    public async Task Index_ReturnsView()
    {
        _serviceMock.Setup(s => s.GetAllAsync())
            .ReturnsAsync(new List<Author>());

        var controller = CreateController();

        var result = await controller.Index();

        Assert.IsType<ViewResult>(result);
    }

    [Fact]
    public async Task Create_Post_AddsAuthor()
    {
        var controller = CreateController();

        var result = await controller.Create(new Author { Name = "A" });

        _serviceMock.Verify(s => s.CreateAsync(It.IsAny<Author>()), Times.Once);
        Assert.IsType<RedirectToActionResult>(result);
    }
}