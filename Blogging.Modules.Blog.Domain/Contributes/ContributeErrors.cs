using Blogging.Common.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogging.Modules.Blog.Domain.Contributes
{
    public static class ContributeErrors
    {
        public static Error NotFound(Guid Id) =>
            Error.NotFound("Contribute.NotFound", $"Contribute with the identifier {Id} is not found");
        public static Error ContributeAlreadyClosed(Guid Id) =>
          Error.NotFound("Contribute.ContributeAlreadyClosed", $"Contribute with the identifier {Id} was already closed");
    }
}
