namespace Blogging.Modules.Blog.Domain.Blogs.State
{
    public class DraftState : BlogStateBase
    {
        public DraftState()
        {
            _state = BlogState.Draft;
        }
        public override void Publish(Blog blog)
        {
            _state = BlogState.Review;
            blog.TransitionToState(new ReviewState());
        }
    }
}
