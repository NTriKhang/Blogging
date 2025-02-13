using Blogging.Common.Application.Messaging;
using Blogging.Common.Domain;
using Blogging.Modules.Blog.Application.Abtractions.Data;
using Blogging.Modules.Blog.Domain.Users;

namespace Blogging.Modules.Blog.Application.Users.CreateReader
{
    internal sealed class CreateUserCommandHandler(
        IUserRepository userRepository
        , IUnitOfWork unitOfWork) : ICommandHandler<CreateUserCommand, Guid>
    {
        public async Task<Result<Guid>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var reader = User.Create(request.Id
                , request.UserName
                , request.DisplayName
                , request.Email
                , request.ImageUrl);
            
            userRepository.Insert(reader);
            await unitOfWork.SaveChangesAsync();

            return reader.Id;
        }
    }
}
