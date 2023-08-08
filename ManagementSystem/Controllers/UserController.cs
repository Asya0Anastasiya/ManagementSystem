using ManagementSystem.Entities;
using ManagementSystem.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;

namespace ManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUser iUser;
        public UserController(IUser user)
        {
            iUser = user;
        }

        [HttpGet]
        //[Authorize(Roles = "Admin, Employee")]
        public IActionResult GetAll() 
        { 
            return Ok(iUser.GetAll());
        }

        [Route("CreateIdentity")]
        [HttpPost]
        public async Task<IActionResult> CreateIdentity(SignUpEntity userLogin)
        {
            if (ModelState.IsValid)
            {
                var result = await iUser.CreateIdentity(userLogin);
                if (result.Succeeded)
                {
                    return Ok(result);
                }
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
            }
            return BadRequest("Invalid data");
        }

        [Route("logout")]
        [HttpPost]
        public async Task<IActionResult> LogOut()
        {
            await iUser.SignOutAsync();
            return Ok("Successfull logout");
        }

        [Route("change-password")]
        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePassword changePassword)
        {
            if (ModelState.IsValid)
            {
                var result = await iUser.ChangePasswordAsync(changePassword);
                if (result.Succeeded)
                {
                    return Ok(result);
                }
            }
            return BadRequest("Error while changing password");
        }
    }
}
