namespace ReadingTracker.API.Entities;

public class User
{
    public int UserId { get; set; }
    public string Name { get; set; } = string.Empty!;
    public string Email { get; set; } = string.Empty!;
    public string PasswordHashed { get; set; } = string.Empty!;
    public DateTime RegistrationDate { get; set; }

    // Navegação
    public ICollection<Book> Books { get; set; } = new List<Book>(); // 1 User -> N Books
    public ICollection<Reading> Readings { get; set; } = new List<Reading>(); // 1 User -> N Radings
}