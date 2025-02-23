using Blogging.Common.Application.Messaging;
using Blogging.Modules.Blog.Application.Blogs.GetBlog;
using Blogging.Modules.Blog.Application.Blogs.GetBlogs;
using Blogging.Modules.Blog.Domain.Sections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogging.Modules.Blog.Application.Section.GetSections
{
    public sealed record GetSectionsQuery(Guid BlogId, int Page, int PageSize) 
        : IQuery<SectionsResponse>;
}
