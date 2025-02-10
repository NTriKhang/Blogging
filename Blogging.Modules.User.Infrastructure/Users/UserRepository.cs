using Blogging.Modules.User.Domain.Users;
using Blogging.Modules.User.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Blogging.Modules.User.Infrastructure.Users
{
    internal sealed class UserRepository(UserDbContext dbContext) : IUserRepository
    {
        public async Task<Domain.Users.User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await dbContext.Users.SingleOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public async Task<Domain.Users.User?> GetByUserNameAsync(string username, CancellationToken cancellationToken = default)
        {
            return await dbContext.Users.SingleOrDefaultAsync(x => x.UserName == username, cancellationToken);
        }

        public void Insert(Domain.Users.User user)
        {
            foreach (Role role in user.Roles)
            {
                dbContext.Attach(role);
            }

            dbContext.Users.Add(user);
        }
    }
}
