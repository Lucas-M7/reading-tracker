using ReadingTracker.API.Enums;

namespace ReadingTracker.API.Dtos.ReadingDTO;

public class ReadingCreateDTO
{
    public int StartPage { get; set; }
    public int EndPage { get; set; }
    public ReadingStatus ReadingStatus { get; set; } = ReadingStatus.NotStarted;
    public Guid BookId { get; set; }
    public Guid UserId { get; set; }

}