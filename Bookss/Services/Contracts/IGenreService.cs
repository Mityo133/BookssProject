using Bookss.Models;

public interface IGenreService
{
    Task<IEnumerable<Genre>> GetAllAsync();
    Task<Genre?> GetByIdAsync(int id);
    Task CreateAsync(Genre genre);
    Task UpdateAsync(Genre genre);
    Task DeleteAsync(int id);
}
