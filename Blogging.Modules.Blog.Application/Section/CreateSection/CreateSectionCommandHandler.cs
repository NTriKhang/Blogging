using Blogging.Common.Application.Messaging;
using Blogging.Common.Domain;
using Blogging.Modules.Blog.Application.Abtractions.Data;
using Blogging.Modules.Blog.Domain.Blogs;
using Blogging.Modules.Blog.Domain.Sections;
using Blogging.Modules.Blog.Domain.Users;

namespace Blogging.Modules.Blog.Application.Section.CreateSection
{
    internal sealed record CreateSectionCommandHandler(
        IUnitOfWork unitOfWork,
        IUserRepository userRepository,
        ISectionRepository sectionRepository,
        IBlogRepository blogRepository) : ICommandHandler<CreateSectionCommand, Guid>
    {
        public async Task<Result<Guid>> Handle(CreateSectionCommand request, CancellationToken cancellationToken)
        {
            var blog = await blogRepository.GetByIdAsync(request.BlogId, cancellationToken: cancellationToken);
            var user = await userRepository.GetByIdAsync(request.UserId, cancellationToken: cancellationToken);

            if (blog is null)
                return Result.Failure<Guid>(BlogErrors.NotFound(request.BlogId));

            if (user is null)
                return Result.Failure<Guid>(UserErrors.NotFound(request.UserId));

            var section = Domain.Sections.Section.Create(
                request.BlogId,
                request.UserId,
                request.Title,
                request.Content);

            sectionRepository.Insert(section);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return section.Id;
        }
    }
}
