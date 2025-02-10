namespace Blogging.Modules.User.Application.Users.RegisterUser
{
    internal sealed class UserValidateError
    {
        public static readonly string UserNameEmpty = "User name can't be empty";
        public static readonly string DisplayNameEmpty = "Display name can't be empty";
        public static readonly string EmailEmpty = "Email can't be empty";
        public static readonly string PasswordEmpty = "Your password cannot be empty";
        public static readonly string EmailInvalid = "Email is invalid";
        public static readonly string PasswordMinLength = "Your password length must be at least 8.";
        public static readonly string PasswordMaxLength = "Your password length must not exceed 16.";
        public static readonly string PasswordUppercase = "Your password must contain at least one uppercase letter.";
        public static readonly string PasswordLowercase = "Your password must contain at least one lowercase letter.";
        public static readonly string PasswordNumber = "Your password must contain at least one number.";
        public static readonly string PasswordSpecialChar = "Your password must contain at least one (!? *.).";
    }
}
