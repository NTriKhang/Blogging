using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Blogging.Modules.Blog.Domain.Contributes
{
    public interface IContributeRepository
    {
        Task<Contribute?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        void Insert(Contribute contribute);
    }
}
