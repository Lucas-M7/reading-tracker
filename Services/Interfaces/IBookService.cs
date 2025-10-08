using ReadingTracker.API.Dtos.BookDTO;
using ReadingTracker.API.Enums;
using ReadingTracker.API.Services.Common;

namespace ReadingTracker.API.Services.Interfaces;

public interface IBookService
{
    Task<BookReadDTO> CreateBookAsync(BookCreateDTO bookDto);
    Task<BookReadDTO?> GetBookByIdAsync(Guid bookId);
    Task<IEnumerable<BookReadDTO>> GetAllBooksAsync();
    Task<BookReadDTO?> UpdateBookAsync(Guid bookId, BookUpdateDTO bookDto);
    Task<bool> DeleteBookAsync(Guid bookId);

    Task<PagedResult<BookReadDTO>> SearchBooksAsync(string? title, string? author, Genre? genre, int pageSize);
}