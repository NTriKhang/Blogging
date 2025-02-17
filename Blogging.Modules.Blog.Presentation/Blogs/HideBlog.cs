using Blogging.Common.Presentation.Endpoints;
using Blogging.Modules.Blog.Application.Blogs.HideBlog;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Blogging.Modules.Blog.Presentation.Blogs
{
    internal class HideBlog : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPut("blogs/{id}/hide", async (Guid Id, ISender sender) =>
            {
                var cmd = new HideBlogCommand(Id);
                var res = await sender.Send(cmd);

                return res.IsSuccess ? Results.Ok() : Results.Problem(res.Error.Description);
            }).WithTags(Tags.Blogs);
        }
    }
}
