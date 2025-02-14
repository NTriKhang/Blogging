using Blogging.Common.Application.Messaging;
using Blogging.Common.Domain;
using Blogging.Modules.Blog.Application.Abtractions.Data;
using Blogging.Modules.Blog.Domain.Blogs;

namespace Blogging.Modules.Blog.Application.Blogs.ModifyBlog
{
    internal sealed class ModifyBlogCommandHandler(
        IBlogRepository blogRepository
        , IUnitOfWork unitOfWork) : ICommandHandler<ModifyBlogCommand>
    {
        public async Task<Result> Handle(ModifyBlogCommand request, CancellationToken cancellationToken)
        {
            var blog = await blogRepository.GetByIdAsync(request.Id, cancellationToken);
            if (blog == null)
                return Result.Failure(BlogErrors.NotFound(request.Id));

            blog.Modify();
            await unitOfWork.SaveChangesAsync();

            return Result.Success();
        }
    }
}
