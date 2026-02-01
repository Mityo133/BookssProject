using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Bookss.Models
{
    public class Author
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Age { get; set; }
        public virtual List<Book>? Books { get; set; }
        
    }
}
