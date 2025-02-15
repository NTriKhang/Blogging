using Blogging.Common.Application.Messaging;
using Blogging.Common.Domain;
using Blogging.Modules.Blog.Application.Abtractions.Data;
using Blogging.Modules.Blog.Domain.Blogs;

namespace Blogging.Modules.Blog.Application.Blogs.PublishBlog
{
    internal sealed class PublishBlogCommandHandler(IBlogRepository blogRepository
        , IUnitOfWork unitOfWork) 
        : ICommandHandler<PublishBlogCommand>
    {
        public async Task<Result> Handle(PublishBlogCommand request, CancellationToken cancellationToken)
        {
            var blog = await blogRepository.GetByIdAsync(request.Id, cancellationToken);
            if(blog == null) 
                return Result.Failure(BlogErrors.NotFound(request.Id));

            var res = blog.Publish();
            if (res.IsFailure)
                return res;

            await unitOfWork.SaveChangesAsync();

            return res;
        }
    }
}
