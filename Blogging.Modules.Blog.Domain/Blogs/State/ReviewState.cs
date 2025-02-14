namespace Blogging.Modules.Blog.Domain.Blogs.State
{
    public class ReviewState : BlogStateBase
    {
        public ReviewState()
        {
            _state = BlogState.Review;
        }
        public override void UnPublish(Blog blog)
        {
            blog.TransitionToState(new DraftState());
        }
    }
}
