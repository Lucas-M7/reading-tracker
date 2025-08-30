using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ReadingTracker.API.Entities;
using ReadingTracker.API.Entities.Identity;

namespace ReadingTracker.API.Data;

public class AppDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Book> Books { get; set; } = default!;
    public DbSet<Reading> Readings { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Book>(book => // Configuração da entidade Book <-> User
        {
            book.ToTable("Books"); // Nome da tabela (opcional)
            book.HasKey(b => b.BookId); // Chave primária

            // Propriedades obrigatórias:
            book.Property(b => b.Title)
                .IsRequired()
                .HasMaxLength(200);

            book.Property(b => b.Author)
                .IsRequired()
                .HasMaxLength(100);

            book.Property(b => b.Gender)
                .HasConversion<string>()
                .IsRequired()
                .HasMaxLength(50);

            book.Property(b => b.TotalPages)
                .IsRequired();

            book.Property(b => b.UserId)
                .IsRequired();

            // Relacionamento: Book tem UM User
            book.HasOne(b => b.User) // 1 Livro tem UM usuário
                .WithMany(u => u.Books) // 1 Usuário tem MUITOS livros
                .HasForeignKey(b => b.UserId) // Chave estrangeira
                .OnDelete(DeleteBehavior.Cascade); // Ao deletar o usuário, deleta todos os seus livros
        });

        modelBuilder.Entity<Reading>(reading =>
        {
            reading.ToTable("Readings");
            reading.HasKey(r => r.ReadingId);

            reading.Property(r => r.StartPage)
                .IsRequired();

            reading.Property(r => r.EndPage)
                .IsRequired();

            reading.Property(r => r.Status)
                .HasConversion<string>()
                .IsRequired();

            reading.Property(r => r.BookId)
                .IsRequired();

            reading.Property(r => r.UserId)
                .IsRequired();

            // Configuração da entidade Reading <-> Book
            reading.HasOne(r => r.Book)
            .WithMany(b => b.Readings)
            .HasForeignKey(r => r.BookId)
            .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Reading>()
            .HasOne(r => r.User)
            .WithMany(u => u.Readings)
            .HasForeignKey(r => r.UserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}