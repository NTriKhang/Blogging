using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogging.Modules.Blog.Domain.Users
{
    public interface IReaderRepository
    {
        Task<Reader?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<Reader?> GetByUserNameAsync(string username, CancellationToken cancellationToken = default);
        void Insert(Reader user);
    }
}
