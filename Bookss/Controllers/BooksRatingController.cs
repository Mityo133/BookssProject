using Bookss.Models;
using Microsoft.AspNetCore.Mvc;

public class BooksRatingController : Controller
{
    private readonly IBooksRatingService _ratingService;

    public BooksRatingController(IBooksRatingService ratingService)
    {
        _ratingService = ratingService;
    }
   
    [HttpGet]
   [HttpGet]
[HttpGet]
public async Task<IActionResult> Create(int bookId)
{
    var book = await _bookService.GetByIdAsync(bookId);

    if (book == null)
        return NotFound();

    var model = new BooksRating
    {
        BookId = book.Id
    };

    return View(model);
}
    // POST: Rate book
    [HttpPost]
    public async Task<IActionResult> Create(int bookId, int rating)
    {
        if (rating < 1 || rating > 5)
            return BadRequest();

        var ratingModel = new BooksRating
        {
            BookId = bookId,
            Rating = rating
        };

        await _ratingService.AddRatingAsync(ratingModel);

        return RedirectToAction("Details", "Books", new { id = bookId });
    }
}
