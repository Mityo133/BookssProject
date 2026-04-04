using Bookss.Models;

public interface IAuthorsService
{
    Task<IEnumerable<Author>> GetAllAsync();
    Task<Author?> GetByIdAsync(int id);
    Task CreateAsync(Author author);
    Task UpdateAsync(Author author);
    Task DeleteAsync(int id);
}