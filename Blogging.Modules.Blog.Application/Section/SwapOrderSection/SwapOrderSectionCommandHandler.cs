using Blogging.Common.Application.Messaging;
using Blogging.Common.Domain;
using Blogging.Modules.Blog.Application.Abtractions.Data;
using Blogging.Modules.Blog.Domain.Sections;

namespace Blogging.Modules.Blog.Application.Section.SwapOrderSection
{
    internal sealed class SwapOrderSectionCommandHandler(
        IUnitOfWork unitOfWork
        , ISectionRepository sectionRepository) : ICommandHandler<SwapOrderSectionCommand>
    {
        public async Task<Result> Handle(SwapOrderSectionCommand request, CancellationToken cancellationToken)
        {
            Task<Domain.Sections.Section?> sectionA = sectionRepository.GetByIdAsync(request.SectionAId, cancellationToken);
            Task<Domain.Sections.Section?> sectionB = sectionRepository.GetByIdAsync(request.SectionBId, cancellationToken);
            var res = await Task.WhenAll(sectionA, sectionB);
            
            if (res[0] is null)
                return Result.Failure(SectionErrors.NotFound(request.SectionAId));

            if(res[1] is null)
                return Result.Failure(SectionErrors.NotFound(request.SectionBId));

            res[0]!.SwapOrder(res[1]!);
            await unitOfWork.SaveChangesAsync();    

            return Result.Success();
        }
    }
}
