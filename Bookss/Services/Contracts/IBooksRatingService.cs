using Bookss.Models;
public interface IBooksRatingService
{
    Task AddRatingAsync(BooksRating rating);
    Task<double> GetAverageRatingAsync(int bookId);
}