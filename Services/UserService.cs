using AutoMapper;
using Microsoft.AspNetCore.Identity;
using ReadingTracker.API.Dtos.UserDTO;
using ReadingTracker.API.Entities.Identity;
using ReadingTracker.API.Services.Interfaces;

namespace ReadingTracker.API.Services;

public class UserService : IUserService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IMapper _mapper;
    private readonly ITokenService _tokenService;
    private readonly ILogger<UserService> _logger;

    public UserService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,
        IMapper mapper, ITokenService tokenService, ILogger<UserService> logger)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _mapper = mapper;
        _tokenService = tokenService;
        _logger = logger;
    }

    public async Task<UserReadDTO> RegisterAsync(UserRegisterDTO registerDTO)
    {
        var existingUser = await _userManager.FindByEmailAsync(registerDTO.Email);
        if (existingUser != null)
        {
            throw new ArgumentException("Este email já está em uso.");
        }

        var user = _mapper.Map<ApplicationUser>(registerDTO);
        user.UserName = registerDTO.Email;

        var result = await _userManager.CreateAsync(user, registerDTO.Password);

        if (!result.Succeeded)
        {
            // Concatena erros de validação do Identity
            var erros = string.Join("\n", result.Errors.Select(e => e.Description));
            throw new InvalidOperationException($"Falha ao registrar usuário: {erros}");
        }

        _logger.LogInformation("Novo usuário registrado: {Email}", user.Email);

        var userReadDto = _mapper.Map<UserReadDTO>(user);

        userReadDto.Token = _tokenService.GenerateToken(user);

        return userReadDto;
    }

    public async Task<UserReadDTO?> LoginAsync(UserLoginDTO loginDTO)
    {
        var user = await _userManager.FindByEmailAsync(loginDTO.Email);
        if (user == null) return null; // usuário não encontrado

        var result = await _signInManager.CheckPasswordSignInAsync(user, loginDTO.Password, lockoutOnFailure: false);

        if (!result.Succeeded) return null; // senha incorreta

        _logger.LogInformation("Usuário logado com sucesso: {Email}", user.Email);

        var userReadDto = _mapper.Map<UserReadDTO>(user);
        userReadDto.Token = _tokenService.GenerateToken(user);

        return userReadDto;
    }

    public async Task<UserReadDTO?> UpdateAsync(Guid id, UserUpdateDTO updateDTO)
    {
        var user = await _userManager.FindByIdAsync(id.ToString());
        if (user == null) return null;

        user.FullName = updateDTO.FullName ?? user.FullName;

        if (!string.IsNullOrEmpty(updateDTO.Email) && user.Email != updateDTO.Email)
        {
            await _userManager.SetEmailAsync(user, updateDTO.Email);
            await _userManager.SetUserNameAsync(user, updateDTO.Email);
        }

        await _userManager.UpdateAsync(user);
        _logger.LogInformation("Usuário atualizado: {UserId}", id);

        return _mapper.Map<UserReadDTO>(user);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var user = await _userManager.FindByIdAsync(id.ToString());
        if (user == null) return false;

        var result = await _userManager.DeleteAsync(user);

        if (result.Succeeded)
        {
            _logger.LogInformation("Usuário deletado: {UserId}", id);
        }

        return result.Succeeded;
    }
}