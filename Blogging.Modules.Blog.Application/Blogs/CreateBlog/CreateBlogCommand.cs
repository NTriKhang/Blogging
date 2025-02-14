using Blogging.Common.Application.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogging.Modules.Blog.Application.Blogs.CreateBlog
{
    public sealed record CreateBlogCommand(Guid UserId
        , string Title
        , string Description
        , string ThumbnailUrl) : ICommand<Guid>;
}
