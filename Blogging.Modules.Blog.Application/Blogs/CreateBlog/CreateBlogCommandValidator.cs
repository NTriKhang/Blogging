using FluentValidation;

namespace Blogging.Modules.Blog.Application.Blogs.CreateBlog
{
    public sealed class CreateBlogCommandValidator : AbstractValidator<CreateBlogCommand>
    {
        public CreateBlogCommandValidator()
        {
            RuleFor(x => x.Title).NotEmpty();
            RuleFor(x => x.Description).NotEmpty();
        }
    }
}
