using Bookss.Data;
using Bookss.Models;

using Microsoft.EntityFrameworkCore;

public class GenreService : IGenreService
{
    private readonly ApplicationDbContext _context;

    public GenreService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Genre>> GetAllAsync()
        => await _context.Genre.ToListAsync();

    public async Task<Genre?> GetByIdAsync(int id)
        => await _context.Genre.FindAsync(id);

    public async Task CreateAsync(Genre genre)
    {
        _context.Genre.Add(genre);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Genre genre)
    {
        _context.Genre.Update(genre);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var genre = await _context.Genre.FindAsync(id);
        if (genre != null)
        {
            _context.Genre.Remove(genre);
            await _context.SaveChangesAsync();
        }
    }
}