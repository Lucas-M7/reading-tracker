using Microsoft.AspNetCore.Identity;
using ReadingTracker.API.Dtos.UserDTO;

namespace ReadingTracker.API.Services.Interfaces;

public interface IUserService
{
    Task<UserReadDTO> RegisterAsync(UserRegisterDTO registerDTO);
    Task<UserReadDTO?> UpdateAsync(Guid id, UserUpdateDTO updateDTO);
    Task<bool> DeleteAsync(Guid id);
    Task<UserReadDTO?> LoginAsync(UserLoginDTO loginDTO);
    Task<UserReadDTO?> GetUserByIdAsync(Guid id);
    Task<IdentityResult> ChangePasswordAsync(Guid id, ChangePasswordDTO passwordDTO);
}