using MultRH.Domain.Entities;

namespace MultRH.Application.Users
{
    public interface ITokenService
    {
        string GenerateToken(User user);
    }
}
