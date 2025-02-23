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
        : INotificationHandler<SectionUpdatedDomainEvent>, INotificationHandler<SectionCreatedDomainEvent>
    {
        public async Task Handle(SectionUpdatedDomainEvent notification, CancellationToken cancellationToken)
        {
            await CreateContribute(notification.UserId, notification.SectionId, notification.Content, notification.Title);
        }

        public async Task Handle(SectionCreatedDomainEvent notification, CancellationToken cancellationToken)
        {
            await CreateContribute(notification.UserId, notification.SectionId, notification.Content, notification.Title);
        }
        private async Task CreateContribute(Guid UserId, Guid SectionId, string Content, string Title)
        {
            var contribute = Contribute.Create(
            UserId
            , SectionId
            , Content
            , Title);

            contributeRepository.Insert(contribute);

            var contributeContent = ContributeContent.Create(
                contribute.Id
                , Content);

            contributeContentRepository.Insert(contributeContent);

            await unitOfWork.SaveChangesAsync();
        }
    }
}
