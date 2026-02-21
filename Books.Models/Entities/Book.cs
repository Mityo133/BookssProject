using System.ComponentModel.DataAnnotations;

namespace Bookss.Models.Entities
{
    public class Book
    {
        [Key]
        public Guid Id { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }

        public Guid GenreId { get; set; }
        public virtual Genre? Genre { get; set; }

        public Guid AuthorId { get; set; }
        public virtual Author? Author { get; set; }

        public virtual List<BooksRating>? Ratings { get; set; }
        public virtual List<MyFavoriteBook>? FavoriteBooks { get; set; }
    }
}