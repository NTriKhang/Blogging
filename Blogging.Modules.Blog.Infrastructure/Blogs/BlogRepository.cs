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
        public async Task<Domain.Blogs.Blog?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var blog = await blogDbContext.Blogs.SingleOrDefaultAsync(x => x.Id == id, cancellationToken);
            return blog;
        }
        public void Insert(Domain.Blogs.Blog blog)
        {
            blogDbContext.Blogs.Add(blog);
        }
    }
}
