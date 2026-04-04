using Bookss.Data;
using Bookss.Models;
using Microsoft.EntityFrameworkCore;

public class MyFavoriteBooksService : IMyFavoriteBooksService
{
    private readonly ApplicationDbContext _context;

    public MyFavoriteBooksService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddToFavoritesAsync(MyFavoriteBook favorite)
    {
        _context.MyFavoriteBooks.Add(favorite);
        await _context.SaveChangesAsync();
    }

    public async Task RemoveFromFavoritesAsync(int id)
    {
        var fav = await _context.MyFavoriteBooks.FindAsync(id);
        if (fav != null)
        {
            _context.MyFavoriteBooks.Remove(fav);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<MyFavoriteBook>> GetUserFavoritesAsync(string userId)
    {
        return await _context.MyFavoriteBooks
            .Where(f => f.UserId == userId)
            .Include(f => f.Book)
            .ToListAsync();
    }
}