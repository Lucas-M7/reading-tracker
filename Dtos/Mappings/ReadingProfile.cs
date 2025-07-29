using AutoMapper;
using ReadingTracker.API.Dtos.ReadingDTO;
using ReadingTracker.API.Entities;

namespace ReadingTracker.API.Dtos.Mappings;

public class ReadingProfile : Profile
{
    public ReadingProfile()
    {
        CreateMap<Reading, ReadingReadDTO>();
        CreateMap<ReadingCreateDTO, Reading>();
        CreateMap<ReadingUpdateDTO, Reading>();
    }
}