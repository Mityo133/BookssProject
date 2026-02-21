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
    public interface IBookService
    {
        Task<List<BookViewModel>> GetAllAsync();
        Task<BookViewModel?> GetByIdAsync(Guid id);
        Task CreateAsync(BookCreateOrEditViewModel model);
        Task UpdateAsync(BookCreateOrEditViewModel model);
        Task DeleteAsync(Guid id);
    }
}
