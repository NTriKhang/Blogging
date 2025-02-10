using Blogging.Common.Application.Messaging;
using Blogging.Common.Domain;
using Blogging.Modules.User.Application.Abtractions.Data;
using Blogging.Modules.User.Domain.Users;

namespace Blogging.Modules.User.Application.Users.UpdatePassword
{
    internal sealed class UpdatePasswordCommandHandler(
        IUserRepository userRepository
        , IUnitOfWork unitOfWork) : ICommandHandler<UpdatePasswordCommand>
    {
        public async Task<Result> Handle(UpdatePasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await userRepository.GetByIdAsync(request.UserId);
            if (user is null)
            {
                return Result.Failure(UserErrors.NotFound(request.UserId));
            }

            user.UpdatePassword(request.Password);

            await unitOfWork.SaveChangesAsync();

            return Result.Success();
        }
    }
}
