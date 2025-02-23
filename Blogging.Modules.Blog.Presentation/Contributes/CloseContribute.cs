using Blogging.Common.Presentation.Endpoints;
using Blogging.Modules.Blog.Application.Contributes.CloseContribute;
using Blogging.Modules.Blog.Application.Contributes.UpdateContribute;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogging.Modules.Blog.Presentation.Contributes
{
    internal sealed class CloseContribute : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPut("contributes/{id}/close", async (Guid Id, CloseContributeRequest request, ISender sender) =>
            {
                var cmd = new CloseContributeCommand(Id, request.IsAccepted);
                var res = await sender.Send(cmd);

                return res.IsSuccess ? Results.Ok() : Results.Problem(res.Error.Description);
            }).WithTags(Tags.Contributes);
        }
        internal sealed record CloseContributeRequest(bool IsAccepted);
    }
}
