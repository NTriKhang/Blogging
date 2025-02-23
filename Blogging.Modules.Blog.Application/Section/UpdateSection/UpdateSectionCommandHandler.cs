using Blogging.Common.Application.Messaging;
using Blogging.Common.Domain;
using Blogging.Modules.Blog.Application.Abtractions.Data;
using Blogging.Modules.Blog.Domain.Sections;

namespace Blogging.Modules.Blog.Application.Section.UpdateSection
{
    internal sealed record UpdateSectionCommandHandler(
        IUnitOfWork unitOfWork
        , ISectionRepository sectionRepository) : ICommandHandler<UpdateSectionCommand>
    {
        public async Task<Result> Handle(UpdateSectionCommand request, CancellationToken cancellationToken)
        {
            var section = await sectionRepository.GetByIdAsync(request.Id);
            if (section is null)
                return Result.Failure(SectionErrors.NotFound(request.Id));

            section.Update(request.Title, request.Content);
            await unitOfWork.SaveChangesAsync();

            return Result.Success();
        }
    }
}
