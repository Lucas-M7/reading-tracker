using ReadingTracker.API.Enums;

namespace ReadingTracker.API.Dtos.BookDTO;

public class BookReadDTO
{
    public Guid BookId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public Genre Genre { get; set; } = Genre.Other;
    public int TotalPages { get; set; }
    public DateTime DateAdded { get; set; }

    /// <summary>
    /// Total de páginas que o usuário já leu deste livro,
    /// </summary>
    public int PagesRead { get; set; }

    // /// <summary>
    // /// Percentual do progresso da leitura (0 a 100).
    // /// </summary>
    // public double Progress { get; set; }

    /// <summary>
    /// Indica se o livro já foi completamente lido.
    /// </summary>
    public bool IsFinished { get; set; }
}