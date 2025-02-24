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
            blog.SetBlogPublicVisible(false);
            blog.SetBlogInternalVisible(true);
            blog.SetBlogState(_state);
        }
        public override void UnHide(Blog blog)
        {
            _state = BlogState.Review;
            blog.SetBlogPublicVisible(false);
            blog.SetBlogInternalVisible(true);
            blog.SetBlogState(_state);
        }
    }
}
