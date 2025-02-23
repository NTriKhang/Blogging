using Blogging.Common.Application.Messaging;
using Blogging.Common.Domain;
using Blogging.Modules.Blog.Application.Abtractions.Data;
using Blogging.Modules.Blog.Domain.Blogs;
using Blogging.Modules.Blog.Domain.Sections;
using Blogging.Modules.Blog.Domain.Users;

namespace Blogging.Modules.Blog.Application.Section.CreateSection
{
    internal sealed record CreateSectionCommandHandler(
        IUnitOfWork unitOfWork
        , IUserRepository userRepository
        , ISectionRepository sectionRepository
        , IBlogRepository blogRepository) : ICommandHandler<CreateSectionCommand, Guid>
    {
        public async Task<Result<Guid>> Handle(CreateSectionCommand request, CancellationToken cancellationToken)
        {
            var blogTask = blogRepository.GetByIdAsync(request.BlogId);
            var userTask = userRepository.GetByIdAsync(request.UserId);

            await Task.WhenAll([blogTask, userTask]);
            var blog = blogTask.Result;
            var user = userTask.Result;

            if (blog is null)
                return Result.Failure<Guid>(BlogErrors.NotFound(request.BlogId));

            if(user is null)
                return Result.Failure<Guid>(UserErrors.NotFound(request.UserId));

            var section = Domain.Sections.Section.Create(request.BlogId
                , request.UserId
                , request.Title
                , request.Content
                , request.Order);

            sectionRepository.Insert(section);
            await unitOfWork.SaveChangesAsync();

            return section.Id;
        }
    }
}
