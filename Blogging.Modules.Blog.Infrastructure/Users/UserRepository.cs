using Blogging.Modules.Blog.Domain.Users;
using Blogging.Modules.Blog.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogging.Modules.Blog.Infrastructure.Users
{
    internal class UserRepository(BlogDbContext dbContext) : IUserRepository
    {
        public async Task<Domain.Users.User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await dbContext.Users.SingleOrDefaultAsync(r => r.Id == id, cancellationToken);
        }

        public async Task<Domain.Users.User?> GetByUserNameAsync(string username, CancellationToken cancellationToken = default)
        {
            return await dbContext.Users.SingleOrDefaultAsync(r => r.UserName == username, cancellationToken);
        }

        public void Insert(Domain.Users.User user)
        {
            dbContext.Users.Add(user);
        }
    }
}
