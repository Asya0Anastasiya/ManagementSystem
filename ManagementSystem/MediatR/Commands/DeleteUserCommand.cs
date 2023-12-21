using MediatR;

namespace UserService.MediatR.Commands
{
    public class DeleteUserCommand : IRequest
    {
        public Guid Id { get; }

        public DeleteUserCommand(Guid id)
        {
            Id = id;
        }
    }
}
