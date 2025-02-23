using Blogging.Common.Presentation.Endpoints;
using Blogging.Modules.Blog.Application.Blogs.UnPublishBlog;
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
    internal sealed class UpdateContribute : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPut("contributes/{id}", async (Guid Id, UpdateContributeRequest request, ISender sender) =>
            {
                var cmd = new UpdateContributeCommand(Id, request.Title, request.Content);
                var res = await sender.Send(cmd);

                return res.IsSuccess ? Results.Ok() : Results.Problem(res.Error.Description);
            }).WithTags(Tags.Contributes);
        }
        internal sealed record UpdateContributeRequest(string Title, string Content);
    }
}
