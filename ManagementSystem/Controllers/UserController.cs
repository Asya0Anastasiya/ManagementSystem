using UserService.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using UserService.Models.UserDto;
using UserService.Helpers.Pagination;
using UserService.Helpers;
using Newtonsoft.Json;
using UserService.Models.TokenDto;
using UserService.Models.Entities;

namespace UserService.Controllers
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

        [HttpGet("getUsers/pageNumber/{pageNumber}/pageSize/{pageSize}")]
        public async Task<IActionResult> GetUsersAsync([FromQuery] FilteringParameters parameters, int pageNumber, int pageSize)
        {
            var totalData = await _userService.GetUsersCountAsync();
            var users = await _userService.GetUsersAsync(parameters, pageNumber, pageSize);
            var metadata = new
            {
                totalData,
            };
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
            return Ok(users);
        }

        [HttpPut("changePassword")]
        public async Task<IActionResult> ChangePasswordAsync([FromBody] ChangePasswordModel model)
        {
            await _userService.ChangePassword(model.Id, model.OldPassword, model.NewPassword);
            return Ok();
        }


        [HttpPost("signin")]
        public async Task<IActionResult> LoginAsync([FromBody] SignInModel signInModel)
        {
            Tokens tokens = await _userService.Login(signInModel);
            return Ok(new
            {
                Token = tokens.Token,
                RefreshToken = tokens.RefreshToken,
            });
        }

        [HttpGet]
        [Route("getUser/{id}")]
        public async Task<IActionResult> GetUserInfoAsync([FromRoute] Guid id)
        {
            return Ok(await _userService.GetUserInfo(id));
        }

        [HttpDelete]
        [Route("removeUser/{id}")]
        public async Task<IActionResult> DeleteUserAsync(Guid id)
        {
            await _userService.DeleteUserAsync(id);
            // return 204?
            return Ok();
        }

        [HttpPut]
        [Route("updateUser")]
        public async Task<IActionResult> UpdateUserAsync([FromBody] UpdateUserModel model)
        {
            await _userService.UpdateUserAsync(model);
            return Ok();
        }

        [HttpPost]
        [Route("setUserImage/{userId}")]
        public async Task<IActionResult> SetUserImageAsync(Guid userId, IFormFile file)
        {
            await _userService.SetUserImageAsync(userId, file);
            return Ok();
        }

        [HttpGet]
        [Route("getUserImage/{userId}")]
        public async Task<IActionResult> GetUserImageAsync(Guid userId)
        {
            
            byte[] imageData = await _userService.GetUserImageAsync(userId);
            return File(imageData, "image/png");
        }

        [HttpPost("refreshTokenVerification")]
        public async Task<IActionResult> RefreshTokenVerification([FromHeader] string refreshToken)
        {
            return Ok(await _userService.ValidateRefreshTokenAsync(refreshToken));
        }
    }
}
