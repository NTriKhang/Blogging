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
            blog.SetBlogState(_state);
        }
    }
}
