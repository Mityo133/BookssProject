using Bookss.Models.Entities;
using Bookss.Models.ViewModels.Genre;
public interface IGenreService
{
    Task<IEnumerable<GenreViewModel>> GetAllAsync();
    Task<GenreViewModel> GetByIdAsync(Guid id);
    Task CreateAsync(GenreCreateViewModel model);
    Task UpdateAsync(GenreEditViewModel model);
    Task DeleteAsync(Guid id);
}
