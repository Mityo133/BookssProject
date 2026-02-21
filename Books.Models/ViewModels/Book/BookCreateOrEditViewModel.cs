using System.ComponentModel.DataAnnotations;
namespace Books.Models.ViewModels.Book;

internal class BookCreateOrEditViewModel
{
    public Guid? Id { get; set; }
    [Required(ErrorMessage = "Моля, въведете име.")]
    [StringLength(100, ErrorMessage = "Името не може да е над 100 символа.")]
    [Display(Title = "Име на автора")]
    public string Title { get; set; }
}
