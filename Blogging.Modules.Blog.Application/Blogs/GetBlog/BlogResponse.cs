namespace Blogging.Modules.Blog.Application.Blogs.GetBlog
{
    public record BlogResponse(Guid Id
        , Guid UserId 
        , string Title 
        , string Description 
        , DateTime CDate
        , DateTime UDate 
        , string ThumbnailUrl
        , int Like 
        , int Dislike 
        , string State);
}
