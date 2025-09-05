using Microsoft.AspNetCore.Identity;
using ReadingTracker.API.Dtos.UserDTO;
using ReadingTracker.API.Entities.Identity;
using ReadingTracker.API.Services.Interfaces;

namespace ReadingTracker.API.Services;

public class UserService : IUserService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public UserService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public Task<bool> DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<UserReadDTO?> LoginAsync(UserLoginDTO loginDTO)
    {
        throw new NotImplementedException();
    }

    public Task<UserReadDTO> RegisterAsync(UserRegisterDTO createDTO)
    {
        throw new NotImplementedException();
    }

    public Task<UserReadDTO> UpdateAsync(Guid id, UserUpdateDTO updateDTO)
    {
        throw new NotImplementedException();
    }
}