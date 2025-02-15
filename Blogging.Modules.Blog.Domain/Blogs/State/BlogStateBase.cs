using Blogging.Modules.Blog.Domain.Blogs.State;

namespace Blogging.Modules.Blog.Domain.Blogs
{
    public enum BlogState
    {
        Draft,
        Review,
        Publish,
        Modifying,
        Hide
    }
    public abstract class BlogStateBase : IBlogState
    {
        protected BlogState _state { get; set; }
        protected BlogStateBase(BlogState state)
        {
            _state = state;
        }
        public void Hide(Blog blog)
        {
            _state = BlogState.Hide;
            blog.SetBlogState(_state);
        }
        public virtual void Modify(Blog blog)
        {
            _state = BlogState.Modifying;
            blog.SetBlogState(_state);
        }
        public virtual void Publish(Blog blog)
        {
            _state = BlogState.Publish;
            blog.SetBlogState(_state);
        }
        public virtual void UnPublish(Blog blog)
        {

        }
    }
}
