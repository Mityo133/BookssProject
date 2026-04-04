using Bookss.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

public class BooksController : Controller
{
    private readonly IBookService _bookService;
    private readonly IAuthorsService _authorsService;
    private readonly IGenreService _genresService;

    public BooksController(
        IBookService bookService,
        IAuthorsService authorsService,
        IGenreService genresService)
    {
        _bookService = bookService;
        _authorsService = authorsService;
        _genresService = genresService;
    }

    // ✅ GET: Books
    public async Task<IActionResult> Index()
    {
        var books = await _bookService.GetAllAsync();
        return View(books);
    }

    // ✅ GET: Books/Details/5
    public async Task<IActionResult> Details(int id)
    {
        var book = await _bookService.GetByIdAsync(id);

        if (book == null)
            return NotFound();

        return View(book);
    }

    // ✅ GET: Books/Create
    public async Task<IActionResult> Create()
    {
        await LoadDropdowns();
        return View();
    }

    // ✅ POST: Books/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Book book)
    {
        if (!ModelState.IsValid)
        {
            await LoadDropdowns();
            return View(book);
        }

        await _bookService.CreateAsync(book);
        return RedirectToAction(nameof(Index));
    }

    // ✅ GET: Books/Edit/5
    public async Task<IActionResult> Edit(int id)
    {
        var book = await _bookService.GetByIdAsync(id);

        if (book == null)
            return NotFound();

        await LoadDropdowns();
        return View(book);
    }

    // ✅ POST: Books/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Book book)
    {
        if (id != book.Id)
            return BadRequest();

        if (!ModelState.IsValid)
        {
            await LoadDropdowns();
            return View(book);
        }

        await _bookService.UpdateAsync(book);
        return RedirectToAction(nameof(Index));
    }

    // ✅ GET: Books/Delete/5
    public async Task<IActionResult> Delete(int id)
    {
        var book = await _bookService.GetByIdAsync(id);

        if (book == null)
            return NotFound();

        return View(book);
    }

    // ✅ POST: Books/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _bookService.DeleteAsync(id);
        return RedirectToAction(nameof(Index));
    }

    // 🔧 Helper method (VERY IMPORTANT)
    private async Task LoadDropdowns()
    {
        var authors = await _authorsService.GetAllAsync();
        var genres = await _genresService.GetAllAsync();

        ViewBag.AuthorId = new SelectList(authors, "Id", "Name");
        ViewBag.GenreId = new SelectList(genres, "Id", "Name");
    }
}
