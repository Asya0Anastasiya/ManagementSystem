using UserService.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using UserService.Models.UserDto;
using UserService.Helpers.Pagination;
using UserService.Helpers;
using Newtonsoft.Json;

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
            var pagination = new PaginationParameters(pageNumber, pageSize);
            var users = await _userService.GetUsersAsync(parameters, pagination);
            var metadata = new
            {
                totalData,
                pageSize,
                pageNumber
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
            string token = await _userService.Login(signInModel);
            return Ok(new
            {
                Token = token,
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
            // "наверное нам не надо полностью удалять пользователя".
            // Помню, но это, наверное, потом надо добавить "активный чи не"
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
    }
}
