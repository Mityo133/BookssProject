using Bookss.Models;
using Microsoft.AspNetCore.Mvc;

public class AuthorsController : Controller
{
    private readonly IAuthorsService _authorsService;

    public AuthorsController(IAuthorsService authorsService)
    {
        _authorsService = authorsService;
    }

    // GET: Authors
    public async Task<IActionResult> Index()
    {
        var authors = await _authorsService.GetAllAsync();
        return View(authors);
    }

    // GET: Authors/Details/5
    public async Task<IActionResult> Details(int id)
    {
        var author = await _authorsService.GetByIdAsync(id);
        if (author == null) return NotFound();

        return View(author);
    }

    // GET: Authors/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Authors/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Author author)
    {
        if (!ModelState.IsValid)
            return View(author);

        await _authorsService.CreateAsync(author);
        return RedirectToAction(nameof(Index));
    }

    // GET: Authors/Edit/5
    public async Task<IActionResult> Edit(int id)
    {
        var author = await _authorsService.GetByIdAsync(id);
        if (author == null) return NotFound();

        return View(author);
    }

    // POST: Authors/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Author author)
    {
        if (id != author.Id)
            return BadRequest();

        if (!ModelState.IsValid)
            return View(author);

        await _authorsService.UpdateAsync(author);
        return RedirectToAction(nameof(Index));
    }

    // GET: Authors/Delete/5
    public async Task<IActionResult> Delete(int id)
    {
        var author = await _authorsService.GetByIdAsync(id);
        if (author == null) return NotFound();

        return View(author);
    }

    // POST: Authors/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _authorsService.DeleteAsync(id);
        return RedirectToAction(nameof(Index));
    }
}