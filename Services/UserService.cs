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

    public async Task<bool> DeleteAsync(Guid id)
    {
        var user = await _userManager.FindByIdAsync(id.ToString());
        if (user == null) return false;

        var result = await _userManager.DeleteAsync(user);
        return result.Succeeded;
    }

    public async Task<string> LoginAsync(UserLoginDTO loginDTO)
    {
        var user = await _userManager.FindByEmailAsync(loginDTO.Email);
        if (user == null) throw new Exception("Usuário não encontrado.");

        var result = await _signInManager.CheckPasswordSignInAsync(user, loginDTO.Password, false);
        if (!result.Succeeded) throw new Exception("Senha incorreta.");

        return "JWT AQUI";
    }

    public async Task<ApplicationUser> RegisterAsync(UserCreateDTO createDTO)
    {
        var user = new ApplicationUser
        {
            Id = Guid.NewGuid(),
            UserName = createDTO.Email,
            Email = createDTO.Email,
            FullName = createDTO.Name
        };

        var result = await _userManager.CreateAsync(user, createDTO.Password);

        return result.Succeeded ? user : null;
    }

    public async Task<bool> UpdateAsync(Guid id, UserUpdateDTO updateDTO)
    {
        var user = await _userManager.FindByIdAsync(id.ToString());
        if (user == null) throw new Exception("Usuário não encontrado.");

        user.FullName = updateDTO.Name;
        user.Email = updateDTO.Email;
        user.UserName = updateDTO.Email;

        var result = await _userManager.UpdateAsync(user);
        return result.Succeeded;
    }
}