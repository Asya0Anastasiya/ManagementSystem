using MediatR;
using UserService.Interfaces.Services;
using UserService.Models.TokenDto;

namespace UserService.MediatR.Commands
{
    public class ValidateRefreshTokenCommand : IRequest<Tokens>
    {
        public string RefreshToken { get; set; }

        public ValidateRefreshTokenCommand(string refreshToken)
        {
            RefreshToken = refreshToken;
        }
    }

    public class ValidateRefreshTokenHandler : IRequestHandler<ValidateRefreshTokenCommand, Tokens>
    {
        private readonly IUserService _userService;

        public ValidateRefreshTokenHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<Tokens> Handle(ValidateRefreshTokenCommand request, CancellationToken cancellationToken)
        {
            return await _userService.ValidateRefreshTokenAsync(request.RefreshToken);
        }
    }
}
