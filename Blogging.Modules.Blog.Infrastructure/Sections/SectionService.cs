using Blogging.Common.Domain;
using Blogging.Modules.Blog.Application.Abtractions.Data;
using Blogging.Modules.Blog.Application.Section;
using Blogging.Modules.Blog.Domain.Sections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Blogging.Modules.Blog.Infrastructure.Sections
{
    internal class SectionService(
        ISectionRepository sectionRepository
        , IUnitOfWork unitOfWork) : ISectionService
    {
        public async Task<Result> SwapSectionOrder(Guid sectionAId, Guid sectionBId, CancellationToken cancellationToken = default)
        {
            var sectionA = await sectionRepository.GetByIdAsync(sectionAId, cancellationToken);
            var sectionB = await sectionRepository.GetByIdAsync(sectionBId, cancellationToken);

            if (sectionA is null)
                return Result.Failure(SectionErrors.NotFound(sectionAId));

            if (sectionB is null)
                return Result.Failure(SectionErrors.NotFound(sectionBId));

            if(sectionA.BlogId != sectionB.BlogId)
                return Result.Failure(SectionErrors.NotInTheSameBlog(sectionAId, sectionBId));

            int tmpOrder = sectionA.Order;
            sectionA.ChangeSectionOrder(sectionB.Order);
            sectionB.ChangeSectionOrder(-1);

            await unitOfWork.SaveChangesAsync();

            sectionB.ChangeSectionOrder(tmpOrder);
            await unitOfWork.SaveChangesAsync();

            return Result.Success();
        }
    }
}
