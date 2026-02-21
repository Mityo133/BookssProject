using Bookss.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Books.Models.ViewModels.Book;
using Books.Services.Interfaces;
namespace Books.Business.Services.Interfaces
{
    internal class IBookService
    {
        Task<IEnumerable<Book>> AllAsync { get; }

        Task<Book?> GetByIdAsync(int id);
        Task CreateAsync(Book book);
        Task UpdateAsync(Book book);
        Task DeleteAsync(int id);
    }
}
