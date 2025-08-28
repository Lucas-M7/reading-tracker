using System.ComponentModel.DataAnnotations;
using ReadingTracker.API.Enums;

namespace ReadingTracker.API.Dtos.BookDTO;

public class BookUpdateDTO
{
    [Required(ErrorMessage = "O título é obrigatório.")]
    [StringLength(100, ErrorMessage = "O título não pode ter mais de 100 caracteres.")]
    public string Title { get; set; } = string.Empty;

    [Required(ErrorMessage = "O autor é obrigatório.")]
    [StringLength(100, ErrorMessage = "O autor não pode ter mais de 100 caracteres.")]
    public string Author { get; set; } = string.Empty;
    public Genre Genre { get; set; } = Genre.Other;

    [Range(1, int.MaxValue, ErrorMessage = "O número total de páginas deve ser de no mínimo 1.")]
    public int TotalPages { get; set; }
}