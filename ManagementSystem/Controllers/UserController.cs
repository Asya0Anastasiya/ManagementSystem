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
        [Authorize(Roles = "Admin, Employee")]
        public IActionResult GetAll() 
        { 
            return Ok(iUser.GetAll());
        }

        [HttpPost]
        //[Authorize(Roles = "Admin")]
        public IActionResult Create([FromBody] UserEntity userEntity) 
        { 
            if (ModelState.IsValid)
            {
                iUser.Create(userEntity);
                return Ok(userEntity);
            }
            return BadRequest("Invalid user");
        }
    }
}
