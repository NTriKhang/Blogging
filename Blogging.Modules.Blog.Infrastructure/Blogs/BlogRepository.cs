using Blogging.Modules.Blog.Domain.Blogs;
using Blogging.Modules.Blog.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogging.Modules.Blog.Infrastructure.Blogs
{
    internal class BlogRepository(BlogDbContext blogDbContext) : IBlogRepository
    {
        public async Task<Domain.Blogs.Blog?> GetByIdAsync(Guid id
            , bool includeContributor = false
            , CancellationToken cancellationToken = default)
        {
            IQueryable<Domain.Blogs.Blog> query = blogDbContext.Blogs;
            if(includeContributor)
            {
                query = query.Include(x => x.Contributors);
            }    
            var blog = await query.SingleOrDefaultAsync(x => x.Id == id, cancellationToken);
            return blog;
        }
        public void Insert(Domain.Blogs.Blog blog)
        {
            blogDbContext.Blogs.Add(blog);
        }
    }
}
