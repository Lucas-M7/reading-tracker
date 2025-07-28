namespace ReadingTracker.API.Dtos.User.Dto;

public class UserReadDTO
{
    public int UserId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}