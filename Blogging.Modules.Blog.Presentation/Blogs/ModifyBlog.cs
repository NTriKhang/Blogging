using Blogging.Common.Presentation.Endpoints;
using Blogging.Modules.Blog.Application.Blogs.ModifyBlog;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Blogging.Modules.Blog.Presentation.Blogs
{
    internal class ModifyBlog : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPut("blogs/{id}/modify", async (Guid Id, ISender sender) =>
            {
                var cmd = new ModifyBlogCommand(Id);
                var res = await sender.Send(cmd);

                return res.IsSuccess ? Results.Ok() : Results.Problem(res.Error.Description);
            }).WithTags(Tags.Blogs);
        }
    }
}
