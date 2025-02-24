using Blogging.Modules.Blog.Domain.Blogs;
using Blogging.Modules.Blog.UnitTests.Abstractions;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogging.Modules.Blog.UnitTests.Blogs
{
    public class BlogTest : BaseTest
    {
        [Fact]
        public void Publish_ShouldReturnReviewState_WhenStateWasDraft()
        {
            // Arrange
            var blog = Domain.Blogs.Blog.Create(
                Guid.NewGuid()
                , Faker.Music.Genre()
                , Faker.Music.Genre()
                , Faker.Music.Genre()); // draft state when initial
            
            // Act
            blog.Publish(); //state change to review
            
            // Assert
            blog.State.ShouldBe(BlogState.Review);
            blog.IsInternalVisible.ShouldBeTrue();
            blog.IsPublicVisible.ShouldBeFalse();
            AssertDomainEventWasPublished<BlogStateUpdatedDomainEvent>(blog);
        }

        [Fact]
        public void Publish_ShouldReturnPublishState_WhenStateWasReview()
        {
            // Arrange
            var blog = Domain.Blogs.Blog.Create(
              Guid.NewGuid()
              , Faker.Music.Genre()
              , Faker.Music.Genre()
              , Faker.Music.Genre()); // draft state when initial
            blog.Publish(); // state change to review
            blog.ClearDomain();

            // Act
            blog.Publish(); // state change to publish

            // Assert
            blog.State.ShouldBe(BlogState.Publish);
            blog.IsInternalVisible.ShouldBeTrue();
            blog.IsPublicVisible.ShouldBeTrue();
            AssertDomainEventWasPublished<BlogStateUpdatedDomainEvent>(blog);
        }

        [Fact]
        public void Publish_ShouldReturnFailure_WhenStateWasPublish()
        {
            // Arrange
            var blog = Domain.Blogs.Blog.Create(
              Guid.NewGuid()
              , Faker.Music.Genre()
              , Faker.Music.Genre()
              , Faker.Music.Genre()); // draft state when initial
            blog.Publish(); // state change to review
            blog.Publish(); // state change to publish
            
            // Act
            var res = blog.Publish(); // state change to publish

            // Assert
            blog.State.ShouldBe(BlogState.Publish);
            res.IsSuccess.ShouldBeFalse();
            res.IsFailure.ShouldBeTrue();
            res.Error.Description.ShouldBe(BlogErrors.InvalidStateToProcess(blog.Id, blog.State, nameof(blog.Publish)).Description);
        }
        [Fact]
        public void UnPublish_ShouldReturnFailure_WhenStateWasDraft()
        {
            // Arrange
            var blog = Domain.Blogs.Blog.Create(
                Guid.NewGuid(),
                Faker.Music.Genre(),
                Faker.Music.Genre(),
                Faker.Music.Genre()); // draft state when initial

            // Act
            var res = blog.UnPublish(); 

            // Assert
            res.IsFailure.ShouldBeTrue();
            res.Error.Description.ShouldBe(BlogErrors.InvalidStateToProcess(blog.Id, blog.State, nameof(blog.UnPublish)).Description);

            blog.State.ShouldBe(BlogState.Draft);
            blog.IsInternalVisible.ShouldBeTrue();
            blog.IsPublicVisible.ShouldBeFalse();
        }
        [Fact]
        public void UnPublish_ShouldSetStateToDraft_WhenStateWasReview()
        {
            // Arrange
            var blog = Domain.Blogs.Blog.Create(
                Guid.NewGuid(),
                Faker.Music.Genre(),
                Faker.Music.Genre(),
                Faker.Music.Genre()); // draft state when initial

            blog.Publish(); // state change to review
            blog.ClearDomain();

            // Act
            var res = blog.UnPublish(); // state change to draft

            // Assert
            res.IsSuccess.ShouldBeTrue();
            blog.State.ShouldBe(BlogState.Draft);
            blog.IsInternalVisible.ShouldBeTrue();
            blog.IsPublicVisible.ShouldBeFalse();
            AssertDomainEventWasPublished<BlogStateUpdatedDomainEvent>(blog);
        }
        [Fact]
        public void UnPublish_ShouldSetStateToReview_WhenStateWasPublish()
        {
            // Arrange
            var blog = Domain.Blogs.Blog.Create(
                Guid.NewGuid(),
                Faker.Music.Genre(),
                Faker.Music.Genre(),
                Faker.Music.Genre()); // draft state when initial

            blog.Publish(); // state change to review
            blog.Publish(); // state change to publish
            blog.ClearDomain();
            // Act
            var res = blog.UnPublish(); // state change to review

            // Assert
            res.IsSuccess.ShouldBeTrue();
            blog.State.ShouldBe(BlogState.Review);
            blog.IsInternalVisible.ShouldBeTrue();
            blog.IsPublicVisible.ShouldBeFalse();
            AssertDomainEventWasPublished<BlogStateUpdatedDomainEvent>(blog);
        }
        [Fact]
        public void Modify_ShouldSetStateToModifying_WhenStateWasPublish()
        {
            // Arrange
            var blog = Domain.Blogs.Blog.Create(
                Guid.NewGuid(),
                Faker.Music.Genre(),
                Faker.Music.Genre(),
                Faker.Music.Genre()); // draft state when initial

            blog.Publish(); // state change to review
            blog.Publish(); // state change to publish
            blog.ClearDomain();

            // Act
            var res = blog.Modify(); // state change to modifying

            // Assert
            res.IsSuccess.ShouldBeTrue();
            blog.State.ShouldBe(BlogState.Modifying);
            AssertDomainEventWasPublished<BlogStateUpdatedDomainEvent>(blog);
        }

        [Fact]
        public void Modify_ShouldReturnFailure_WhenStateWasDraft()
        {
            // Arrange
            var blog = Domain.Blogs.Blog.Create(
                Guid.NewGuid(),
                Faker.Music.Genre(),
                Faker.Music.Genre(),
                Faker.Music.Genre()); // draft state when initial

            // Act
            var res = blog.Modify(); // attempt to modify

            // Assert
            blog.State.ShouldBe(BlogState.Draft);
            res.IsFailure.ShouldBeTrue();
            res.Error.Description.ShouldBe(BlogErrors.InvalidStateToProcess(blog.Id, blog.State, nameof(blog.Modify)).Description);
        }
        [Fact]
        public void Modify_ShouldReturnFailure_WhenStateWasReview()
        {
            // Arrange
            var blog = Domain.Blogs.Blog.Create(
                Guid.NewGuid(),
                Faker.Music.Genre(),
                Faker.Music.Genre(),
                Faker.Music.Genre()); // draft state when initial

            blog.Publish();

            // Act
            var res = blog.Modify(); // attempt to modify

            // Assert
            blog.State.ShouldBe(BlogState.Review);
            res.IsFailure.ShouldBeTrue();
            res.Error.Description.ShouldBe(BlogErrors.InvalidStateToProcess(blog.Id, blog.State, nameof(blog.Modify)).Description);
        }
        [Fact]
        public void Modify_ShouldReturnFailure_WhenStateWasModify()
        {
            // Arrange
            var blog = Domain.Blogs.Blog.Create(
                Guid.NewGuid(),
                Faker.Music.Genre(),
                Faker.Music.Genre(),
                Faker.Music.Genre()); // draft state when initial
            blog.Publish(); // to review
            blog.Publish(); // to publish
            blog.Modify(); // to modify

            // Act
            var res = blog.Modify(); // attempt to modify

            // Assert
            blog.State.ShouldBe(BlogState.Modifying);
            res.IsFailure.ShouldBeTrue();
            res.Error.Description.ShouldBe(BlogErrors.InvalidStateToProcess(blog.Id, blog.State, nameof(blog.Modify)).Description);
        }
        [Fact]
        public void Hide_ShouldSetStateToHide_WhenStateWasPublish()
        {
            // Arrange
            var blog = Domain.Blogs.Blog.Create(
                Guid.NewGuid(),
                Faker.Music.Genre(),
                Faker.Music.Genre(),
                Faker.Music.Genre()); // draft state when initial

            blog.Publish(); // state change to review
            blog.Publish(); // state change to publish
            blog.ClearDomain();
            // Act
            var res = blog.Hide(); // state change to hide

            // Assert
            res.IsSuccess.ShouldBeTrue();
            blog.State.ShouldBe(BlogState.Hide);
            AssertDomainEventWasPublished<BlogStateUpdatedDomainEvent>(blog);
        }
        [Fact]
        public void Hide_ShouldReturnFailure_WhenStateWasHide()
        {
            // Arrange
            var blog = Domain.Blogs.Blog.Create(
                Guid.NewGuid(),
                Faker.Music.Genre(),
                Faker.Music.Genre(),
                Faker.Music.Genre()); // draft state when initial

            blog.Publish(); // state change to review
            blog.Publish(); // state change to publish
            blog.Hide(); // state change to hide

            // Act
            var res = blog.Hide(); // attempt to hide again

            // Assert
            res.IsFailure.ShouldBeTrue();
            res.Error.Description.ShouldBe(BlogErrors.InvalidStateToProcess(blog.Id, blog.State, nameof(blog.Hide)).Description);
        }

        [Fact]
        public void AddContributor_ShouldAddContributor_WhenContributorDoesNotExist()
        {
            // Arrange
            var blog = Domain.Blogs.Blog.Create(
                Guid.NewGuid(),
                Faker.Music.Genre(),
                Faker.Music.Genre(),
                Faker.Music.Genre()); // draft state when initial

            var user = Domain.Users.User.Create(
                Guid.NewGuid(),
                Faker.Person.UserName,
                Faker.Person.FullName,
                Faker.Person.Email,
                Faker.Person.Avatar);

            // Act
            var res = blog.AddContributor(user);

            // Assert
            res.IsSuccess.ShouldBeTrue();
            blog.Contributors.ShouldContain(user);
            AssertDomainEventWasPublished<BlogContributorAddedDomainEvent>(blog);
        }

        [Fact]
        public void AddContributor_ShouldReturnFailure_WhenContributorAlreadyExists()
        {
            // Arrange
            var blog = Domain.Blogs.Blog.Create(
                Guid.NewGuid(),
                Faker.Music.Genre(),
                Faker.Music.Genre(),
                Faker.Music.Genre()); // draft state when initial

            var user = Domain.Users.User.Create(
                Guid.NewGuid(),
                Faker.Person.UserName,
                Faker.Person.FullName,
                Faker.Person.Email,
                Faker.Person.Avatar);

            blog.AddContributor(user);

            // Act
            var res = blog.AddContributor(user); // attempt to add again

            // Assert
            res.IsFailure.ShouldBeTrue();
            res.Error.Description.ShouldBe(BlogErrors.ContributorAlreadyExist(blog.Id, user.Id).Description);
        }

        [Fact]
        public void RemoveContributor_ShouldRemoveContributor_WhenContributorExists()
        {
            // Arrange
            var blog = Domain.Blogs.Blog.Create(
                Guid.NewGuid(),
                Faker.Music.Genre(),
                Faker.Music.Genre(),
                Faker.Music.Genre()); // draft state when initial

            var user = Domain.Users.User.Create(
                Guid.NewGuid(),
                Faker.Person.UserName,
                Faker.Person.FullName,
                Faker.Person.Email,
                Faker.Person.Avatar);

            blog.AddContributor(user);

            // Act
            var res = blog.RemoveContributor(user);

            // Assert
            res.IsSuccess.ShouldBeTrue();
            blog.Contributors.ShouldNotContain(user);
            AssertDomainEventWasPublished<BlogContributorRemovedDomainEvent>(blog);
        }
        [Fact]
        public void RemoveContributor_ShouldReturnFailure_WhenContributorDoesNotExist()
        {
            // Arrange
            var blog = Domain.Blogs.Blog.Create(
                Guid.NewGuid(),
                Faker.Music.Genre(),
                Faker.Music.Genre(),
                Faker.Music.Genre()); // draft state when initial

            var user = Domain.Users.User.Create(
                Guid.NewGuid(),
                Faker.Person.UserName,
                Faker.Person.FullName,
                Faker.Person.Email,
                Faker.Person.Avatar);

            // Act
            var res = blog.RemoveContributor(user); // attempt to remove non-existent contributor

            // Assert
            res.IsFailure.ShouldBeTrue();
            res.Error.Description.ShouldBe(BlogErrors.ContributorDoesNotExist(blog.Id, user.Id).Description);
        }
        [Fact]
        public void UnHide_ShouldSetStateToDraft_WhenStateWasHideAndTargetStateIsDraft()
        {
            // Arrange
            var blog = Domain.Blogs.Blog.Create(
                Guid.NewGuid(),
                Faker.Music.Genre(),
                Faker.Music.Genre(),
                Faker.Music.Genre()); // draft state when initial

            blog.Hide(); // state change to hide
            blog.ClearDomain();

            // Act
            var res = blog.UnHide(BlogState.Draft); // state change to draft

            // Assert
            res.IsSuccess.ShouldBeTrue();
            blog.State.ShouldBe(BlogState.Draft);
            AssertDomainEventWasPublished<BlogStateUpdatedDomainEvent>(blog);
        }

        [Fact]
        public void UnHide_ShouldSetStateToReview_WhenStateWasHideAndTargetStateIsReview()
        {
            // Arrange
            var blog = Domain.Blogs.Blog.Create(
                Guid.NewGuid(),
                Faker.Music.Genre(),
                Faker.Music.Genre(),
                Faker.Music.Genre()); // draft state when initial

            blog.Hide(); // state change to hide
            blog.ClearDomain();

            // Act
            var res = blog.UnHide(BlogState.Review); // state change to review

            // Assert
            res.IsSuccess.ShouldBeTrue();
            blog.State.ShouldBe(BlogState.Review);
            AssertDomainEventWasPublished<BlogStateUpdatedDomainEvent>(blog);
        }

        [Fact]
        public void UnHide_ShouldReturnFailure_WhenStateIsNotHide()
        {
            // Arrange
            var blog = Domain.Blogs.Blog.Create(
                Guid.NewGuid(),
                Faker.Music.Genre(),
                Faker.Music.Genre(),
                Faker.Music.Genre()); // draft state when initial

            blog.Publish(); // state change to review

            // Act
            var res = blog.UnHide(BlogState.Draft); // attempt to unhide from review state

            // Assert
            res.IsFailure.ShouldBeTrue();
            res.Error.Description.ShouldBe(BlogErrors.InvalidStateToProcess(blog.Id, blog.State, nameof(blog.UnHide)).Description);
        }

        [Fact]
        public void UnHide_ShouldReturnFailure_WhenTargetStateIsModifying()
        {
            // Arrange
            var blog = Domain.Blogs.Blog.Create(
                Guid.NewGuid(),
                Faker.Music.Genre(),
                Faker.Music.Genre(),
                Faker.Music.Genre()); // draft state when initial

            blog.Hide(); // state change to hide

            // Act
            var res = blog.UnHide(BlogState.Modifying); // attempt to unhide to modifying state

            // Assert
            res.IsFailure.ShouldBeTrue();
            res.Error.Description.ShouldBe(BlogErrors.InvalidStateToProcess(blog.Id, blog.State, nameof(blog.UnHide)).Description);
        }
    }
}
