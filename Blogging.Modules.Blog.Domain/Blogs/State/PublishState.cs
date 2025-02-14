namespace Blogging.Modules.Blog.Domain.Blogs.State
{
    public class PublishState : BlogStateBase
    {
        public PublishState()
        {
            _state = BlogState.Publish;
        }
        public override void UnPublish(Blog blog)
        {
            blog.TransitionToState(new ReviewState());
        }
    }
}
