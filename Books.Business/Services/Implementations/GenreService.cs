using AutoMapper;
using Books.Models.ViewModels.Genre;
using Bookss.Data;
using Bookss.Models.Entities;
using Microsoft.EntityFrameworkCore;

public class GenreService : IGenreService
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GenreService(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<GenreViewModel>> GetAllAsync()
    {
        var genres = await _context.Genre.ToListAsync();
        return _mapper.Map<List<GenreViewModel>>(genres);
    }

    public async Task<GenreViewModel?> GetByIdAsync(Guid id)
    {
        var genre = await _context.Genre.FindAsync(id);
        return genre == null ? null : _mapper.Map<GenreViewModel>(genre);
    }

    public async Task CreateAsync(CreateGenreViewModel model)
    {
        var genre = _mapper.Map<Genre>(model);
        await _context.Genre.AddAsync(genre);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(CreateGenreViewModel model)
    {
        var genre = await _context.Genre.FindAsync(model.Id);

        if (genre == null)
            return;

        _mapper.Map(model, genre);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var genre = await _context.Genre.FindAsync(id);

        if (genre == null)
            return;

        _context.Genre.Remove(genre);
        await _context.SaveChangesAsync();
    }

    Task<List<GenreViewModel>> IGenreService.GetAllAsync()
    {
        throw new NotImplementedException();
    }

    Task<GenreViewModel?> IGenreService.GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    Task IGenreService.CreateAsync(CreateGenreViewModel model)
    {
        throw new NotImplementedException();
    }

    Task IGenreService.UpdateAsync(CreateGenreViewModel model)
    {
        throw new NotImplementedException();
    }

    Task IGenreService.DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    
}