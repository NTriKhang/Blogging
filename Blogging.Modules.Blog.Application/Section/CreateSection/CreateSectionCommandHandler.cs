using Blogging.Common.Application.Messaging;
using Blogging.Common.Domain;
using Blogging.Modules.Blog.Application.Abtractions.Data;
using Blogging.Modules.Blog.Domain.Blogs;
using Blogging.Modules.Blog.Domain.Sections;

namespace Blogging.Modules.Blog.Application.Section.CreateSection
{
    internal sealed record CreateSectionCommandHandler(
        IUnitOfWork unitOfWork
        , ISectionRepository sectionRepository
        , IBlogRepository blogRepository) : ICommandHandler<CreateSectionCommand, Guid>
    {
        public async Task<Result<Guid>> Handle(CreateSectionCommand request, CancellationToken cancellationToken)
        {
            var blog = await blogRepository.GetByIdAsync(request.BlogId);
            if (blog is null)
                return Result.Failure<Guid>(BlogErrors.NotFound(request.BlogId));

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
