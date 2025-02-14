using Blogging.Common.Domain;
using Blogging.Modules.Blog.Domain.Blogs.State;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogging.Modules.Blog.Domain.Blogs
{
    public sealed class Blog : Entity
    {
        private IBlogState _blogState;
        public Guid Id { get; private set; }
        public Guid UserId { get; private set; }
        public string Title { get; private set; } = string.Empty;
        public string Description { get; private set; } = string.Empty;
        public DateTime CDate { get; private set; }
        public DateTime UDate { get; private set; }
        public string ThumbnailUrl { get; private set; } = string.Empty;
        public int Like { get; private set; }
        public int Dislike { get; private set; }
        public string State { get; private set; } = string.Empty;

        public static Blog Create(
            Guid UserId
            , string Title
            , string Description
            , string ThumbnailUrl
            )
        {
            Blog blog = new Blog()
            {
                Id = Guid.NewGuid(),
                UserId = UserId,
                Title = Title,
                Description = Description,
                CDate = DateTime.UtcNow,
                UDate = DateTime.UtcNow,
                ThumbnailUrl = ThumbnailUrl,
                Like = 0,
                Dislike = 0,
                State = BlogState.Draft.ToString(),
                _blogState = new DraftState()
            };
            return blog;
        }
        public void SetBlogState(IBlogState blogState)
        {
            _blogState = blogState;
        }
        public void Publish()
        {
            _blogState.Publish(this);
            Raise(new BlogStateUpdatedDomainEvent(Id, State));
        }
        public void UnPublish()
        {
            _blogState.UnPublish(this);
            Raise(new BlogStateUpdatedDomainEvent(Id, State));
        }
        public void Modify()
        {
            _blogState.Modify(this);
            Raise(new BlogStateUpdatedDomainEvent(Id, State));
        }
        public void Hide()
        {
            _blogState.Hide(this);
            Raise(new BlogStateUpdatedDomainEvent(Id, State));
        }
        internal void TransitionToState(IBlogState newState)
        {
            _blogState = newState;
            State = _blogState.State.ToString();
        }
    }
}
