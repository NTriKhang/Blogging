using Blogging.Common.Presentation.Endpoints;
using Blogging.Modules.Blog.Application.Section.CreateSection;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogging.Modules.Blog.Presentation.Sections
{
    internal sealed class CreateSection : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("section", async (CreateSectionRequest request, ISender sender) =>
            {
                var cmd = new CreateSectionCommand(request.BlogId
                    , request.UserId
                    , request.Title
                    , request.Content);

                var res = await sender.Send(cmd);

                return res.IsSuccess ? Results.Ok(res.Value) : Results.Problem(res.Error.Description);
            }).WithTags(Tags.Sections);
        }
        internal sealed record CreateSectionRequest(Guid BlogId
            , Guid UserId
            , string Title
            , string Content);
    }
}
