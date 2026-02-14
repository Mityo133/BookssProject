using System.ComponentModel.DataAnnotations;

namespace Bookss.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public int GenreId { get; set; }
        public virtual Genre Genre { get; set; }
        public int AuthorId { get; set; }
        public virtual Author Author { get; set; }
    }
}
