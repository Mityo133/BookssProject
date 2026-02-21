using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Bookss.Models.Entities
{
    public class BooksRating
    {
        [Key]
        public Guid Id { get; set; }

        public Guid BookId { get; set; }
        public virtual Book? Book { get; set; }

        public string UserId { get; set; }
        public virtual IdentityUser? User { get; set; }

        public int Rating { get; set; }
        public string Text { get; set; }
    }
}