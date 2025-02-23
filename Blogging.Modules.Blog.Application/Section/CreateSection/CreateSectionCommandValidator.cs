using FluentValidation;

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
}
