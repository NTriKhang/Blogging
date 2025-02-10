using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blogging.Common.Application.Data;
using Npgsql;

namespace Blogging.Common.Infrastructure.Data
{
    internal class DbConnectionFactory(NpgsqlDataSource dataSource) : IDbConnectionFactory
    {
        public async ValueTask<DbConnection> OpenConnectionAsync()
        {
            return await dataSource.OpenConnectionAsync();
        }
    }
}
