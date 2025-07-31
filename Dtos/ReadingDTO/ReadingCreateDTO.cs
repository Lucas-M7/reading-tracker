namespace ReadingTracker.API.Dtos.ReadingDTO;

public class ReadingCreateDTO
{
    public int StartPage { get; set; }
    public int EndPage { get; set; }
    public Guid BookId { get; set; }
    public Guid UserId { get; set; }

}