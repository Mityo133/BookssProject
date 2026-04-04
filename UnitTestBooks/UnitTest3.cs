using Bookss.Controllers;
using Bookss.Models;
using Bookss.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

public class GenresControllerTests
{
    private readonly Mock<IGenresService> _serviceMock = new();

    private GenresController CreateController()
        => new GenresController(_serviceMock.Object);

    [Fact]
    public async Task Index_ReturnsView()
    {
        _serviceMock.Setup(s => s.GetAllAsync())
            .ReturnsAsync(new List<Genre>());

        var controller = CreateController();

        var result = await controller.Index();

        Assert.IsType<ViewResult>(result);
    }
}