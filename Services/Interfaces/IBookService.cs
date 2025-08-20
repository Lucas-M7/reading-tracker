using ReadingTracker.API.Dtos.BookDTO;
using ReadingTracker.API.Entities;
using ReadingTracker.API.Enums;

namespace ReadingTracker.API.Services.Interfaces;

public interface IBookService
{
    Task<BookReadDTO> CreateBookAsync(BookCreateDTO bookDto);
    Task<BookReadDTO?> GetBookByIdAsync(Guid id);
    Task<IEnumerable<BookReadDTO>> GetAllBooksAsync();
    Task<BookReadDTO?> UpdateBookAsync(Guid id, BookUpdateDTO bookDto);
    Task<bool> DeleteBookAsync(Guid id);

    Task<IEnumerable<BookReadDTO>> SearchBooksAsync(string title, string? author, Gender? gender, int? totalPages, int pageNumber, int pageSize);
}