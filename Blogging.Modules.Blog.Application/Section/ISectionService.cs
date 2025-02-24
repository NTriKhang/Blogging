using Blogging.Common.Domain;
using Blogging.Modules.Blog.Domain.Sections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogging.Modules.Blog.Application.Section
{
    public interface ISectionService
    {
        Task<Result> SwapSectionOrder(Guid SectionAId, Guid SectionBId, CancellationToken cancellationToken = default);
    }
}
