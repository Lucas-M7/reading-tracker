using ReadingTracker.API.Dtos.UserDTO;

namespace ReadingTracker.API.Services.Interfaces;

public interface IUserService
{
    public Task<UserReadDTO?> GetByIdAsync(Guid id);
    Task<IEnumerable<UserReadDTO>> GetAllsAsync();

    Task<UserCreateDTO> CreateAsync(UserCreateDTO createDTO);
    Task<UserUpdateDTO> UpdateAsync(Guid id, UserUpdateDTO updateDTO);
    Task<bool> DeleteAsync(Guid id);
}