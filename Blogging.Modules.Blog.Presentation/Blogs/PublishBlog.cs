using Blogging.Common.Presentation.Endpoints;
using Blogging.Modules.Blog.Application.Blogs.PublishBlog;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogging.Modules.Blog.Presentation.Blogs
{
    internal sealed class PublishBlog : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPut("blogs/{id}/publish", async (Guid Id, ISender sender) =>
            {
                var cmd = new PublishBlogCommand(Id);
                var res = await sender.Send(cmd);

                return res.IsSuccess ? Results.Ok() : Results.Problem(res.Error.Description);
            });
        }
    }
}
