using AutoMapper;
using ReadingTracker.API.Dtos.BookDTO;
using ReadingTracker.API.Entities;

namespace ReadingTracker.API.Dtos.Mappings;

public class BookProfile : Profile
{
    public BookProfile()
    {
        // Entidade -> DTO de retorno (leitura)
        CreateMap<Book, BookReadDTO>();

        // DTO de criação -> Entidade
        CreateMap<BookCreateDTO, Book>();

        // DTO de atualização -> Entidade
        CreateMap<BookUpdateDTO, Book>();
    }
}