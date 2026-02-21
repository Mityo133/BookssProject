using Books.Business.Repositories;
using Bookss.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using Bookss.Models.Entities;
using Books.Models.ViewModels.Authors;
using Books.Services.Interfaces;
using Books.Business.Repositories;

namespace Books.Business.Services.Implementations
    
{
    internal class GenreService
    {
        private readonly IRepository<Genre> _repository;

        public GenreService(IRepository<Genre> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Genre>> GetAllAsync()
            => await _repository.GetAllAsync();

        public async Task<Genre> GetByIdAsync(int id)
            => await _repository.GetByIdAsync(id);

        public async Task CreateAsync(Genre genre)
            => await _repository.AddAsync(genre);

        public async Task UpdateAsync(Genre genre)
            => await _repository.UpdateAsync(genre);

        public async Task DeleteAsync(int id)
            => await _repository.DeleteAsync(id);
    }
}
