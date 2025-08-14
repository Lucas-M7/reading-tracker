using ReadingTracker.API.Dtos.ReadingDTO;

namespace ReadingTracker.API.Services.Interfaces;

public interface IReadingService
{
    Task<ReadingReadDTO> GetByIdAsync(Guid id);
    Task<IEnumerable<ReadingReadDTO>> GetByBookIdAsync(Guid bookId);
    Task<ReadingCreateDTO> CreateAsync(ReadingCreateDTO createDTO);
    Task<bool> UpdateProgressAsync(Guid id, int currentPage);
    Task<bool> MarkAsFinishedAsync(Guid id);
    Task<bool> DeleteAsync(Guid id);
}