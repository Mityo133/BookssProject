using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Bookss.Models
{
    public class BooksRating
    {
        [Key]
        public int Id { get; set; }
        public int BookId { get; set; }
        public int UserId1 { get; set; }
        public int Rating { get; set; }
        public string Text { get; set; }
        public virtual Book Book { get; set; }
        public virtual IdentityUser User { get; set; }
    }
}
