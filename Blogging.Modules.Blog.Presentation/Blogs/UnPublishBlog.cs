using Blogging.Common.Presentation.Endpoints;
using Blogging.Modules.Blog.Application.Blogs.PublishBlog;
using Blogging.Modules.Blog.Application.Blogs.UnPublishBlog;
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
    internal class UnPublishBlog : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPut("blogs/{id}/unpublish", async (Guid Id, ISender sender) =>
            {
                var cmd = new UnPublishBlogCommand(Id);
                var res = await sender.Send(cmd);

                return res.IsSuccess ? Results.Ok() : Results.Problem(res.Error.Description);
            }).WithTags(Tags.Blogs);
        }
    }
}
