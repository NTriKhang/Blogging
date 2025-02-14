namespace Blogging.Modules.Blog.Domain.Blogs.State
{
    public class ModifyingState : BlogStateBase
    {
        public ModifyingState()
        {
            _state = BlogState.Modifying;
        }
    }
}
