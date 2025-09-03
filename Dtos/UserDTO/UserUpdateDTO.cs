using System.ComponentModel.DataAnnotations;

namespace ReadingTracker.API.Dtos.UserDTO;

public class UserUpdateDTO
{
    [StringLength(100, ErrorMessage = "O nome não pode ter mais de 100 caracteres.")]
    public string? FullName { get; set; }

    [EmailAddress(ErrorMessage = "O formato do email é inválido.")]
    public string? Email { get; set; }
}