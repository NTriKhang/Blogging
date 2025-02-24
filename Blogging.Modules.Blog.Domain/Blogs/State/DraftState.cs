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
            blog.SetBlogInternalVisible(true);
            blog.SetBlogPublicVisible(false);
            blog.SetBlogState(_state);
        }
        public override void UnHide(Blog blog)
        {
            _state = BlogState.Draft;
            blog.SetBlogPublicVisible(false);
            blog.SetBlogInternalVisible(true);
            blog.SetBlogState(_state);
        }
    }
}
