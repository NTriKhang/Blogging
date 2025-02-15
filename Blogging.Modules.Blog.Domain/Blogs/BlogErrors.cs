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
        public static Error BlogAlreadyPublic(Guid Id) =>
            Error.Failure("Blogs.BlogAlreadyPublic", $"Blog with the identifier {Id} was already published");
    }
}
