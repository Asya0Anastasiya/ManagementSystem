using UserServiceAPI.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserServiceAPI.Models.UserDto;
using UserServiceAPI.Helpers;
using UserServiceAPI.Helpers.Pagination;

namespace UserServiceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateUserAsync([FromBody] SignUpModel signUpModel)
        {
            await _userService.Create(signUpModel);
            return Ok();
        }

        [Authorize]
        [HttpGet("getUsers/pageNumber/{pageNumber}/pageSize/{pageSize}")]
        public async Task<IActionResult> GetUsersAsync([FromQuery] FilteringParameters parameters, int pageNumber, int pageSize) 
        {
            var pagination = new PaginationParameters(pageNumber, pageSize);

            return Ok(await _userService.GetUsersAsync(parameters, pagination));
        }

        [HttpPut("changePassword")]
        public async Task<IActionResult> ChangePasswordAsync(Guid id, string oldPassword, string newPassword)
        {
            await _userService.ChangePassword(id, oldPassword, newPassword);
            return Ok();
        }


        [HttpPost("signin")]
        public async Task<IActionResult> LoginAsync([FromBody] SignInModel signInModel)
        {
            string token = await _userService.Login(signInModel);
            return Ok(new
            {
                Token = token,
            });
        }

        [HttpGet]
        [Route("getUser/{id}/{month}")]
        // получать месяц с UI
        public async Task<IActionResult> GetUserInfo([FromRoute] Guid id, int month)
        {
            return Ok(await _userService.GetUserInfo(id, month));
        }

        [HttpDelete]
        [Route("removeUser/{id}")]
        public async Task<IActionResult> DeleteUserAsync(Guid id)
        {
            await _userService.DeleteUserAsync(id);
            return Ok();
        }

        [HttpPut]
        [Route("updateUser/{id}")]
        public async Task<IActionResult> UpdateUserAsync([FromBody] UserInfoModel model)
        {
            // ne rabotajet, prichodit ne ta model
            await _userService.UpdateUserAsync(model);
            return Ok();
        }
    }
}
