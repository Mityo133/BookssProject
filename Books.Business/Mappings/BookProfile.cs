using AutoMapper;
using Books.Models.ViewModels.Book;
using Bookss.Models.Entities;


public class BookProfile : Profile
{
    public BookProfile()
    {
        CreateMap<Book, BookViewModel>()
            .ForMember(dest => dest.AuthorName,
                opt => opt.MapFrom(src => src.Author.Name))
            .ForMember(dest => dest.GenreName,
                opt => opt.MapFrom(src => src.Genre.Name));

        CreateMap<BookViewModel, Book>();
    }
}