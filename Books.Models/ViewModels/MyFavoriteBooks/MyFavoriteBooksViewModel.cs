using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Models.ViewModels.MyFavoriteBooks
{
    public class MyFavoriteBooksViewModel
    {
        public Guid Id { get; set; }

        public string UserId { get; set; }
        public virtual IdentityUser? User { get; set; }

        public Guid BookId { get; set; }
        
    }
}
