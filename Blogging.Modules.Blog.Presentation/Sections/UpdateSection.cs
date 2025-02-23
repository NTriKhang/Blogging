using Blogging.Common.Presentation.Endpoints;
using Blogging.Modules.Blog.Application.Section.CreateSection;
using Blogging.Modules.Blog.Application.Section.UpdateSection;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Blogging.Modules.Blog.Presentation.Sections
{
    internal sealed class UpdateSection : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPut("section", async (UpdateSectionRequest request, ISender sender) =>
            {
                var cmd = new UpdateSectionCommand(
                    request.Id
                    , request.UserId
                    , request.Title
                    , request.Content);

                var res = await sender.Send(cmd);

                return res.IsSuccess ? Results.Ok() : Results.Problem(res.Error.Description);
            }).WithTags(Tags.Sections);
        }
        internal sealed record UpdateSectionRequest(Guid Id
            , Guid UserId
            , string Title
            , string Content);
    }
}
