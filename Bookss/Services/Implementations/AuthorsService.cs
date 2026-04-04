using Bookss.Data;
using Bookss.Models;
using Microsoft.EntityFrameworkCore;

public class AuthorsService : IAuthorsService
{
    private readonly ApplicationDbContext _context;

    public AuthorsService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Author>> GetAllAsync()
        => await _context.Authors.ToListAsync();

    public async Task<Author?> GetByIdAsync(int id)
        => await _context.Authors.FindAsync(id);

    public async Task CreateAsync(Author author)
    {
        _context.Authors.Add(author);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Author author)
    {
        _context.Authors.Update(author);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var author = await _context.Authors.FindAsync(id);
        if (author != null)
        {
            _context.Authors.Remove(author);
            await _context.SaveChangesAsync();
        }
    }
}