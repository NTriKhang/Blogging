using FluentValidation;

namespace Blogging.Modules.Blog.Application.Section.SwapOrderSection
{
    internal sealed class SwapOrderSectionCommandValidator : AbstractValidator<SwapOrderSectionCommand>
    {
        public SwapOrderSectionCommandValidator()
        {
            RuleFor(x => x.SectionAId).NotEmpty().NotEqual(x => x.SectionBId);
            RuleFor(x => x.SectionBId).NotEmpty();
        }
    }
}
