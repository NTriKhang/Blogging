using Blogging.Common.Presentation.Endpoints;
using Blogging.Modules.Blog.Application.Blogs.GetBlog;
using Blogging.Modules.Blog.Application.Blogs.GetBlogs;
using Blogging.Modules.Blog.Application.Section.GetSections;
using Blogging.Modules.Blog.Presentation;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

internal sealed class GetSections : IEndpoint
{
    private const int DefaultPage = 1;
    private const int DefaultPageSize = Paginate.SectionPerPage;

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("sections/{blogId}", async (ISender sender
            , Guid blogId 
            , int page = DefaultPage
            , int pageSize = DefaultPageSize) =>
        {
            if (page <= 0 || pageSize <= 0)
            {
                return Results.BadRequest("Invalid pagination parameters.");
            }
            var getBlogQuery = new GetSectionsQuery(blogId, page, pageSize);

            var res = await sender.Send(getBlogQuery);

            return res.IsSuccess ? Results.Ok(res.Value) : Results.Problem(res.Error.Description);
        }).WithTags(Tags.Sections);
    }
}
