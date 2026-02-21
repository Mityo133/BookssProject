using AutoMapper;
using Books.Models.ViewModels.Genre;
using Bookss.Models.Entities;
using Books.Models.ViewModels;

public class GenreProfile : Profile
{
    public GenreProfile()
    {
        CreateMap<Genre, GenreViewModel>();
        CreateMap<GenreViewModel, Genre>();
    }
}