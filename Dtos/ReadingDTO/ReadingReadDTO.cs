namespace ReadingTracker.API.Dtos.ReadingDTO;

public class ReadingReadDTO
{
    public int ReadingId { get; set; }
    public int StartPage { get; set; }
    public int EndPage { get; set; }
    public int BookId { get; set; }
    public int UserId { get; set; }
}