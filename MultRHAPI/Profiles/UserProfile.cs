using AutoMapper;
using MultRHAPI.Data.Dtos;
using MultRHAPI.Models;

namespace MultRHAPI.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<CreateUserDto, User>();
        }
    }
}
