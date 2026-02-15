using System;
using Microsoft.AspNetCore.Identity;


namespace Bookss.Models.Entities
{
    
    public class MyFavoriteBook
    {
        public Guid Id { get; set; }

        
        public string UserId { get; set; }
        public virtual IdentityUser User { get; set; }

        
        public Guid BookId { get; set; }
        public virtual Book Book { get; set; }
    }
}
