using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.Interfaces.Services;
using UserService.MediatR.Commands;
using UserService.MediatR.Handlers;
using UserService.MediatR.Queries;
using UserService.Models.UserDto;
using Xunit;

namespace Tests.Handlers
{
    public class UsersHandlersTests
    {
        private readonly Mock<IUserService> _userService;

        public UsersHandlersTests()
        {
            _userService = new Mock<IUserService>();
        }

        [Fact]
        public async Task GetUsersCountHandler_ReturnsInt()
        {
            GetUsersCountHandler _getUsersCountHandler = new(_userService.Object);
            var result = await _getUsersCountHandler.Handle(new GetUsersCountQuery(), CancellationToken.None);

            Assert.IsType<int>(result);
        }

        [Fact]
        public async Task LoginHandler_ReturnError()
        {
            LoginHandler _loginHandler = new(_userService.Object);
            SignInModel signInModel = new SignInModel()
            {
                Email = "wrongEmail",
                Password = "wrongPassword"
            };
            var result = await _loginHandler.Handle(new LoginCommand(signInModel), CancellationToken.None);
            Assert.True(result == null);
        }
    }
}
