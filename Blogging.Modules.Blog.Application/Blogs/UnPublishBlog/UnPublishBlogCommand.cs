using Blogging.Common.Application.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogging.Modules.Blog.Application.Blogs.UnPublishBlog
{
    public sealed record UnPublishBlogCommand(Guid Id) : ICommand;
}
