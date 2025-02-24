using Blogging.Common.Domain;
using Blogging.Modules.Blog.Domain.Blogs.State;
using Blogging.Modules.Blog.Domain.Blogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogging.Modules.Blog.Domain.Sections
{
    public sealed class Section : Entity
    {
        public Guid Id { get; private set; }
        public Guid BlogId { get; private set; }
        public string Title { get; private set; } 
        public string Content { get; private set; }
        public int Order { get; private set; }  
        public DateTime CDate { get; private set; }
        public DateTime UDate { get; private set; }
        public static Section Create(
            Guid blogId
            , Guid userId
            , string title
            , string content
         )
        {
            Section section = new Section()
            {
                Id = Guid.NewGuid(),
                BlogId = blogId,
                Title = title,
                CDate = DateTime.UtcNow,
                UDate = DateTime.UtcNow,
                Content = content
            };

            section.Raise(new SectionCreatedDomainEvent( section.Id, userId, section.Content, section.Title ));

            return section;
        }
        public void ChangeSectionOrder(int newOrder)
        {
            if(Order == newOrder) return;

            Order = newOrder;
        }
        public void Update(
            Guid userId
            , string title
            , string content)
        {
            if (Title == title && Content == content)
                return;

            Raise(new SectionUpdatedDomainEvent(Id, userId, title, content));
        }
    }
}
