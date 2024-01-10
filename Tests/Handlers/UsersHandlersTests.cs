using Moq;
using UserService.Interfaces.Services;
using UserService.MediatR.Commands;
using UserService.Models.UserDto;

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
