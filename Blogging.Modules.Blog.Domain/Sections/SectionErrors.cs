using Blogging.Common.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogging.Modules.Blog.Domain.Sections
{
    public static class SectionErrors
    {
        public static Error NotFound(Guid Id) =>
            Error.NotFound("Sections.NotFound", $"Section with the identifier {Id} is not found");
        public static Error NotInTheSameBlog(Guid SectionA, Guid SectionB) =>
            Error.Problem("Sections.NotInTheSameBlog", $"Section {SectionA} is not the same blog with section {SectionB}");
    }
}
