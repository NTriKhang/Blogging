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
        public BlogState State => _state;
        protected BlogState _state { get; set; }

        public void Hide(Blog blog)
        {
            _state = BlogState.Hide;
            blog.TransitionToState(new HideState());
        }
        public virtual void Modify(Blog blog)
        {
            _state = BlogState.Modifying;
            blog.TransitionToState(new ModifyingState());
        }
        public virtual void Publish(Blog blog)
        {
            _state = BlogState.Publish;
            blog.TransitionToState(new PublishState());
        }
        public virtual void UnPublish(Blog blog)
        {

        }
    }
}
