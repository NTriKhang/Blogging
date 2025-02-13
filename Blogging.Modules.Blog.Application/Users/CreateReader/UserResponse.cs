namespace Blogging.Modules.Blog.Application.Users.CreateReader
{
    public sealed record UserResponse(Guid Id
        , string UserName
        , string DisplayName
        , string ImageUrl
        , string Email);
}
