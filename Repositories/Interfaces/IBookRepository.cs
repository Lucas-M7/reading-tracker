using ReadingTracker.API.Entities;

namespace ReadingTracker.API.Repositories.Interfaces;

public interface IBookRepository
{
    Task<IEnumerable<Book>> GetAllByUserIdAsync(Guid userId);
    Task<Book> GetByIdAsync(Guid id);
    Task<Book> CreateAsync(Book book);
    Task<Book> UpdateAsync(Book book);
    Task<bool> DeleteAsync(Book book);
}