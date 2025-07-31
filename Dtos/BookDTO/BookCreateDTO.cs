using ReadingTracker.API.Enums;

namespace ReadingTracker.API.Dtos.BookDTO;

public class BookCreateDTO
{
    public string Title { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public Gender Gender { get; set; } = Gender.Other;
    public int TotalPages { get; set; }
    public Guid UserId { get; set; }
}
