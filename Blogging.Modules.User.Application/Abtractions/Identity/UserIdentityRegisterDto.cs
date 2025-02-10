namespace Blogging.Modules.User.Application.Abtractions.Identity
{
    public sealed record UserIdentityRegisterDto(string userName, string email, string password);
}
