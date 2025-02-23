using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogging.Modules.Blog.Domain.Sections
{
    public interface ISectionRepository
    {
        Task<Section?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        void Delete(Section section, CancellationToken cancellationToken = default);
        void Insert(Section section);
    }
}
