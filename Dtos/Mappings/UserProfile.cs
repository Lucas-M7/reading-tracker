using AutoMapper;
using ReadingTracker.API.Dtos.UserDTO;
using ReadingTracker.API.Entities;

namespace ReadingTracker.API.Dtos.Mappings;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserReadDTO>();
        CreateMap<UserCreateDTO, User>();
        CreateMap<UserUpdateDTO, User>();

    }
}