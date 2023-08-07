using ManagementSystem.Entities;
using ManagementSystem.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        [Authorize(Roles = "Administrator, Employee")]
        public IActionResult GetAll() 
        { 
            return Ok(iUser.GetAll());
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public IActionResult Create([FromBody] UserEntity userEntity) 
        { 
            iUser.Create(userEntity);
            return Ok(userEntity);
        }


        //[HttpGet]
        //public IActionResult Public()
        //{
        //    return Ok("This is public method");
        //}

        //[HttpGet("Admins")]
        //[Authorize(Roles = "Administrator")]
        //public IActionResult Private()
        //{
        //    return Ok("Private message");
        //}
    }
}
