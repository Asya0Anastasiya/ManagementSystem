using Microsoft.AspNetCore.Mvc;
using UserService.Models.UserDto;
using UserService.Helpers;
using Newtonsoft.Json;
using UserService.Models.TokenDto;
using UserService.Models.Entities;
using UserService.Models.TokenDto;
using UserService.Models.Entities;
using MediatR;
using UserService.MediatR.Queries;
using UserService.MediatR.Commands;
using MediatR;
using UserService.MediatR.Queries;
using UserService.MediatR.Commands;

namespace UserService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateUserAsync([FromBody] SignUpModel signUpModel)
        {
            await _mediator.Send(new CreateUserCommand(signUpModel));
            return Ok();
        }

        [HttpGet("getUsers/pageNumber/{pageNumber}/pageSize/{pageSize}")]
        public async Task<IActionResult> GetUsersAsync([FromQuery] FilteringParameters parameters, int pageNumber, int pageSize)
        {
            var result = await _mediator.Send(new GetUserInfoListQuery(parameters, pageNumber, pageSize));
            return Ok(result);
        }

        [HttpPut("changePassword")]
        public async Task<IActionResult> ChangePasswordAsync([FromBody] ChangePasswordModel model)
        {
            await _mediator.Send(new ChangePasswordCommand(model));
            return Ok();
        }


        [HttpPost("signin")]
        public async Task<IActionResult> LoginAsync([FromBody] SignInModel signInModel)
        {
            Tokens tokens = await _userService.Login(signInModel);
            string token = await _mediator.Send(new LoginCommand(signInModel));
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
            return Ok(await _mediator.Send(new GetUserByIdQuery(id)));
        }

        [HttpDelete]
        [Route("removeUser/{id}")]
        public async Task<IActionResult> DeleteUserAsync(Guid id)
        {
            await _mediator.Send(new DeleteUserCommand(id));
            // return 204?
            return Ok();
        }

        [HttpPut]
        [Route("updateUser")]
        public async Task<IActionResult> UpdateUserAsync([FromBody] UpdateUserModel model)
        {
            await _mediator.Send(new UpdateUserCommand(model));
            return Ok();
        }

        [HttpPost]
        [Route("setUserImage/{userId}")]
        public async Task<IActionResult> SetUserImageAsync(Guid userId, IFormFile file)
        {
            await _mediator.Send(new SetUserImageCommand(userId, file));
            return Ok();
        }

        [HttpGet]
        [Route("getUserImage/{userId}")]
        public async Task<IActionResult> GetUserImageAsync(Guid userId)
        {            
            byte[] imageData = await _mediator.Send(new GetUserImageQuery(userId));
            return File(imageData, "image/png");
        }

        [HttpPost("refreshTokenVerification")]
        public async Task<IActionResult> RefreshTokenVerification([FromHeader] string refreshToken)
        {
            return Ok(await _userService.ValidateRefreshTokenAsync(refreshToken));
        }
    }
}
