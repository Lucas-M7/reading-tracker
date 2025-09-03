using System.ComponentModel.DataAnnotations;

namespace ReadingTracker.API.Dtos.UserDTO;

public class ChangePasswordDTO
{
    [Required(ErrorMessage = "A senha atual é obrigatória.")]
    public string OldPassword { get; set; }

    [Required(ErrorMessage = "A nova senha é obrigatória.")]
    [StringLength(15, MinimumLength = 5, ErrorMessage = "A nova senha deve ter entre 5 a 15 caracteres.")]
    public string NewPassword { get; set; }

    [Required(ErrorMessage = "A confirmação de senha é obrigatória.")]
    [Compare("NewPassword", ErrorMessage = "As senhas não conferem.")]
    public string ConfirmNewPassword { get; set; }
}