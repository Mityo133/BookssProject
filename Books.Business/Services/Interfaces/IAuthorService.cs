using Books.Models.ViewModels.Authors;

namespace Books.Services.Interfaces
{
    public interface IAuthorService
    {
        Task<IEnumerable<AuthorViewModel>> GetAllAuthorsAsync();
        Task<AuthorViewModel?> GetAuthorByIdAsync(Guid id);
        Task<AuthorCreateOrEditViewModel?> GetAuthorForEditAsync(Guid id);
        Task CreateAuthorAsync(AuthorCreateOrEditViewModel model);
        Task UpdateAuthorAsync(AuthorCreateOrEditViewModel model);
        Task<bool> DeleteAuthorAsync(Guid id);
    }
}