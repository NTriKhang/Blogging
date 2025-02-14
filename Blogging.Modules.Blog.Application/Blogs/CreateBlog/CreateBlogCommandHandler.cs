using Blogging.Common.Application.Messaging;
using Blogging.Common.Domain;
using Blogging.Modules.Blog.Application.Abtractions.Data;
using Blogging.Modules.Blog.Domain.Blogs;
using Blogging.Modules.Blog.Domain.Users;

namespace Blogging.Modules.Blog.Application.Blogs.CreateBlog
{
    internal sealed class CreateBlogCommandHandler(
        IUnitOfWork unitOfWork
        , IUserRepository userRepository
        , IBlogRepository blogRepository) : ICommandHandler<CreateBlogCommand, Guid>
    {
        public async Task<Result<Guid>> Handle(CreateBlogCommand request, CancellationToken cancellationToken)
        {
            var user = await userRepository.GetByIdAsync(request.UserId);
            if (user is null)
            {
                return Result.Failure<Guid>(UserErrors.NotFound(request.UserId));
            }
            var blog = Domain.Blogs.Blog.Create(
                request.UserId
                , request.Title
                , request.Description
                , request.ThumbnailUrl);

            blogRepository.Insert(blog);
            await unitOfWork.SaveChangesAsync();

            return blog.Id;
        }
    }
}
