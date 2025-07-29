namespace ReadingTracker.API.Dtos.UserDTO;

public class UserReadDTO
{
    public int UserId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}