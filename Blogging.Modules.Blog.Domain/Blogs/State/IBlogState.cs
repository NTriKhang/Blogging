namespace Blogging.Modules.Blog.Domain.Blogs
{
    public interface IBlogState
    {
        void Publish(Blog blog);
        void UnPublish(Blog blog);
        void Modify(Blog blog);
        void Hide(Blog blog);
    }
}
