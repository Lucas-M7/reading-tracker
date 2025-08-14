using ReadingTracker.API.Dtos.BookDTO;
using ReadingTracker.API.Entities;
using ReadingTracker.API.Enums;

namespace ReadingTracker.API.Services.Interfaces;

public interface IBookService
{
    Task<Book> CreateBookAsync(BookCreateDTO bookDto);
    Task<Book?> GetBookByIdAsync(Guid id);
    Task<IEnumerable<BookReadDTO>> GetAllBooksAsync();
    Task<Book?> UpdateBookAsync(Guid id, BookUpdateDTO bookDto);
    Task<bool> DeleteBookAsync(Guid id);

    Task<IEnumerable<BookReadDTO>> SearchBooksAsync(string title, string? author, Gender? gender, int? totalPages);

    Task<bool> MarkAsReadAsync(Guid id);
}