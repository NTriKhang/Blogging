using Blogging.Common.Presentation.Endpoints;
using Blogging.Modules.Blog.Application.Blogs.AddBlogContributor;
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
using static Blogging.Modules.Blog.Presentation.Blogs.CreateBlog;

namespace Blogging.Modules.Blog.Presentation.Blogs
{
    internal sealed class AddContributor : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("blogs/{blogId}/contributors", async (Guid blogId, AddContributorRequest request, ISender sender) =>
            {
                var createCmd = new AddBlogContributorCommand(blogId, request.UserId);
                var res = await sender.Send(createCmd);

                return res.IsSuccess ? Results.Ok() : Results.Problem(res.Error.Description);
                ;
            }).WithTags(Tags.Blogs);
        }
        internal record AddContributorRequest(Guid UserId);
    }
}
