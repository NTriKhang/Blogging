using Blogging.Common.Application.Messaging;
using Blogging.Common.Domain;
using Blogging.Modules.Blog.Application.Abtractions.Data;
using Blogging.Modules.Blog.Domain.Blogs;
using Blogging.Modules.Blog.Domain.Users;

namespace Blogging.Modules.Blog.Application.Blogs.RemoveBlogContributor
{
    internal sealed class RemoveBlogContributorCommandHandler(
       IUnitOfWork unitOfWork
       , IUserRepository userRepository
       , IBlogRepository blogRepository) : ICommandHandler<RemoveBlogContributorCommand>
    {
        public async Task<Result> Handle(RemoveBlogContributorCommand request, CancellationToken cancellationToken)
        {
            var user = await userRepository.GetByIdAsync(request.UserId);
            if (user is null)
                return Result.Failure(UserErrors.NotFound(request.UserId));

            var blog = await blogRepository.GetByIdAsync(request.BlogId, true);
            if (blog is null)
                return Result.Failure(BlogErrors.NotFound(request.BlogId));

            var res = blog.RemoveContributor(user);
            if (res.IsFailure)
                return res;

            await unitOfWork.SaveChangesAsync();
            return res;
        }
    }
}
