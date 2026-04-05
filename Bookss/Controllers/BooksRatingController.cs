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
public async Task<IActionResult> Create(int bookId)
{
    var book = await _ratingService.Books.FindAsync(bookId);

    if (book == null)
        return NotFound();

    return View(book); // pass the book to the view
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
