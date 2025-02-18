using Blogging.Common.Presentation.Endpoints;
using Blogging.Modules.Blog.Application.Blogs.RemoveBlogContributor;
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
    internal sealed class RemoveContributor : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapDelete("blogs/{blogId}/contributors/{userId}", async (Guid blogId, Guid userId, ISender sender) =>
            {
                var createCmd = new RemoveBlogContributorCommand(blogId, userId);
                var res = await sender.Send(createCmd);

                return res.IsSuccess ? Results.Ok() : Results.Problem(res.Error.Description);
            }).WithTags(Tags.Blogs);
        }
    }
}
