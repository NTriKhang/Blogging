using Blogging.Modules.Blog.Application.Section.GetSection;

namespace Blogging.Modules.Blog.Application.Section.GetSections
{
    public record SectionsResponse(IEnumerable<SectionResponse> Sections
        , int Page
        , int PageSize
        , int TotalPages
        );
}
