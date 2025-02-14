using Blogging.Common.Presentation.Endpoints;
using Blogging.Modules.Blog.Application.Blogs.CreateBlog;
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
    internal sealed class CreateBlog : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("blogs", async (CreateBlogRequest request, ISender sender) =>
            {
                var createCmd = new CreateBlogCommand(request.UserId
                    , request.Title
                    , request.Description
                    , request.ThumbnailUrl);
                var res = await sender.Send(createCmd);

                return res.IsSuccess ? Results.Ok(res.Value) : Results.Problem(res.Error.Description);
                ;
            }).WithTags(Tags.Blogs);
        }
        internal sealed record CreateBlogRequest(Guid UserId
            , string Title
            , string Description
            , string ThumbnailUrl);
    }
}
