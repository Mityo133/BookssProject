using Bookss.Controllers;
using Bookss.Models;
using Bookss.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

public class BooksControllerTests
{
    private readonly Mock<IBookService> _bookServiceMock = new();
    private readonly Mock<IAuthorsService> _authorsServiceMock = new();
    private readonly Mock<IGenresService> _genresServiceMock = new();

    private BooksController CreateController()
    {
        return new BooksController(
            _bookServiceMock.Object,
            _authorsServiceMock.Object,
            _genresServiceMock.Object);
    }

    [Fact]
    public async Task Index_ReturnsViewWithBooks()
    {
        _bookServiceMock.Setup(s => s.GetAllAsync())
            .ReturnsAsync(new List<Book> { new Book { Title = "Test" } });

        var controller = CreateController();

        var result = await controller.Index();

        var viewResult = Assert.IsType<ViewResult>(result);
        var model = Assert.IsAssignableFrom<IEnumerable<Book>>(viewResult.Model);

        Assert.Single(model);
    }

    [Fact]
    public async Task Details_ReturnsNotFound_WhenBookMissing()
    {
        _bookServiceMock.Setup(s => s.GetByIdAsync(1))
            .ReturnsAsync((Book)null);

        var controller = CreateController();

        var result = await controller.Details(1);

        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task Create_Post_Redirects_WhenValid()
    {
        var controller = CreateController();

        var result = await controller.Create(new Book { Title = "New Book" });

        var redirect = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal("Index", redirect.ActionName);

        _bookServiceMock.Verify(s => s.CreateAsync(It.IsAny<Book>()), Times.Once);
    }

    [Fact]
    public async Task Create_Post_ReturnsView_WhenInvalid()
    {
        var controller = CreateController();
        controller.ModelState.AddModelError("Title", "Required");

        var result = await controller.Create(new Book());

        Assert.IsType<ViewResult>(result);
    }
}