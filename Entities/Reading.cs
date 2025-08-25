using ReadingTracker.API.Entities.Identity;
using ReadingTracker.API.Enums;

namespace ReadingTracker.API.Entities;

public class Reading
{
    public Guid ReadingId { get; set; }
    public DateTime Date { get; set; } = DateTime.UtcNow;
    public int StartPage { get; set; }
    public int EndPage { get; set; }
    public ReadingStatus Status { get; set; } = ReadingStatus.NotStarted;
    public string? Notes { get; set; }

    public Guid BookId { get; set; } // FK - Qual livro tá sendo ou foi lido
    public Book Book { get; set; } = default!; // Relação com o livro

    public Guid UserId { get; set; }
    public ApplicationUser User { get; set; } = default!;
}