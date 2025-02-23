using Blogging.Common.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogging.Modules.Blog.Domain.Sections
{
    public sealed class SectionUpdatedDomainEvent(
        Guid sectionId
        , Guid userId
        , string title
        , string content) : DomainEvent
    {
        public Guid SectionId { get; init; } = sectionId;
        public Guid UserId { get; init; } = userId;
        public string Title { get; init; } = title;
        public string Content { get; init; } = content;
    }
}
