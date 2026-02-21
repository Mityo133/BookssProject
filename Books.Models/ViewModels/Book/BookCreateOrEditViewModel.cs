using System.ComponentModel.DataAnnotations;
namespace Books.Models.ViewModels.Book;


    public class BookCreateOrEditViewModel
{
    public Guid? Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string ImageUrl { get; set; }
   
    public Guid AuthorId { get; set; }
    public Guid GenreId { get; set; }
}
