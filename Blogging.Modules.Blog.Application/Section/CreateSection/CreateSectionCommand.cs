using Blogging.Common.Application.Messaging;
using Blogging.Common.Domain;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogging.Modules.Blog.Application.Section.CreateSection
{
    internal sealed class CreateSectionCommandValidator : AbstractValidator<CreateSectionCommand>
    {
        public CreateSectionCommandValidator()
        {
            RuleFor(x => x.BlogId).NotEmpty();
            RuleFor(x => x.Title).NotEmpty();
        }
    }
    public sealed record CreateSectionCommand(
        Guid BlogId
        , string Title
        , string Content
        , int Order) : ICommand<Guid>;

    internal sealed record CreateSectionCommandHandler : ICommandHandler<CreateSectionCommand, Guid>
    {
        public Task<Result<Guid>> Handle(CreateSectionCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
