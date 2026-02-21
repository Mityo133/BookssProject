using System.ComponentModel.DataAnnotations;

namespace Bookss.Models.Entities
{
    public class Genre
    {
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        public virtual List<Book>? Books { get; set; }
    }
}