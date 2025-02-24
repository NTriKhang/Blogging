using Blogging.Common.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogging.Modules.Blog.Domain.Blogs
{
    public sealed class BlogStateUpdatedDomainEvent(
        Guid blogId, BlogState state) : DomainEvent
    {
        public Guid BlogId { get; init; } = blogId;
        public BlogState State { get; init; } = state;
    }
}
