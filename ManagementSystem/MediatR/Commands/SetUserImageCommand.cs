using MediatR;

namespace UserService.MediatR.Commands
{
    public class SetUserImageCommand : IRequest
    {
        public Guid UserId { get; }

        public IFormFile File { get; }

        public SetUserImageCommand(Guid userId, IFormFile file)
        {
            UserId = userId;
            File = file;
        }

    }
}
