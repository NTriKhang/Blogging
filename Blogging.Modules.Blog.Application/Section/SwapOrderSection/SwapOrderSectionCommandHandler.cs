using Blogging.Common.Application.Messaging;
using Blogging.Common.Domain;
using Blogging.Modules.Blog.Application.Abtractions.Data;
using Blogging.Modules.Blog.Domain.Sections;

namespace Blogging.Modules.Blog.Application.Section.SwapOrderSection
{
    internal sealed class SwapOrderSectionCommandHandler(
        ISectionService sectionService) : ICommandHandler<SwapOrderSectionCommand>
    {
        public async Task<Result> Handle(SwapOrderSectionCommand request, CancellationToken cancellationToken)
        {
            return await sectionService.SwapSectionOrder(request.SectionAId, request.SectionBId, cancellationToken);
        }
    }
}
