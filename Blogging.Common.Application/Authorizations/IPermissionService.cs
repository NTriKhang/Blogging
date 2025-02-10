using Blogging.Common.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogging.Common.Application.Authorizations
{
    public interface IPermissionService
    {
        Task<Result<PermissionResponse>> GetUserPermissionAsync(string identityId);
    }
}
