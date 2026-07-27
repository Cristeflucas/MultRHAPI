using AutoMapper;
using MultRH.Application.Users.Dtos;
using MultRH.Domain.Entities;

namespace MultRH.Application.Users.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<CreateUserDto, User>();
        }
    }
}
