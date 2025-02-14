namespace Blogging.Modules.Blog.Domain.Blogs
{
    public static class BlogStateFactory
    {
        public static IBlogState CreateState(BlogState state)
        {
            return state switch
            {
                BlogState.Draft => new DraftState(),
                BlogState.Review => new ReviewState(),
                BlogState.Publish => new PublishState(),
                BlogState.Modifying => new ModifyingState(),
                BlogState.Hide => new HideState(),
                _ => throw new ArgumentException("Invalid blog state"),
            };
        }
    }
}
