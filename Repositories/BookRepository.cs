using Microsoft.EntityFrameworkCore;
using ReadingTracker.API.Data;
using ReadingTracker.API.Entities;
using ReadingTracker.API.Enums;
using ReadingTracker.API.Repositories.Interfaces;
using ReadingTracker.API.Services.Common;

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
    public async Task UpdateAsync(Book book)
    {
        _context.Entry(book).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Book book)
    {
        _context.Books.Remove(book);
        await _context.SaveChangesAsync();
    }

    public async Task<Book?> GetByIdAsync(Guid bookId, Guid userId)
    {
        return await _context.Books
            .FirstOrDefaultAsync(b => b.BookId == bookId && b.UserId == userId);
    }

    public async Task<IEnumerable<Book>> GetAllByUserIdAsync(Guid userId)
    {
        return await _context.Books
            .AsNoTracking()
            .Where(b => b.UserId == userId)
            .ToListAsync();
    }

    public async Task<bool> DoesBookExistAsync(Guid userId, string title, string author)
    {
        var normalizedTitle = title.Trim().ToLower();
        var normalizedAuthor = author.Trim().ToLower();

        return await _context.Books
            .AnyAsync(b => b.UserId == userId &&
                b.Title.ToLower() == normalizedTitle &&
                b.Author.ToLower() == normalizedAuthor);
    }

    public async Task<PagedResult<Book>> SearchAsync(Guid userId, string? title, string? author, Genre? genre, int pageSize)
    {
        var query = _context.Books
            .AsNoTracking()
            .Where(b => b.UserId == userId);

        if (!string.IsNullOrWhiteSpace(title))
        {
            query = query.Where(b => b.Title.ToLower().Contains(title.Trim().ToLower()));
        }

        if (!string.IsNullOrWhiteSpace(author))
        {
            query = query.Where(b => b.Author.ToLower().Contains(author.Trim().ToLower()));
        }

        if (genre.HasValue)
        {
            query = query.Where(b => b.Genre == genre.Value);
        }

        var totalCount = await query.CountAsync();

        var items = await query
            .OrderBy(b => b.Title)
            .Take(pageSize)
            .ToListAsync();

        return new PagedResult<Book>
        {
            Items = items,
            TotalCount = totalCount
        };
    }
}