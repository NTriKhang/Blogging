using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogging.Common.Presentation.Endpoints
{
    public interface IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app);
    }
}
