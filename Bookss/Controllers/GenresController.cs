using Bookss.Models;
using Microsoft.AspNetCore.Mvc;

public class GenresController : Controller
{
    private readonly IGenreService _genresService;

    public GenresController(IGenreService genresService)
    {
        _genresService = genresService;
    }

    public async Task<IActionResult> Index()
    {
        var genres = await _genresService.GetAllAsync();
        return View(genres);
    }

    public async Task<IActionResult> Details(int id)
    {
        var genre = await _genresService.GetByIdAsync(id);
        if (genre == null) return NotFound();

        return View(genre);
    }

    public IActionResult Create() => View();

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Genre genre)
    {
        if (!ModelState.IsValid)
            return View(genre);

        await _genresService.CreateAsync(genre);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var genre = await _genresService.GetByIdAsync(id);
        if (genre == null) return NotFound();

        return View(genre);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Genre genre)
    {
        if (id != genre.Id)
            return BadRequest();

        if (!ModelState.IsValid)
            return View(genre);

        await _genresService.UpdateAsync(genre);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int id)
    {
        var genre = await _genresService.GetByIdAsync(id);
        if (genre == null) return NotFound();

        return View(genre);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _genresService.DeleteAsync(id);
        return RedirectToAction(nameof(Index));
    }
}