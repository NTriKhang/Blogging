using Blogging.Common.Application.Messaging;
using Blogging.Common.Domain;
using Blogging.Modules.Blog.Application.Abtractions.Data;
using Blogging.Modules.Blog.Domain.Sections;
using Blogging.Modules.Blog.Domain.Users;

namespace Blogging.Modules.Blog.Application.Section.UpdateSection
{
    internal sealed record UpdateSectionCommandHandler(
        IUnitOfWork unitOfWork
        , ISectionRepository sectionRepository
        , IUserRepository userRepository) : ICommandHandler<UpdateSectionCommand>
    {
        public async Task<Result> Handle(UpdateSectionCommand request, CancellationToken cancellationToken)
        {
            Task<Domain.Sections.Section?> sectionTask = sectionRepository.GetByIdAsync(request.Id);
            Task<Domain.Users.User?> userTask = userRepository.GetByIdAsync(request.UserId);

            await Task.WhenAll([sectionTask, userTask]);

            var section = sectionTask.Result;
            var user = userTask.Result;

            if (section is null)
                return Result.Failure(SectionErrors.NotFound(request.Id));

            if(user is null)
                return Result.Failure(UserErrors.NotFound(request.UserId));

            section.Update(request.UserId, request.Title, request.Content);
            await unitOfWork.SaveChangesAsync();

            return Result.Success();
        }
    }
}
