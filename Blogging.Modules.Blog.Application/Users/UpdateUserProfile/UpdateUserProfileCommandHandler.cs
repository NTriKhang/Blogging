using Blogging.Common.Application.Messaging;
using Blogging.Common.Domain;
using Blogging.Modules.Blog.Application.Abtractions.Data;
using Blogging.Modules.Blog.Domain.Users;

namespace Blogging.Modules.Blog.Application.Users.UpdateUserProfile
{
    public sealed class UpdateUserProfileCommandHandler(
        IUnitOfWork unitOfWork
        , IUserRepository userRepository) : ICommandHandler<UpdateUserProfileCommand>
    {
        public async Task<Result> Handle(UpdateUserProfileCommand request, CancellationToken cancellationToken)
        {
            var user = await userRepository.GetByIdAsync(request.Id);
            if (user == null)
                return Result.Failure(Error.NotFound("Users.UpdateUserProfile", $"User with {request.Id} does not exists"));
            user.Update(request.UserName, request.DisplayName, request.Email, request.ImasgeUrl);

            await unitOfWork.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }
}
