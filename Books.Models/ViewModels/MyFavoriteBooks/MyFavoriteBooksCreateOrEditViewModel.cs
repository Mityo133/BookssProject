using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Models.ViewModels.MyFavoriteBooks
{
    public class MyFavoriteBooksCreateOrEditViewModel
    {
        public Guid? Id { get; set; }   // Null when creating, filled when editing

        [Required]
        public Guid BookId { get; set; }

        [Required]
        public string UserId { get; set; }
    }
}
