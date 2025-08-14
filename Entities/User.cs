using ReadingTracker.API.Entities.Identity;

namespace ReadingTracker.API.Entities;

public class User
{
    public Guid UserId { get; set; }

    public string ApplicationUserId { get; set; } = default!;
    public ApplicationUser ApplicationUser { get; set; } = default!;

    public string DisplayName { get; set; } = default!;
    public DateTime RegistrationDate { get; set; } = DateTime.UtcNow;

    // Navegação
    public ICollection<Book> Books { get; set; } = new List<Book>(); // 1 User -> N Books
    public ICollection<Reading> Readings { get; set; } = new List<Reading>(); // 1 User -> N Radings
}