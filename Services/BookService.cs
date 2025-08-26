using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using ReadingTracker.API.Dtos.BookDTO;
using ReadingTracker.API.Entities;
using ReadingTracker.API.Enums;
using ReadingTracker.API.Repositories.Interfaces;
using ReadingTracker.API.Services.Auth;
using ReadingTracker.API.Services.Interfaces;

namespace ReadingTracker.API.Services;

public class BookService : IBookService
{
    private readonly IBookRepository _bookRepository;
    private readonly IUserRepository _userRepository;
    private readonly ICurrentUserService _currentUser;
    private IMapper _mapper;
    private readonly ILogger<BookService> _logger;

    public BookService(IBookRepository bookRepository,
        IUserRepository userRepository,
        ICurrentUserService currentUser,
        IMapper mapper,
        ILogger<BookService> logger)
    {
        _bookRepository = bookRepository;
        _userRepository = userRepository;
        _currentUser = currentUser;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<BookReadDTO> CreateBookAsync(BookCreateDTO bookDto)
    {
        var userId = _currentUser.GetUserId();

        if (bookDto.TotalPages <= 0) throw new ArgumentException("A quantidade total de páginas deve ser maior que zero.");

        var user = await _userRepository.GetByIdAsync(userId);
        if (user is null) throw new KeyNotFoundException("Usuário não encontrado.");

        var existing = await _bookRepository.GetAllByUserIdAsync(userId);
        if (existing.Any(b =>
            b.Title.Trim().ToLower() == bookDto.Title.Trim().ToLower() &&
            b.Author.Trim().ToLower() == bookDto.Author.Trim().ToLower()))
        {
            throw new ArgumentException("Este livro já está cadastrado.");
        }

        var book = _mapper.Map<Book>(bookDto);
        book.UserId = userId;

        await _bookRepository.CreateAsync(book);
        _logger.LogInformation("Livro criado. BookId={BookId} UserId={UserId}", book.BookId, userId);

        return _mapper.Map<BookReadDTO>(book);
    }

    public async Task<BookReadDTO?> GetBookByIdAsync(Guid id)
    {
        var userId = _currentUser.GetUserId();
        var book = await _bookRepository.GetByIdAsync(id);

        if (book is null) return null;

        if (book.UserId != userId) return null;

        return _mapper.Map<BookReadDTO>(book);
    }

    public async Task<IEnumerable<BookReadDTO>> GetAllBooksAsync()
    {
        var userId = _currentUser.GetUserId();
        var books = await _bookRepository.GetAllByUserIdAsync(userId);
        return _mapper.Map<IEnumerable<BookReadDTO>>(books.OrderBy(b => b.Title));
    }

    public async Task<BookReadDTO?> UpdateBookAsync(Guid id, BookUpdateDTO bookDto)
    {
        var userId = _currentUser.GetUserId();

        var book = await _bookRepository.GetByIdAsync(id);
        if (book is null || book.UserId != userId) return null;

        if (bookDto.TotalPages <= 0)
            throw new ArgumentException("O total de páginas deve ser maior que zero.");

        _mapper.Map(bookDto, book);

        await _bookRepository.UpdateAsync(book);
        _logger.LogInformation("Livro atualizado. BookId={BookId} UserId={UserId}", book.BookId, userId);

        return _mapper.Map<BookReadDTO>(book);
    }

    public async Task<bool> DeleteBookAsync(Guid id)
    {
        var userId = _currentUser.GetUserId();

        var book = await _bookRepository.GetByIdAsync(id);
        if (book is null || book.UserId != userId) return false;

        var ok = await _bookRepository.DeleteAsync(book);
        if (ok)
        {
            _logger.LogInformation("Livro deletado. BookId={Bookd} UserId={Userd}", book.BookId, userId);
        }

        return ok;
    }

    public async Task<IEnumerable<BookReadDTO>> SearchBooksAsync(string title, string? author, Genre? gender, int? totalPages, int pageNumber, int pageSize)
    {
        var userId = _currentUser.GetUserId();

        if (pageNumber <= 0) pageNumber = 1;
        if (pageSize <= 0 || pageSize > 100) pageSize = 20;
        if (totalPages <= 0) throw new InvalidOperationException("ERRO: Verifique a quantidade de páginas buscada.");

        var books = await _bookRepository.GetAllByUserIdAsync(userId);

        if (!string.IsNullOrWhiteSpace(title))
        {
            var t = title.Trim().ToLower();
            books = books.Where(b => b.Title.ToLower().Contains(t));
        }

        if (!string.IsNullOrWhiteSpace(author))
        {
            var a = author.Trim().ToLower();
            books = books.Where(b => b.Author.ToLower().Contains(a));
        }

        var paged = books
            .OrderBy(b => b.Title)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);

        return _mapper.Map<IEnumerable<BookReadDTO>>(paged);
    }
}