using System.ComponentModel.DataAnnotations;

namespace ReadingTracker.API.Dtos.UserDTO;

public class UserLoginDTO
{
    [Required(ErrorMessage = "Insira o email.")]
    [EmailAddress(ErrorMessage = "Insira um email válido.")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Insira a senha.")]
    public string Password { get; set; } = string.Empty;
}