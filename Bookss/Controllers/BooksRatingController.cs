using Bookss.Models;
using Microsoft.AspNetCore.Mvc;

public class BooksRatingController : Controller
{
    private readonly IBooksRatingService _ratingService;
    private readonly IBookService _bookService;

    public BooksRatingController(IBooksRatingService ratingService, IBookService bookService)
    {
        _ratingService = ratingService;
        _bookService = bookService;
    }

    // GET: Create rating form
    [HttpGet]
    public async Task<IActionResult> Create(int bookId)
    {
        var book = await _bookService.GetByIdAsync(bookId);

        if (book == null)
            return NotFound();

        var model = new BooksRating
        {
            BookId = book.Id,
            Text = ""
        };

        // Optional: pass title to View
        ViewBag.BookTitle = book.Title;

        return View(model);
    }

    // POST: Rate book
    [HttpPost]
    public async Task<IActionResult> Create(BooksRating model)
    {
        if (model.Rating < 1 || model.Rating > 5)
            return BadRequest();

        await _ratingService.AddRatingAsync(model);

        return RedirectToAction("Details", "Books", new { id = model.BookId });
    }
}