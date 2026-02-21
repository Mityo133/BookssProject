using Books.Models.ViewModels.MyFavoriteBooks;


namespace Books.Business.Services.Interfaces
{
    public interface IMyFavoriteBooksService
    {
        Task<List<MyFavoriteBooksViewModel>> GetAllAsync();

        Task<MyFavoriteBooksViewModel?> GetByIdAsync(Guid id);
         Task CreateAsync(MyFavoriteBooksViewModel model);
         Task DeleteAsync(Guid id);
    }
}
