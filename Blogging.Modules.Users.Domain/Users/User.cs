using Blogging.Common.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogging.Modules.User.Domain.Users
{
    public sealed class User : Entity
    {
        public Guid Id { get; private set; }
        public string IdentityId { get; private set; }
        public string UserName { get; private set; }
        public string DisplayName { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
        public string? ImageUrl { get; private set; }
        private List<Role> _roles = [];
        public IReadOnlyCollection<Role> Roles => _roles.ToList();
        public static User Create(Guid Id
            , string IdentiyId
            , string UserName
            , string DisplayName
            , string Email
            , string Password
            , string ImageUrl)
        {
            User users = new User
            {
                Id = Id,
                IdentityId = IdentiyId,
                UserName = UserName,
                DisplayName = DisplayName,
                Email = Email,
                Password = Password,
                ImageUrl = ImageUrl,
            };
            users._roles.Add(Role.Administrator);

            users.Raise(new UserRegisterDomainEvent(Id));
            return users;
        }
        public void UpdateProfile(string displayName,
            string userName,
            string email,
            string imageUrl
            )
        {
            if (displayName == DisplayName
                && userName == UserName
                && email == Email
                && imageUrl == ImageUrl)
                return;

            DisplayName = displayName;
            UserName = userName;
            Email = email;
            ImageUrl = imageUrl;

            Raise(new UserProfileUpdatedDomainEvent(
                Id,
                DisplayName,
                UserName,
                Email,
                imageUrl));
        }
        public void RequestAuthCode()
        {
            Raise(new UserAuthCodeRequestedDomainEvent(Id, Email));
        }
        public void UpdatePassword(string password)
        {
            if (Password == password)
                return;
            Password = password;
            Raise(new UserPasswordUpdated(Id));
        }
    }
}
