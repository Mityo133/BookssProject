using AutoMapper;
using Books.Business.Services.Interfaces;
using Books.Models.ViewModels.MyFavoriteBooks;
using Bookss.Data;
using Bookss.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Business.Services.Implementations
{
    public class MyFavoriteBooksService : IMyFavoriteBooksService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public MyFavoriteBooksService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<MyFavoriteBooksViewModel>> GetAllAsync()
        {
            var favorites = await _context.MyFavoriteBooks
                .Include(f => f.Book)
                .ToListAsync();

            return _mapper.Map<List<MyFavoriteBooksViewModel>>(favorites);
        }

        public async Task<MyFavoriteBooksViewModel?> GetByIdAsync(Guid id)
        {
            var favorite = await _context.MyFavoriteBooks
                .Include(f => f.Book)
                .FirstOrDefaultAsync(f => f.Id == id);

            return favorite == null ? null : _mapper.Map<MyFavoriteBooksViewModel>(favorite);
        }

        public async Task CreateAsync(MyFavoriteBooksViewModel model)
        {
            var favorite = _mapper.Map<MyFavoriteBook>(model);

            await _context.MyFavoriteBooks.AddAsync(favorite);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var favorite = await _context.MyFavoriteBooks.FindAsync(id);

            if (favorite == null)
                return;

            _context.MyFavoriteBooks.Remove(favorite);
            await _context.SaveChangesAsync();
        }

        Task<List<MyFavoriteBooksViewModel>> IMyFavoriteBooksService.GetAllAsync()
        {
            throw new NotImplementedException();
        }

        Task<MyFavoriteBooksViewModel?> IMyFavoriteBooksService.GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        Task IMyFavoriteBooksService.CreateAsync(MyFavoriteBooksViewModel model)
        {
            throw new NotImplementedException();
        }
    }
}
