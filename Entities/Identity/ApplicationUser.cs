using Microsoft.AspNetCore.Identity;

namespace ReadingTracker.API.Entities.Identity;

public class ApplicationUser : IdentityUser<Guid>
{
    public string FullName { get; set; } = string.Empty;
    public DateTime CreatedAt { get; init; }

    public ICollection<Book> Books { get; private set; } = new List<Book>();
    public ICollection<Reading> Readings { get; private set; } = new List<Reading>();

    public ApplicationUser()
    {
        CreatedAt = DateTime.UtcNow;
    }
}