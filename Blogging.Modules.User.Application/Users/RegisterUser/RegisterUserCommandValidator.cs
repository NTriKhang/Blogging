using FluentValidation;

namespace Blogging.Modules.User.Application.Users.RegisterUser
{
    public sealed class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserCommandValidator()
        {
            RuleFor(x => x.request.UserName).NotEmpty().WithMessage(UserValidateError.UserNameEmpty);
            RuleFor(x => x.request.DisplayName).NotEmpty().WithMessage(UserValidateError.DisplayNameEmpty);
            RuleFor(x => x.request.Email).NotEmpty().WithMessage(UserValidateError.EmailEmpty)
                .EmailAddress().WithMessage(UserValidateError.EmailInvalid);
            //RuleFor(p => p.request.Password).NotEmpty().WithMessage(UserValidateError.PasswordEmpty)
            //     .MinimumLength(8).WithMessage(UserValidateError.PasswordMinLength)
            //     .MaximumLength(16).WithMessage(UserValidateError.PasswordMaxLength)
            //     .Matches(@"[A-Z]+").WithMessage(UserValidateError.PasswordUppercase)
            //     .Matches(@"[a-z]+").WithMessage(UserValidateError.PasswordLowercase)
            //     .Matches(@"[0-9]+").WithMessage(UserValidateError.PasswordNumber)
            //     .Matches(@"[\!\?\*\.]+").WithMessage(UserValidateError.PasswordSpecialChar);
        }
    }
}
