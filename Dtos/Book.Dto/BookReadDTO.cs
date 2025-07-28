using ReadingTracker.API.Enums;

namespace ReadingTracker.API.Dtos.Book.Dto;

public class BookReadDTO
{
    public Guid BookId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public Gender Gender { get; set; } = Gender.Other;
    public int TotalPages { get; set; }
}