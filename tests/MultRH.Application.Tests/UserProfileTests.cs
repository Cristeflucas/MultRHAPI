using AutoMapper;
using MultRH.Application.Users.Dtos;
using MultRH.Application.Users.Profiles;
using MultRH.Domain.Entities;
using Xunit;

namespace MultRH.Application.Tests
{
    public class UserProfileTests
    {
        private readonly IMapper _mapper;

        public UserProfileTests()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<UserProfile>());
            _mapper = config.CreateMapper();
        }

        [Fact]
        public void Map_CreateUserDtoParaUser()
        {
            var dto = new CreateUserDto
            {
                FullName = "João Silva",
                Email = "joao.silva@example.com",
                Password = "Joao123!",
                ConfirmPassword = "Joao123!",
                Cpf = "112.219.584-29"
            };
            var user = _mapper.Map<User>(dto);

            Assert.Equal(dto.FullName, user.FullName);
            Assert.Equal(dto.Email, user.Email);
            Assert.Equal(dto.Cpf, user.Cpf);
        }
    }
}
