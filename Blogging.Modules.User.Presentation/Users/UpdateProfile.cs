using Blogging.Common.Presentation.Endpoints;
using Blogging.Modules.User.Application.Users.UpdateProfile;
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
    internal class UpdateProfile : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPut("users/update_profile", async (UpdateProfileRequest request, ISender sender) =>
            {
                var updateCommand = new UpdateProfileCommand(
                    request.UserId
                    , request.DisplayName
                    , request.UserName
                    , request.Email
                    , request.ImageUrl);
                var res = await sender.Send(updateCommand);
                return res.IsSuccess ? Results.NoContent() : Results.Problem(res.Error.Description);
            }).WithTags(Tags.Users);
        }
        internal sealed class UpdateProfileRequest
        {
            public Guid UserId { get; set; }
            public string UserName { get; set; }
            public string DisplayName { get; set; }
            public string Email { set; get; }
            public string ImageUrl { get; set; }
        }
    }
}
