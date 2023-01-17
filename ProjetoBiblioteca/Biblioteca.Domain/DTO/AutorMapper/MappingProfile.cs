using AutoMapper;
using Biblioteca.Domain.DTO.Request;
using Biblioteca.Domain.Entities;


namespace Biblioteca.Domain.DTO.AutorMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Author, AuthorDTO>().ReverseMap();
            CreateMap<Book, BookDTO>().ReverseMap();
            CreateMap<Category, CategoryDTO>().ReverseMap();
            CreateMap<Client, ClientDTO>().ReverseMap();
            CreateMap<BookRental, BookRentalDTO>().ReverseMap();
            CreateMap<Stock, StockDTO>().ReverseMap();

            CreateMap<Author, AuthorRequest>().ReverseMap();
            CreateMap<Book, BookRequest>().ReverseMap();
            CreateMap<Category, CategoryRequest>().ReverseMap();
            CreateMap<Client, ClientRequest>().ReverseMap();
            CreateMap<BookRental, BookRentalRequest>().ReverseMap();
            CreateMap<Stock, StockRequest>().ReverseMap();
        }
    }
}
