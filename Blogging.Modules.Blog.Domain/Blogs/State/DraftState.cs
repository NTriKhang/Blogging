namespace Blogging.Modules.Blog.Domain.Blogs.State
{
    public class DraftState : BlogStateBase
    {
        public DraftState(BlogState state) : base(state)
        {
        }

        public override void Publish(Blog blog)
        {
            _state = BlogState.Review;
            blog.SetBlogState(_state);
        }
    }
}
