namespace Blogging.Modules.Blog.Domain.Blogs.State
{
    public class PublishState : BlogStateBase
    {
        public PublishState(BlogState state) : base(state)
        {
        }

        public override void UnPublish(Blog blog)
        {
            _state = BlogState.Review;
            blog.SetBlogPublicVisible(false);
            blog.SetBlogInternalVisible(true);
            blog.SetBlogState(_state);
        }
        public override void UnHide(Blog blog)
        {
            _state = BlogState.Publish;
            blog.SetBlogPublicVisible(true);
            blog.SetBlogInternalVisible(true);
            blog.SetBlogState(_state);
        }
    }
}
