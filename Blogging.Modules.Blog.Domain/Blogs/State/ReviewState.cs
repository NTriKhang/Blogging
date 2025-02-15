namespace Blogging.Modules.Blog.Domain.Blogs.State
{
    public class ReviewState : BlogStateBase
    {
        public ReviewState(BlogState state) : base(state)
        {
        }

        public override void UnPublish(Blog blog)
        {
            _state = BlogState.Draft;
            blog.SetBlogState(_state);
        }
    }
}
