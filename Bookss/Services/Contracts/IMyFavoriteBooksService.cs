using Bookss.Models;


public interface IMyFavoriteBooksService
{
    Task AddToFavoritesAsync(MyFavoriteBook favorite);
    Task RemoveFromFavoritesAsync(int id);
    Task<IEnumerable<MyFavoriteBook>> GetUserFavoritesAsync(string userId);
}