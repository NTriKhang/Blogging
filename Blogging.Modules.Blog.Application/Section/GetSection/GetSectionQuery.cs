using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogging.Modules.Blog.Application.Section.GetSection
{
    public record SectionResponse(Guid Id
        , Guid BlogId
        , string Title
        , string Content
        , int Order
        , DateTime CDate
        , DateTime UDate);
    //public sealed record GetSectionQuery
}
