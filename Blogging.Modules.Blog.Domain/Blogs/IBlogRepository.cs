using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogging.Modules.Blog.Domain.Blogs
{
    public interface IBlogRepository
    {
        Task<Blog?> GetByIdAsync(Guid id
            , bool includeContributor = false
            , CancellationToken cancellationToken = default);
        void Insert(Blog user);
    }
}
