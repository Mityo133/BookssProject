using AutoMapper;
using Books.Models.ViewModels.MyFavoriteBooks;
using Bookss.Models.Entities;


public class MyFavoriteBooksProfile : Profile
{
   
        public MyFavoriteBooksProfile()
        {
        CreateMap<MyFavoriteBooksViewModel, MyFavoriteBook>();
        CreateMap<MyFavoriteBook, MyFavoriteBooksViewModel>();
    }
    
}