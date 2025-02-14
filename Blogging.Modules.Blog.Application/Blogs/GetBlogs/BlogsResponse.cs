using Blogging.Modules.Blog.Application.Blogs.GetBlog;

namespace Blogging.Modules.Blog.Application.Blogs.GetBlogs
{
    public record BlogsResponse(IEnumerable<BlogResponse> Blogs
        , int Page
        , int PageSize
        , int TotalPages
        );
}
