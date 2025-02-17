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
    }
}
