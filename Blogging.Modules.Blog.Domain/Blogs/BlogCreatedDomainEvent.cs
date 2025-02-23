using Blogging.Common.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogging.Modules.Blog.Domain.Blogs
{
    public class BlogCreatedDomainEvent(
        Guid Id) : DomainEvent
    {
        public Guid BlogId { get; init; } = Id;
    }
}
