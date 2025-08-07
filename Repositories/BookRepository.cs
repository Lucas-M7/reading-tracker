using Microsoft.EntityFrameworkCore;
using ReadingTracker.API.Data;
using ReadingTracker.API.Entities;
using ReadingTracker.API.Repositories.Interfaces;

namespace ReadingTracker.API.Repositories;

public class BookRepository : IBookRepository
{
    private readonly AppDbContext _context;

    public BookRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Book> CreateAsync(Book book)
    {
        _context.Books.Add(book);
        await _context.SaveChangesAsync();
        return book;
    }

    public async Task<bool> DeleteAsync(Book book)
    {
        _context.Books.Remove(book);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<IEnumerable<Book>> GetAllByUserIdAsync(Guid userId)
    {
        return await _context.Books
            .Where(b => b.UserId == userId)
            .ToListAsync();
    }

    public async Task<Book> GetByIdAsync(Guid id)
    {
        return await _context.Books
            .Include(b => b.Readings)
            .FirstOrDefaultAsync(b => b.BookId == id);
    }

    public async Task<Book> UpdateAsync(Book book)
    {
        _context.Books.Update(book);
        await _context.SaveChangesAsync();
        return book;
    }
}