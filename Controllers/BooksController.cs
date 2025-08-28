using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using ReadingTracker.API.Dtos.BookDTO;
using ReadingTracker.API.Enums;
using ReadingTracker.API.Services.Common;
using ReadingTracker.API.Services.Interfaces;

namespace ReadingTracker.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class BooksController : ControllerBase
{
    private readonly IBookService _bookService;
    private readonly ILogger<BooksController> _logger;

    public BooksController(IBookService bookService, ILogger<BooksController> logger)
    {
        _bookService = bookService;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<BookReadDTO>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        _logger.LogInformation("Buscando todos os livros do usuário.");
        var books = await _bookService.GetAllBooksAsync();
        return Ok(books);
    }

    [HttpGet("{bookId:guid}", Name = "GetBookById")]
    [ProducesResponseType(typeof(BookReadDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetBookById(Guid bookId)
    {
        _logger.LogInformation("Buscando livro pelo ID: {BookId}", bookId);
        var book = await _bookService.GetBookByIdAsync(bookId);

        if (book is null)
        {
            _logger.LogWarning("Livro com ID: {BookId} não encontrado.", bookId);
            return NotFound();
        }

        return Ok(book);
    }

    [HttpPost]
    [ProducesResponseType(typeof(BookReadDTO), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateBook([FromBody] BookCreateDTO bookDto)
    {
        try
        {
            _logger.LogInformation("Tentativa de criação de novo livro com título: {Title}", bookDto.Title);
            var newBook = await _bookService.CreateBookAsync(bookDto);

            return CreatedAtAction(nameof(GetBookById), new { bookId = newBook.BookId }, newBook);
        }
        catch (ArgumentException ex)
        {
            _logger.LogWarning(ex, "Erro de argumento ao criar livro: {Message}", ex.Message);
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPut("{bookId:guid}")]
    [ProducesResponseType(typeof(BookReadDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateBook(Guid bookId, [FromBody] BookUpdateDTO bookDto)
    {
        _logger.LogInformation("Tentativa de atualização do livro com ID: {BookId}", bookId);
        var updatedBook = await _bookService.UpdateBookAsync(bookId, bookDto);

        if (updatedBook is null)
        {
            _logger.LogWarning("Livro com ID: {BookId} não encontrado para atualização.", bookId);
            return NotFound();
        }

        return Ok(updatedBook);
    }

    [HttpDelete("{bookId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteBook(Guid bookId)
    {
        _logger.LogInformation("Tentativa de deleção do livro de ID: {BookId}", bookId);
        var success = await _bookService.DeleteBookAsync(bookId);

        if (!success)
        {
            _logger.LogWarning("Livro com ID: {BookId} não encontrado para deleção", bookId);
            return NotFound();
        }

        return NoContent();
    }

    [HttpGet("search")]
    [ProducesResponseType(typeof(PagedResult<BookReadDTO>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Search([FromQuery] string? title, [FromQuery] string? author,
        [FromQuery] Genre? genre, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
    {
        _logger.LogInformation("Executando busca de livros com parâmetros.");
        var result = await _bookService.SearchBooksAsync(title, author, genre, pageNumber, pageSize);
        return Ok(result);
    }
}