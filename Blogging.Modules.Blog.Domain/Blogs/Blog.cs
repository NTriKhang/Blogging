using Blogging.Common.Domain;
using Blogging.Modules.Blog.Domain.Blogs.State;
using Blogging.Modules.Blog.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Blogging.Modules.Blog.Domain.Blogs
{
    public sealed class Blog : Entity
    {
        private readonly HashSet<Users.User> _contributors = new();
        private IBlogState _blogState;
        private IBlogState BlogStateInstance
        {
            get
            {
                if (_blogState == null)
                    _blogState = BlogStateFactory.CreateState(this.State);
                return _blogState;
            }
            set
            {
                _blogState = value;
            }
        }
        public Guid Id { get; private set; }
        public Guid UserId { get; private set; }
        public string Title { get; private set; } = string.Empty;
        public string Description { get; private set; } = string.Empty;
        public DateTime CDate { get; private set; }
        public DateTime UDate { get; private set; }
        public string ThumbnailUrl { get; private set; } = string.Empty;
        public int Like { get; private set; }
        public int Dislike { get; private set; }
        public BlogState State { get; private set; }
        public IReadOnlyCollection<User> Contributors => _contributors;
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
                State = BlogState.Draft,
                _blogState = new DraftState(BlogState.Draft)
            };
            return blog;
        }
        public void SetBlogState(BlogState blogState)
        {
            BlogStateInstance = BlogStateFactory.CreateState(blogState);
            State = blogState;
        }
        public Result Publish()
        {
            if (State == BlogState.Publish)
                return Result.Failure(BlogErrors.InvaldStateToProcess(Id, State, nameof(Publish)));

            BlogStateInstance.Publish(this);
            Raise(new BlogStateUpdatedDomainEvent(Id, State));
            return Result.Success();
        }
        public Result UnPublish()
        {
            if (State == BlogState.Draft || State == BlogState.Modifying || State == BlogState.Hide)
                return Result.Failure(BlogErrors.InvaldStateToProcess(Id, State, nameof(UnPublish)));

            BlogStateInstance.UnPublish(this);
            Raise(new BlogStateUpdatedDomainEvent(Id, State));
            return Result.Success();
        }
        public Result Modify()
        {
            if (State == BlogState.Draft 
                || State == BlogState.Modifying 
                || State == BlogState.Review)
                return Result.Failure(BlogErrors.InvaldStateToProcess(Id, State, nameof(Modify)));

            BlogStateInstance.Modify(this);
            Raise(new BlogStateUpdatedDomainEvent(Id, State));
            return Result.Success();
        }
        public Result Hide()
        {
            if (State == BlogState.Draft 
                || State == BlogState.Modifying 
                || State == BlogState.Hide
                || State == BlogState.Publish)
                return Result.Failure(BlogErrors.InvaldStateToProcess(Id, State, nameof(Hide)));

            BlogStateInstance.Hide(this);
            Raise(new BlogStateUpdatedDomainEvent(Id, State));
            return Result.Success();
        }
        public Result AddContributor(User user)
        {
            if (_contributors.Contains(user))
                return Result.Failure(BlogErrors.ContributorAlreadyExist(Id, user.Id));
            _contributors.Add(user);

            Raise(new BlogContributorAdded(Id, user.Id));

            return Result.Success();
        }
        public Result RemoveContributor(User user)
        {
            if(!_contributors.Contains(user))
                return Result.Failure(BlogErrors.ContributorDoesNotExist(Id, user.Id));
            _contributors.Remove(user);

            Raise(new BlogContributorRemoved(Id, user.Id));

            return Result.Success();
        }
    }
}
