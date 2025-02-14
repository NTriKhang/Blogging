namespace Blogging.Modules.Blog.Domain.Blogs.State
{
    public class HideState : BlogStateBase
    {
        public HideState()
        {
            _state = BlogState.Hide;
        }
    }
}
