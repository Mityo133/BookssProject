using AutoMapper;
using Books.Models.ViewModels.MyFavoriteBooks;
using Bookss.Models.Entities;


public class MyFavoriteBookProfile : Profile
{
    public MyFavoriteBookProfile()
    {
        CreateMap<MyFavoriteBook, MyFavoriteBooksViewModel>()
            .ForMember(dest => dest.BookTitle,
                opt => opt.MapFrom(src => src.Book.Title));

        CreateMap<MyFavoriteBooksViewModel, MyFavoriteBook>();
    }
}