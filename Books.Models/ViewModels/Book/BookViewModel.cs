using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Models.ViewModels.Book
{

    public class BookViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }

        public Guid AuthorId { get; set; }
        public string AuthorName { get; set; }

        public Guid GenreId { get; set; }
        public string GenreName { get; set; }
    }

}
