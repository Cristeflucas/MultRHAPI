using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using MultRH.Application.Users;
using MultRH.Application.Users.Dtos;

namespace MultRHAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        [EnableRateLimiting("auth")]
        public async Task<IActionResult> CreateUser(CreateUserDto dto)
        {
            try
            {
                await _userService.Register(dto);
                return Ok("Usuário criado com sucesso");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("login")]
        [EnableRateLimiting("auth")]
        public async Task<IActionResult> Login(LoginUserDto dto)
        {
            try
            {
                var token = await _userService.Login(dto);
                return Ok(token);
            }
            catch (ApplicationException ex)
            {
                return Unauthorized(ex.Message);
            }
        }
    }
}
