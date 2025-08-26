using Microsoft.AspNetCore.Identity;

namespace ReadingTracker.API.Entities.Identity;

public class ApplicationUser : IdentityUser<Guid>
{
    public string FullName { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }

    public ICollection<Book> Books { get; set; } = new List<Book>();
    public ICollection<Reading> Readings { get; set; } = new List<Reading>();
}