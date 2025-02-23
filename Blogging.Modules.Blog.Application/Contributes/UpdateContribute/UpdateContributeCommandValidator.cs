using FluentValidation;

namespace Blogging.Modules.Blog.Application.Contributes.UpdateContribute
{
    internal sealed class UpdateContributeCommandValidator : AbstractValidator<UpdateContributeCommand>
    {
        public UpdateContributeCommandValidator()
        {
            RuleFor(c => c.Content).NotEmpty();
            RuleFor(c => c.Title).NotEmpty();
        }
    }
}
