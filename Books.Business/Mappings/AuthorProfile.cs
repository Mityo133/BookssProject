using AutoMapper;
using Bookss.Models.Entities;
using Books.Models.ViewModels.Authors;

public class AuthorProfile : Profile
{
    public AuthorProfile()
    {
        // Entity -> AuthorViewModel (Read)
        CreateMap<Author, AuthorViewModel>()
            .ForMember(dest => dest.BooksCount,
                       opt => opt.MapFrom(src => src.Books != null ? src.Books.Count : 0));

        // AuthorCreateOrEditViewModel <-> Entity (Create/Update)
        // We use ReverseMap to handle both directions
        CreateMap<AuthorCreateOrEditViewModel, Author>()
            .ForMember(dest => dest.Books, opt => opt.Ignore()) // Don't overwrite books on edit
            .ReverseMap();
    }
}