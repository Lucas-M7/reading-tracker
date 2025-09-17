using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReadingTracker.API.Dtos.UserDTO;
using ReadingTracker.API.Services.Auth;
using ReadingTracker.API.Services.Interfaces;

namespace ReadingTracker.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly ICurrentUserService _currentUser;
    private readonly ILogger<UsersController> _logger;

    public UsersController(IUserService userService, ICurrentUserService currentUser, ILogger<UsersController> logger)
    {
        _userService = userService;
        _currentUser = currentUser;
        _logger = logger;
    }

    // GET /api/user/me
    [HttpGet("me")]
    [ProducesResponseType(typeof(UserReadDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetMyProfile()
    {
        var userId = _currentUser.GetUserId();
        _logger.LogInformation("Buscando perfil para o usuário: {UserId}", userId);

        var user = await _userService.GetUserByIdAsync(userId);

        if (user == null)
        {
            return NotFound();
        }

        return Ok(user);
    }

    [HttpPut("me")]
    [ProducesResponseType(typeof(UserReadDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateMyProfile([FromBody] UserUpdateDTO updateDTO)
    {
        var userId = _currentUser.GetUserId();
        _logger.LogInformation("Atualizando perfil para o usuário: {UserId}", userId);

        var updatedUser = await _userService.UpdateAsync(userId, updateDTO);

        if (updatedUser == null)
        {
            return NotFound();
        }

        return Ok(updatedUser);
    }

    [HttpDelete("me")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteMyAccount()
    {
        var userId = _currentUser.GetUserId();
        _logger.LogInformation("Deletando conta do usuário: {UserId}", userId);

        var success = await _userService.DeleteAsync(userId);

        if (!success)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpPost("me/change-password")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ChangeMyPassword([FromBody] ChangePasswordDTO passwordDTO)
    {
        var userId = _currentUser.GetUserId();
        _logger.LogInformation("Tentativa de mudança de senha para o usuário: {UserId}", userId);

        var result = await _userService.ChangePasswordAsync(userId, passwordDTO);

        if (!result.Succeeded)
        {
            return BadRequest(new { errors = result.Errors });
        }

        return NoContent();
    }
}