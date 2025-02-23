using Blogging.Common.Application.Messaging;
using Blogging.Common.Domain;
using Blogging.Modules.Blog.Application.Abtractions.Data;
using Blogging.Modules.Blog.Domain.Sections;

namespace Blogging.Modules.Blog.Application.Section.DeleteSection
{
    internal sealed class DeleteSectionCommandHandler(
        IUnitOfWork unitOfWork
        , ISectionRepository sectionRepository) : ICommandHandler<DeleteSectionCommand>
    {
        public async Task<Result> Handle(DeleteSectionCommand request, CancellationToken cancellationToken)
        {
            var section = await sectionRepository.GetByIdAsync(request.Id);
            if(section is null) 
                return Result.Failure(SectionErrors.NotFound(request.Id));

            sectionRepository.Delete(section, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
