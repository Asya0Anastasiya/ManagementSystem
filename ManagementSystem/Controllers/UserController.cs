using UserServiceAPI.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserServiceAPI.Models.UserDto;

namespace UserServiceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;
        public UserController(IUserService _userService)
        {
            userService = _userService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateUserAsync([FromBody] SignUpModel signUpModel)
        {
            await userService.Create(signUpModel);
            return Ok();
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetUsersAsync() 
        { 
            return Ok(await userService.GetUsersAsync());
        }

        [HttpPut("changePassword")]
        public async Task<IActionResult> ChangePasswordAsync(Guid id, string oldPassword, string newPassword)
        {
            await userService.ChangePassword(id, oldPassword, newPassword);
            return Ok();
        }


        [HttpPost("signin")]
        public async Task<IActionResult> Login([FromBody] SignInModel signInModel)
        {
            string token = await userService.Login(signInModel);
            return Ok(new
            {
                Token = token,
                Message = "Login ok"
            });
        }

        [HttpGet] // спросить про передачу id на фронт в UserInfoModel
        [Route("{email}")]
        // получать месяц с UI
        public async Task<IActionResult> GetUserInfo([FromRoute] string email, int month)
        {
            return Ok(await userService.GetUserInfo(email, 12));
        }
    }
}
