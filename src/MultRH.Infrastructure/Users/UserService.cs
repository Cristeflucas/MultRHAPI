using AutoMapper;
using Microsoft.AspNetCore.Identity;
using MultRH.Application.Users;
using MultRH.Application.Users.Dtos;
using MultRH.Domain.Entities;
using MultRH.Domain.Enums;

namespace MultRH.Infrastructure.Users
{
    public class UserService : IUserService
    {
        private IMapper _mapper;
        private UserManager<User> _userManager;
        private SignInManager<User> _signInManager;
        private ITokenService _tokenService;

        public UserService(IMapper mapper, UserManager<User> userManager, SignInManager<User> signInManager, ITokenService tokenService)
        {
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
        }

        public async Task Register(CreateUserDto dto)
        {
            User user = _mapper.Map<User>(dto);
            user.UserName = dto.Email;
            user.Email = dto.Email;
            user.Role = UserRole.Candidate;

            IdentityResult result = await _userManager.CreateAsync(user, dto.Password);
            if (!result.Succeeded)
            {
                throw new ApplicationException("Falha ao criar usuário");
            }
        }

        public async Task<string> Login(LoginUserDto dto)
        {
            var result = await _signInManager.PasswordSignInAsync(dto.Email, dto.Password, false, false);
            if (!result.Succeeded)
            {
                throw new ApplicationException("Usuário não autenticado!");
            }
            var user = _signInManager.UserManager.Users.FirstOrDefault(u => u.NormalizedEmail == dto.Email.ToUpper());

            var token = _tokenService.GenerateToken(user);

            return token;
        }

        public async Task<bool> SetPremium(string userId, bool isPremium)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user is null)
            {
                return false;
            }

            user.IsPremium = isPremium;
            var result = await _userManager.UpdateAsync(user);
            return result.Succeeded;
        }
    }
}
