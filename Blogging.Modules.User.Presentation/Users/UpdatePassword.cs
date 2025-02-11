using Blogging.Common.Presentation.Endpoints;
using Blogging.Modules.User.Application.Users.UpdatePassword;
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
    internal class UpdatePassword : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPut("users/update_password", async (UpdatePasswordRequest request, ISender sender) =>
            {
                var updateCommand = new UpdatePasswordCommand(request.UserId, request.Password);
                var res = await sender.Send(updateCommand);

                return res.IsSuccess ? Results.NoContent() : Results.Problem(res.Error.Description);
            }).WithTags(Tags.Users);
        }
        internal sealed class UpdatePasswordRequest
        {
            public Guid UserId { get; set; }
            public string Password { get; set; }
        }
    }
}
