using AutoMapper;
using ReadingTracker.API.Dtos.UserDTO;
using ReadingTracker.API.Entities;
using ReadingTracker.API.Entities.Identity;

namespace ReadingTracker.API.Dtos.Mappings;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<ApplicationUser, UserReadDTO>()
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Id));
        CreateMap<UserRegisterDTO, ApplicationUser>();
        CreateMap<UserUpdateDTO, ApplicationUser>();

    }
}