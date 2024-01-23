using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Tests.MockData;
using UserService.Controllers;
using UserService.Helpers;
using UserService.Helpers.Pagination;
using UserService.MediatR.Queries;
using UserService.Models.UserDto;

namespace Tests.Controller
{
    public class UsersControllerTests
    {
        private readonly Mock<IMediator> _mediator;
        private readonly UserController _userController;

        public UsersControllerTests()
        {
            _mediator = new Mock<IMediator>();
            _userController = new(_mediator.Object);
        }

        [Fact]
        public async Task GetUserInfoAsync_ReturnsOk200WithObj()
        {
            var result = await _userController.GetUserInfoAsync(new Guid("c5842e31-2f98-409b-2cd6-08dbbf946b0b"));
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task CreateUserAsync_ReturnsOk200()
        {
            var signUpModel = new SignUpModel()
            {
                Firstname = "Bill",
                Lastname = "Gates",
                Email = "Bill.G@gmail.com",
                Password = "qwerty1!",
                PhoneNumber = "1234567890",
                PositionId = new Guid("c5842e31-2f98-409b-2cd6-08dbbf946b0b")
            };

            var result = await _userController.CreateUserAsync(signUpModel);
            Assert.IsType<OkResult>(result);
        }
    }
}
