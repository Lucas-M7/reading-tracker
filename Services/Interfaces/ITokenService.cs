using ReadingTracker.API.Entities.Identity;

namespace ReadingTracker.API.Services.Interfaces;

public interface ITokenService
{
    string GenerateToken(ApplicationUser user);
}