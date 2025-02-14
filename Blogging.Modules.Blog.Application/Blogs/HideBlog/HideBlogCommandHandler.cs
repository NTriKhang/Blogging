using Blogging.Common.Application.Messaging;
using Blogging.Common.Domain;
using Blogging.Modules.Blog.Application.Abtractions.Data;
using Blogging.Modules.Blog.Domain.Blogs;

namespace Blogging.Modules.Blog.Application.Blogs.HideBlog
{
    internal sealed class HideBlogCommandHandler(
    IBlogRepository blogRepository
    , IUnitOfWork unitOfWork) : ICommandHandler<HideBlogCommand>
    {
        public async Task<Result> Handle(HideBlogCommand request, CancellationToken cancellationToken)
        {
            var blog = await blogRepository.GetByIdAsync(request.Id, cancellationToken);
            if (blog == null)
                return Result.Failure(BlogErrors.NotFound(request.Id));

            blog.Hide();
            await unitOfWork.SaveChangesAsync();

            return Result.Success();
        }
    }
}
