using MultRH.Application.Users.Dtos;

namespace MultRH.Application.Users
{
    public interface IUserService
    {
        Task Register(CreateUserDto dto);
        Task<string> Login(LoginUserDto dto);
        Task<bool> SetPremium(string userId, bool isPremium);
    }
}
