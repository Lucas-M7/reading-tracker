using ReadingTracker.API.Entities.Identity;
using ReadingTracker.API.Enums;

namespace ReadingTracker.API.Entities;

public class Book
{
    public Guid BookId { get; init; }
    public DateTime RegistrationDate { get; init; }

    public string Title { get; set; }
    public string Author { get; set; }
    public Genre Genre { get; set; }
    public int TotalPages { get; set; }


    public Guid UserId { get; init; }
    public ApplicationUser User { get; init; } = default!;

    public ICollection<Reading> Readings { get; private set; } = new List<Reading>();

    private Book() { }

    public Book(string title, string author, Genre genre, int totalPages, Guid userId)
    {
        BookId = Guid.NewGuid();
        RegistrationDate = DateTime.UtcNow;

        Title = title;
        Author = author;
        Genre = genre;
        TotalPages = totalPages;
        UserId = userId;
    }
    public void AddReading(Reading newReading)
    {
        Readings.Add(newReading);
    }
}