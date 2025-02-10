using Blogging.Common.Application.Messaging;
using Blogging.Common.Domain;
using Blogging.Modules.Blog.Application.Abtractions.Data;
using Blogging.Modules.Blog.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogging.Modules.Blog.Application.Users.CreateReader
{
    public sealed record ReaderResponse(Guid ReaderId
        , string ReaderName
        , string ReaderDisplayName
        , string ReaderImageUrl
        , string ReaderEmail);
    public sealed record CreateReaderCommand(Guid Id
        , string UserName
        , string DisplayName
        , string ImageUrl
        , string Email) : ICommand<Guid>;
    internal sealed class CreateReaderCommandHandler(
        IReaderRepository userRepository
        , IUnitOfWork unitOfWork) : ICommandHandler<CreateReaderCommand, Guid>
    {
        public async Task<Result<Guid>> Handle(CreateReaderCommand request, CancellationToken cancellationToken)
        {
            var reader = Reader.Create(request.Id
                , request.UserName
                , request.DisplayName
                , request.Email
                , request.ImageUrl);
            
            userRepository.Insert(reader);
            await unitOfWork.SaveChangesAsync();

            return reader.Id;
        }
    }
}
