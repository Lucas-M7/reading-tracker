using AutoMapper;
using ReadingTracker.API.Dtos.UserDTO;
using ReadingTracker.API.Entities;
using ReadingTracker.API.Entities.Identity;

namespace ReadingTracker.API.Dtos.Mappings;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<ApplicationUser, UserReadDTO>();
        CreateMap<UserRegisterDTO, ApplicationUser>();
        CreateMap<UserUpdateDTO, ApplicationUser>();

    }
}