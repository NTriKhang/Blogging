using Blogging.Common.Domain;
using Blogging.Modules.Blog.Application.Abtractions.Data;
using Blogging.Modules.Blog.Application.Section.GetSections;
using Blogging.Modules.Blog.Domain.Contributes;
using Blogging.Modules.Blog.Domain.Sections;
using Blogging.Modules.Blog.Domain.Users;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogging.Modules.Blog.Application.Contributes.CreateContribute
{
    internal class CreateContributeDomainEventHandler(
        IContributeRepository contributeRepository
        , IContributeContentRepository contributeContentRepository
        , IUnitOfWork unitOfWork)
        : INotificationHandler<SectionCreatedDomainEvent>
    {
        public async Task Handle(SectionCreatedDomainEvent notification, CancellationToken cancellationToken)
        {
            var contribute = Contribute.Create(
                notification.UserId
                , notification.SectionId
                , notification.Content);

            contributeRepository.Insert(contribute);

            var contributeContent = ContributeContent.Create(
                contribute.Id
                , notification.Content);

            contributeContentRepository.Insert(contributeContent);

            await unitOfWork.SaveChangesAsync();
        }
    }
}
