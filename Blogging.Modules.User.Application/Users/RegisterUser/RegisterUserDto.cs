namespace Blogging.Modules.User.Application.Users.RegisterUser
{
    public sealed record RegisterUserDto(string UserName
        , string DisplayName
        , string Email
        , string Password
        , string ImageUrl);
}
