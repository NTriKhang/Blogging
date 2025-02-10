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
    internal class ReaderRepository(BlogDbContext dbContext) : IReaderRepository
    {
        public async Task<Reader?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await dbContext.Readers.SingleOrDefaultAsync(r => r.Id == id, cancellationToken);
        }

        public async Task<Reader?> GetByUserNameAsync(string username, CancellationToken cancellationToken = default)
        {
            return await dbContext.Readers.SingleOrDefaultAsync(r => r.UserName == username, cancellationToken);
        }

        public void Insert(Reader user)
        {
            dbContext.Readers.Add(user);
        }
    }
}
