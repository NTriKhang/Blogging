using Blogging.Common.Application.Messaging;
using Blogging.Common.Domain;
using Blogging.Modules.User.Application.Abtractions.Data;
using Blogging.Modules.User.Domain.Users;

namespace Blogging.Modules.User.Application.Users.UpdateProfile
{
    internal sealed class UpdateProfileCommandHandler(
        IUserRepository userRepository
        , IUnitOfWork unitOfWork) : ICommandHandler<UpdateProfileCommand>
    {
        public async Task<Result> Handle(UpdateProfileCommand request, CancellationToken cancellationToken)
        {
            var user = await userRepository.GetByIdAsync(request.UserId);
            if(user is null)
            {
                return Result.Failure(UserErrors.NotFound(request.UserId));
            }

            user.UpdateProfile(request.DisplayName, request.UserName, request.Email, request.ImageUrl);

            await unitOfWork.SaveChangesAsync();

            return Result.Success();
        }
    }
}
