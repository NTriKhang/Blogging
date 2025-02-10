using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogging.Common.Application.Data
{
    public interface IDbConnectionFactory
    {
        ValueTask<DbConnection> OpenConnectionAsync();
    }
}
