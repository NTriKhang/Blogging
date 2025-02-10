namespace Blogging.Modules.User.Application.Users.GetUser
{
    public sealed record GetUserResponse(
        Guid Id,
        string UserName,
        string DisplayName,
        string Email,
        string ImageUrl);
}
