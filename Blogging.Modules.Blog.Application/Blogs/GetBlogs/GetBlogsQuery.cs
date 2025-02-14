using Blogging.Common.Application.Messaging;
using Blogging.Modules.Blog.Application.Blogs.GetBlog;
using Blogging.Modules.Blog.Domain.Blogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogging.Modules.Blog.Application.Blogs.GetBlogs
{
    public sealed record GetBlogsQuery(int page, int pageSize) : IQuery<BlogsResponse>;
}
