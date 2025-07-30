using ReadingTracker.API.Enums;

namespace ReadingTracker.API.Entities;

public class Reading
{
    public int ReadingId { get; set; }
    public DateTime Date { get; set; } = DateTime.UtcNow;
    public int StartPage { get; set; }
    public int EndPage { get; set; }
    public ReadingStatus Status { get; set; } = ReadingStatus.NotStarted;
    public string? Notes { get; set; }

    public int BookId { get; set; } // FK - Qual livro tá sendo ou foi lido
    public Book Book { get; set; } = default!; // Relação com o livro

    public int UserId { get; set; }
    public User User { get; set; } = default!;
}