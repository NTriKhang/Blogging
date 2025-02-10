using Blogging.Common.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogging.Modules.User.Application.Abtractions.Identity
{
    public interface IIdentityProviderService
    {
        Task<Result<string>> RegisterUserAsync(UserIdentityRegisterDto user
            , CancellationToken cancellationToken = default);
    }
}
