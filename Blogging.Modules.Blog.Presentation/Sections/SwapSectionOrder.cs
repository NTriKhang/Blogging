using Blogging.Common.Presentation.Endpoints;
using Blogging.Modules.Blog.Application.Section.CreateSection;
using Blogging.Modules.Blog.Application.Section.SwapOrderSection;
using Blogging.Modules.Blog.Application.Section.UpdateSection;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Blogging.Modules.Blog.Presentation.Sections
{
    internal sealed class SwapSectionOrder : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPut("section/swap-section", async (SwapSectionOrderRequest request, ISender sender) =>
            {
                var cmd = new SwapOrderSectionCommand(request.SectionIdA, request.SectionIdB);

                var res = await sender.Send(cmd);

                return res.IsSuccess ? Results.Ok() : Results.Problem(res.Error.Description);
            }).WithTags(Tags.Sections);
        }
        internal sealed record SwapSectionOrderRequest(
            Guid SectionIdA
            , Guid SectionIdB);
    }
}
