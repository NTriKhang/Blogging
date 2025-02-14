using Blogging.Common.Presentation.Endpoints;
using Blogging.Modules.Blog.Application.Blogs.GetBlog;
using Blogging.Modules.Blog.Application.Blogs.GetBlogs;
using Blogging.Modules.Blog.Presentation;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

internal sealed class GetBlogs : IEndpoint
{
    private const int DefaultPage = 1;
    private const int DefaultPageSize = Paginate.BlogPerPage;

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("blogs", async (ISender sender, int page = DefaultPage, int pageSize = DefaultPageSize) =>
        {
            if (page <= 0 || pageSize <= 0)
            {
                return Results.BadRequest("Invalid pagination parameters.");
            }
            var getBlogQuery = new GetBlogsQuery(page, pageSize);

            var res = await sender.Send(getBlogQuery);

            return res.IsSuccess ? Results.Ok(res.Value) : Results.Problem(res.Error.Description);
        }).WithTags(Tags.Blogs);
    }
}
