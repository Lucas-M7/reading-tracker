using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using ReadingTracker.API.Dtos.BookDTO;
using ReadingTracker.API.Entities;
using ReadingTracker.API.Enums;
using ReadingTracker.API.Repositories.Interfaces;
using ReadingTracker.API.Services.Auth;
using ReadingTracker.API.Services.Common;
using ReadingTracker.API.Services.Interfaces;

namespace ReadingTracker.API.Services;

public class BookService : IBookService
{
    private readonly IBookRepository _bookRepository;
    private readonly ICurrentUserService _currentUser;
    private IMapper _mapper;
    private readonly ILogger<BookService> _logger;

    public BookService(IBookRepository bookRepository,
        ICurrentUserService currentUser,
        IMapper mapper,
        ILogger<BookService> logger)
    {
        _bookRepository = bookRepository;
        _currentUser = currentUser;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<BookReadDTO> CreateBookAsync(BookCreateDTO bookDto)
    {
        var userId = _currentUser.GetUserId();

        if (await _bookRepository.DoesBookExistAsync(userId, bookDto.Title, bookDto.Author))
        {
            throw new ArgumentException("Este livro já está cadastrado.");
        }

        var book = new Book(bookDto.Title, bookDto.Author, bookDto.Genre, bookDto.TotalPages, userId);

        var createdBook = await _bookRepository.CreateAsync(book);
        _logger.LogInformation("Livro criado. BookId={BookId} UserId={UserId}", createdBook.BookId, userId);

        return _mapper.Map<BookReadDTO>(createdBook);
    }

    public async Task<BookReadDTO?> GetBookByIdAsync(Guid bookId)
    {
        var userId = _currentUser.GetUserId();
        var book = await _bookRepository.GetByIdAsync(bookId, userId);

        if (book is null) return null;

        return _mapper.Map<BookReadDTO>(book);
    }

    public async Task<IEnumerable<BookReadDTO>> GetAllBooksAsync()
    {
        var userId = _currentUser.GetUserId();
        var books = await _bookRepository.GetAllByUserIdAsync(userId);
        return _mapper.Map<IEnumerable<BookReadDTO>>(books.OrderBy(b => b.Title));
    }

    public async Task<BookReadDTO?> UpdateBookAsync(Guid bookId, BookUpdateDTO bookDto)
    {
        var userId = _currentUser.GetUserId();
        var book = await _bookRepository.GetByIdAsync(bookId, userId);

        if (book is null) return null;

        _mapper.Map(bookDto, book);

        await _bookRepository.UpdateAsync(book);
        _logger.LogInformation("Livro atualizado. BookId={BookId} UserId={UserId}", book.BookId, userId);

        return _mapper.Map<BookReadDTO>(book);
    }

    public async Task<bool> DeleteBookAsync(Guid bookId)
    {
        var userId = _currentUser.GetUserId();
        var book = await _bookRepository.GetByIdAsync(bookId, userId);

        if (book is null) return false;

        await _bookRepository.DeleteAsync(book);
        _logger.LogInformation("Livro deletado. BookId={BookId} UserId={UserId}", book.BookId, userId);

        return true;
    }

    public async Task<PagedResult<BookReadDTO>> SearchBooksAsync(string? title, string? author, Genre? gender, int pageNumber, int pageSize)
    {
        var userId = _currentUser.GetUserId();

        var pagedBooks = await _bookRepository.SearchAsync(userId, title, author, gender, pageNumber, pageSize);

        var pagedDto = new PagedResult<BookReadDTO>
        {
            Items = _mapper.Map<IEnumerable<BookReadDTO>>(pagedBooks.Items),
            TotalCount = pagedBooks.TotalCount
        };

        return pagedDto;
    }
}