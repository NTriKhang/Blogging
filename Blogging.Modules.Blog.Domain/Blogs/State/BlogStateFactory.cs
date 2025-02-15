using Blogging.Modules.Blog.Domain.Blogs.State;

namespace Blogging.Modules.Blog.Domain.Blogs
{
    public static class BlogStateFactory
    {
        public static IBlogState CreateState(BlogState state)
        {
            return state switch
            {
                BlogState.Draft => new DraftState(state),
                BlogState.Review => new ReviewState(state),
                BlogState.Publish => new PublishState(state),
                BlogState.Modifying => new ModifyingState(state),
                BlogState.Hide => new HideState(state),
                _ => throw new ArgumentException("Invalid blog state"),
            };
        }
    }
}
