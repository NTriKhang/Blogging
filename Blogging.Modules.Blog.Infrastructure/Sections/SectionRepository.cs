using Blogging.Modules.Blog.Domain.Blogs;
using Blogging.Modules.Blog.Domain.Sections;
using Blogging.Modules.Blog.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogging.Modules.Blog.Infrastructure.Sections
{
    internal class SectionRepository(BlogDbContext blogDbContext) : ISectionRepository
    {
        public void Delete(Section section, CancellationToken cancellationToken = default)
        {
            blogDbContext.Sections.Remove(section);
        }

        public async Task<Section?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await blogDbContext.Sections.SingleOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public void Insert(Section section)
        {
            blogDbContext.Sections.Add(section);
        }
    }
}
