namespace Blogging.Modules.Blog.Domain.Blogs.State
{
    public class ModifyingState : BlogStateBase
    {
        public ModifyingState(BlogState state) : base(state)
        {
        }
        public override void UnHide(Blog blog)
        {
            throw new NotImplementedException();
        }
    }
}
