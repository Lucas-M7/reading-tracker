
using System.Security.Claims;

namespace ReadingTracker.API.Services.Auth;

public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAcessor;
    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAcessor = httpContextAccessor;
    }

    public Guid GetUserId()
    {
        var userIdString = _httpContextAcessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrWhiteSpace(userIdString)) throw new InvalidOperationException("Usuário não autenticado");
        return Guid.Parse(userIdString);
    }
}