using ManagementSystem.Interfaces;
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
        public IActionResult Create([FromBody] SignUpModel signUpModel)
        {
            try
            {
                //Qwerty1234%
                userService.Create(signUpModel);
                return Ok();
            }
            catch (Exception ex)
            {

                return BadRequest(new
                {
                    Message = ex.Message
                });
            }
            
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetUsers() 
        { 
            return Ok(userService.GetUsers());
        }

        [HttpPut("change-password")]
        public IActionResult ChangePassword(string token, string oldPassword, string newPassword)
        {
            var result = userService.ChangePassword(token, oldPassword, newPassword);
            if (result) return Ok();
            return BadRequest("Wrong password");
        }


        [HttpPost("signin")]
        public IActionResult Login([FromBody] SignInModel signInModel)
        {
            try
            {
                string token = userService.Login(signInModel);
                return Ok(new
                {
                    Token = token,
                    Message = "Login ok"
                }) ;
            }
            catch (Exception ex)
            {

                return BadRequest(new
                {
                    Message = ex.Message
                });
            }
            //string token = userService.Login(signInModel);
            //if (!string.IsNullOrEmpty(token))
            //{
            //    return Ok(new
            //    {
            //        Message = "Successfully login"
            //    });
            //}
            //return BadRequest(new
            //{
            //    Message = "Bad Credentials"
            //});
        }
    }
}
