using Bookss.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

public class FavoriteBooksController : Controller
{
    private readonly IMyFavoriteBooksService _favoriteService;

    public FavoriteBooksController(IMyFavoriteBooksService favoriteService)
    {
        _favoriteService = favoriteService;
    }

    // GET: My Favorites
    public async Task<IActionResult> Index()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        var favorites = await _favoriteService.GetUserFavoritesAsync(userId);
        return View(favorites);
    }

    // POST: Add to favorites
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(int bookId)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        await _favoriteService.AddToFavoritesAsync(new MyFavoriteBook
        {
            BookId = bookId,
            UserId = userId
        });

        return RedirectToAction("Index", "Books");
    }

    // POST: Remove
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        await _favoriteService.RemoveFromFavoritesAsync(id);
        return RedirectToAction(nameof(Index));
    }
}