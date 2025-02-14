using Blogging.Common.Application.Messaging;
using Blogging.Modules.Blog.Application.Blogs.ModifyBlog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogging.Modules.Blog.Application.Blogs.HideBlog
{
    public sealed record HideBlogCommand(Guid Id) : ICommand;
}
