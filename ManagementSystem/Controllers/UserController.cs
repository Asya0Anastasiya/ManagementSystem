using ManagementSystem.Interfaces.Services;
using ManagementSystem.Models.UserModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ManagementSystem.Controllers
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
        public IActionResult ChangePassword(Guid id, string oldPassword, string newPassword)
        {
            userService.ChangePassword(id, oldPassword, newPassword);
            return Ok();
        }


        [HttpPost("signin")]
        public IActionResult Login([FromBody] SignInModel signInModel)
        {
            string token = userService.Login(signInModel);
            return Ok(new
            {
                Token = token,
                Message = "Login ok"
            });
        }
    }
}
