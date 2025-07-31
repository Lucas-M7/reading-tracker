namespace ReadingTracker.API.Dtos.ReadingDTO;

public class ReadingReadDTO
{
    public Guid ReadingId { get; set; }
    public int StartPage { get; set; }
    public int EndPage { get; set; }
    public Guid BookId { get; set; }
    public Guid UserId { get; set; }
}