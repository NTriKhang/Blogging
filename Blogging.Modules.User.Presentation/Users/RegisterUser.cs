using Blogging.Common.Domain;
using Blogging.Common.Presentation.Endpoints;
using Blogging.Modules.User.Application.Users.RegisterUser;
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
    internal class RegisterUser : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("users/register", async (RegisterUserDto request, ISender sender) =>
            {
                var cmd = new RegisterUserCommand(request);
                Result<Guid> res = await sender.Send(cmd);

                return res.IsSuccess ? Results.Ok(res.Value) : Results.Problem(res.Error.Description);
            })
            .WithTags(Tags.Users);
        }
    }
}
