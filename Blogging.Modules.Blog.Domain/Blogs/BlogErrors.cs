using Blogging.Common.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogging.Modules.Blog.Domain.Blogs
{
    public static class BlogErrors
    {
        public static Error NotFound(Guid Id) =>
            Error.NotFound("Blogs.NotFound", $"Blog with the identifier {Id} is not found");
        public static Error InvaldStateToProcess(Guid Id, BlogState state, string action) =>
            Error.Failure("Blogs.InvalidStateToProcess", $"Blog with the identifier {Id} is in state {state} and can't be {action}");
        public static Error ContributorAlreadyExist(Guid BlogId, Guid UserId) =>
            Error.Failure("Blogs.InvalidContributor", $"Blog with the identifier {BlogId} is already have this contributor {UserId}");
        public static Error ContributorDoesNotExist(Guid BlogId, Guid UserId) =>
           Error.Failure("Blogs.ContributorDoesNotExist", $"Blog with the identifier {BlogId} does not have this contributor {UserId}");

    }
}
