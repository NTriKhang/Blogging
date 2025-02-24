namespace Blogging.Modules.Blog.Domain.Blogs.State
{
    public class HideState : BlogStateBase
    {
        public HideState(BlogState state) : base(state)
        {
        }
        public override void UnHide(Blog blog)
        {
        }
    }
}
