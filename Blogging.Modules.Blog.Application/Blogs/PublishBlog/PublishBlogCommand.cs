using Blogging.Common.Application.Messaging;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogging.Modules.Blog.Application.Blogs.PublishBlog
{
    public sealed record PublishBlogCommand(Guid Id) : ICommand;
}
