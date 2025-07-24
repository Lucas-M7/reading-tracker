namespace ReadingTracker.API.Entities;

public class Book
{
    public Guid BookId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public string Gender { get; set; } = string.Empty;
    public int TotalPages { get; set; }
    public DateTime RegistrationDate { get; set; }

    // Relacionamento
    public Guid UserId { get; set; } // FK para o leitor do livro
    public User User { get; set; } = default!; // Navegação para o usuário

    // Leituras feitas a partir deste livro
    public ICollection<Reading> Readings { get; set; } = new List<Reading>();
}