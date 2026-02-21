using AutoMapper;
using Books.Business.Repositories;
using Books.Business.Services.Interfaces;
using Books.Models.ViewModels.Authors;
using Books.Models.ViewModels.Book;
using Books.Services.Interfaces;
using Bookss.Data;
using Bookss.Models.Entities;
using Microsoft.EntityFrameworkCore;


namespace Books.Business.Services.Implementations
{
    public class BookService : IBookService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public BookService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<BookViewModel>> GetAllAsync()
        {
            var books = await _context.Books
                .Include(b => b.Author)
                .Include(b => b.Genre)
                .ToListAsync();

            return _mapper.Map<List<BookViewModel>>(books);
        }

        public async Task<BookViewModel?> GetByIdAsync(Guid id)
        {
            var book = await _context.Books
                .Include(b => b.Author)
                .Include(b => b.Genre)
                .FirstOrDefaultAsync(x => x.Id == id);

            return book == null ? null : _mapper.Map<BookViewModel>(book);
        }

        public async Task CreateAsync(BookCreateOrEditViewModel model)
        {
            var book = _mapper.Map<Book>(model);
            await _context.Books.AddAsync(book);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(BookCreateOrEditViewModel model)
        {
            var book = await _context.Books.FindAsync(model.Id);

            if (book == null)
                return;

            _mapper.Map(model, book);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var book = await _context.Books.FindAsync(id);

            if (book == null)
                return;

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
        }

        Task<List<BookViewModel>> IBookService.GetAllAsync()
        {
            throw new NotImplementedException();
        }

        Task<BookViewModel?> IBookService.GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        Task IBookService.CreateAsync(BookCreateOrEditViewModel model)
        {
            throw new NotImplementedException();
        }

        Task IBookService.UpdateAsync(BookCreateOrEditViewModel model)
        {
            throw new NotImplementedException();
        }

        Task IBookService.DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
