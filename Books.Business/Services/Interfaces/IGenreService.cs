using Books.Models.ViewModels.Genre;
public interface IGenreService
{
    Task<List<GenreViewModel>> GetAllAsync();
    Task<GenreViewModel?> GetByIdAsync(Guid id);
    Task CreateAsync(CreateGenreViewModel model);
    Task UpdateAsync(CreateGenreViewModel model);
    Task DeleteAsync(Guid id);
    
}