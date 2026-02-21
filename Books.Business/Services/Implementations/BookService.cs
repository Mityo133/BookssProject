using AutoMapper;
using Books.Business.Repositories;
using Books.Business.Services.Interfaces;
using Books.Models.ViewModels.Authors;
using Books.Services.Interfaces;
using Bookss.Models.Entities;


namespace Books.Business.Services.Implementations
{
    internal class BookService : IBookService
    {
        private readonly IRepository<Book> _repository;

        public BookService(IRepository<Book> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Book>> GetAllAsync()
            => await _repository.GetAllAsync();

        public async Task<Book?> GetByIdAsync(int id)
            => await _repository.GetByIdAsync(id);

        public async Task CreateAsync(Book book)
            => await _repository.AddAsync(book);

        public async Task UpdateAsync(Book book)
            => await _repository.UpdateAsync(book);

        public async Task DeleteAsync(int id)
            => await _repository.DeleteAsync(id);
    }
}
