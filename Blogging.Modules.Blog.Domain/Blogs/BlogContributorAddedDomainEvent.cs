﻿using Blogging.Common.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogging.Modules.Blog.Domain.Blogs
{
    public sealed class BlogContributorAddedDomainEvent(Guid blogId
        , Guid userId) : DomainEvent
    {
        public Guid BlogId { get; init; } = blogId;
        public Guid UserId { get; init; } = userId;
    }
}
