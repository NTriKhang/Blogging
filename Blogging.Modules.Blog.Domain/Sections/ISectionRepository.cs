using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogging.Modules.Blog.Domain.Sections
{
    internal interface ISectionRepository
    {
        Task<IEnumerable<Section>?> GetByBlogIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<Section?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        void Insert(Section section);
    }
}
