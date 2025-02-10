using Blogging.Common.Domain;
using Blogging.Common.Presentation.Endpoints;
using Blogging.Modules.User.Application.Users.GetUser;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogging.Modules.User.Presentation.Users
{
    internal sealed class GetUser : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("users/{id}", async (Guid id, ISender sender) =>
            {
                Result<GetUserResponse> result = await sender.Send(new GetUserQuery(id));

                return result.IsSuccess ? Results.Ok(result.Value) : Results.Problem(result.Error.Description);
            })
            .WithTags(Tags.Users);
        }
    }
}
