using ReadingTracker.API.Entities;

namespace ReadingTracker.API.Repositories.Interfaces;

public interface IReadingRepository
{
    Task<IEnumerable<Reading>> GetAllAsync();
    Task<Reading?> GetByIdAsync(Guid id);
    Task<Reading> CreateAsync(Reading reading);
    Task<Reading> UpdateAsync(Reading reading);
    Task<bool> DeleteAsync(Guid id);
}