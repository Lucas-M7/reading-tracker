using ReadingTracker.API.Entities;
using ReadingTracker.API.Entities.Identity;

namespace ReadingTracker.API.Repositories.Interfaces;

public interface IUserRepository
{
    Task<List<ApplicationUser>> GetAllAsync();
    Task<ApplicationUser?> GetByIdAsync(Guid id);
    Task<ApplicationUser> CreateAsync(ApplicationUser user);
    Task<ApplicationUser> UpdateAsync(ApplicationUser user);
    Task<bool> DeleteAsync(Guid id);
}