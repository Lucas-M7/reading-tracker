using ReadingTracker.API.Entities;
using ReadingTracker.API.Enums;
using ReadingTracker.API.Services.Common;

namespace ReadingTracker.API.Repositories.Interfaces;

public interface IBookRepository : IGenericRepository<Book>
{
    Task<IEnumerable<Book>> GetAllByUserIdAsync(Guid userId);
    Task<Book?> GetByIdAsync(Guid bookId, Guid userId);
    Task<bool> DoesBookExistAsync(Guid userId, string title, string author);
    Task<PagedResult<Book>> SearchAsync(Guid userId, string? title, string? author, Genre? genre, int pageNumber, int pageSize);
}