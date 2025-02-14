using Blogging.Common.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogging.Modules.Blog.Domain.Blogs
{
    internal sealed class BlogStateUpdatedDomainEvent(
        Guid blogId, string state) : DomainEvent
    {
        public Guid BlogId { get; init; } = blogId;
        public string State { get; init; } = state;
    }
}
