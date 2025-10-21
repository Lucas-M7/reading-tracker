using Microsoft.AspNetCore.Mvc;
using ReadingTracker.API.Dtos.UserDTO;
using ReadingTracker.API.Services.Interfaces;

namespace ReadingTracker.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly ILogger<AuthController> _logger;

    public AuthController(IUserService userService, ILogger<AuthController> logger)
    {
        _userService = userService;
        _logger = logger;
    }

    [HttpPost("register")]
    [ProducesResponseType(typeof(UserRegistrationSuccessDTO), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register([FromBody] UserRegisterDTO registerDTO)
    {
        try
        {
            _logger.LogInformation("Tentativa de registro para o email: {Email}", registerDTO.Email);

            var newUser = await _userService.RegisterAsync(registerDTO);

            return CreatedAtAction(
                "GetMyProfile",
                "Users",
                new { },
                newUser);
        }
        catch (Exception ex) when (ex is ArgumentException || ex is InvalidOperationException)
        {
            _logger.LogWarning(ex, "Falha no registro: {Message}", ex.Message);
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPost("login")]
    [ProducesResponseType(typeof(UserReadDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Login([FromBody] UserLoginDTO loginDTO)
    {
        _logger.LogInformation("Tentativa de login para o email: {Email}", loginDTO.Email);

        var userDto = await _userService.LoginAsync(loginDTO);

        if (userDto == null)
        {
            _logger.LogWarning("Login falhou para o email: {Email}", loginDTO.Email);
            return Unauthorized(new { message = "Email ou senha inválidos." });
        }

        return Ok(userDto);
    }
}