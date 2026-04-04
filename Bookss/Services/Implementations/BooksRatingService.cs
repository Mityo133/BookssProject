using Bookss.Data;
using Bookss.Models;
using Microsoft.EntityFrameworkCore;

public class BooksRatingService : IBooksRatingService
{
    private readonly ApplicationDbContext _context;

    public BooksRatingService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddRatingAsync(BooksRating rating)
    {
        _context.BooksRating.Add(rating);
        await _context.SaveChangesAsync();
    }

    public async Task<double> GetAverageRatingAsync(int bookId)
    {
        return await _context.BooksRating
            .Where(r => r.BookId == bookId)
            .AverageAsync(r => (double?)r.Rating) ?? 0;
    }
}