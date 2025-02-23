using Blogging.Modules.Blog.Domain.Contributes;
using Blogging.Modules.Blog.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Blogging.Modules.Blog.Infrastructure.Contribute
{
    internal class ContributeRepository(BlogDbContext blogDbContext) : IContributeRepository
    {
        public async Task<Domain.Contributes.Contribute?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await blogDbContext.Contributes.SingleOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public void Insert(Domain.Contributes.Contribute contribute)
        {
            blogDbContext.Contributes.Add(contribute);
        }
    }
}
