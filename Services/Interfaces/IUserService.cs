using ReadingTracker.API.Dtos.UserDTO;
using ReadingTracker.API.Entities.Identity;

namespace ReadingTracker.API.Services.Interfaces;

public interface IUserService
{
    Task<UserReadDTO> RegisterAsync(UserRegisterDTO createDTO);
    Task<UserReadDTO> UpdateAsync(Guid id, UserUpdateDTO updateDTO);
    Task<bool> DeleteAsync(Guid id);
    Task<UserReadDTO?> LoginAsync(UserLoginDTO loginDTO);
}