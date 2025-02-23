using Blogging.Common.Application.Messaging;
using Blogging.Common.Domain;
using Blogging.Modules.Blog.Application.Abtractions.Data;
using Blogging.Modules.Blog.Domain.Contributes;

namespace Blogging.Modules.Blog.Application.Contributes.UpdateContribute
{
    internal sealed class UpdateContributeContentCommandHandler(
        IContributeRepository contributeRepository
        , IContributeContentRepository contributeContentRepository
        , IUnitOfWork unitOfWork)
        : ICommandHandler<UpdateContributeCommand>
    {
        public async Task<Result> Handle(UpdateContributeCommand request, CancellationToken cancellationToken)
        {
            var contribute = await contributeRepository.GetByIdAsync(request.ContributeId);

            if (contribute is null)
                return Result.Failure(ContributeErrors.NotFound(request.ContributeId));

            contribute.Update(request.Content, request.Title);

            var contributeContent = ContributeContent.Create(contribute.Id, contribute.Content);
            contributeContentRepository.Insert(contributeContent);

            await unitOfWork.SaveChangesAsync();

            return Result.Success();
        }
    }
}
