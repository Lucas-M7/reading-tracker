namespace ReadingTracker.API.Dtos.Reading.Dto;

public class ReadingCreateDTO
{
    public int StartPage { get; set; }
    public int EndPage { get; set; }
    public int BookId { get; set; }
    public int UserId { get; set; }

}