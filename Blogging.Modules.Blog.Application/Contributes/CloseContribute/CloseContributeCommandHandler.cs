using Blogging.Common.Application.Messaging;
using Blogging.Common.Domain;
using Blogging.Modules.Blog.Application.Abtractions.Data;
using Blogging.Modules.Blog.Domain.Contributes;

namespace Blogging.Modules.Blog.Application.Contributes.CloseContribute
{
    internal sealed class CloseContributeCommandHandler(
        IContributeRepository contributeRepository
        , IUnitOfWork unitOfWork)
        : ICommandHandler<CloseContributeCommand>
    {
        public async Task<Result> Handle(CloseContributeCommand request, CancellationToken cancellationToken)
        {
            var contribute = await contributeRepository.GetByIdAsync(request.Id);
            if (contribute is null)
                return Result.Failure(ContributeErrors.NotFound(request.Id));

            var res = contribute.Close(request.IsAccepted);
            if (res.IsFailure)
                return res;

            await unitOfWork.SaveChangesAsync();

            return res;
        }
    }
}
