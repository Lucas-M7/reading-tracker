using ReadingTracker.API.Dtos.UserDTO;
using ReadingTracker.API.Entities.Identity;

namespace ReadingTracker.API.Services.Interfaces;

public interface IUserService
{
    Task<ApplicationUser> RegisterAsync(UserCreateDTO createDTO);
    Task<bool> UpdateAsync(Guid id, UserUpdateDTO updateDTO);
    Task<bool> DeleteAsync(Guid id);
    Task<string> LoginAsync(UserLoginDTO loginDTO);
}