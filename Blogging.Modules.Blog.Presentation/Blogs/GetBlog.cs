using Blogging.Common.Presentation.Endpoints;
using Blogging.Modules.Blog.Application.Blogs.GetBlog;
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
    internal sealed class GetBlog : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("blogs/{id}", async (Guid id, ISender sender) =>
            {
                var getBlogCommand = new GetBlogQuery(id);

                var res = await sender.Send(getBlogCommand);

                return res.IsSuccess ? Results.Ok(res.Value) : Results.Problem(res.Error.Description);
                ;
            }).WithTags(Tags.Blogs);
        }
    }
}
