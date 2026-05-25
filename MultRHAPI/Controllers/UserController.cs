using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultRHAPI.Data.Dtos;
using MultRHAPI.Services;

namespace MultRHAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
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
    }
}
