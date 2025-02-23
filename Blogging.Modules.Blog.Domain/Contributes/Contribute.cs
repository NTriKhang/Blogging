using Blogging.Common.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogging.Modules.Blog.Domain.Contributes
{
    public sealed class Contribute : Entity
    {
        public Guid Id { get; private set; }
        public Guid UserId { get; private set; }
        public Guid SectionId { get; private set; }
        public string Content { get; private set; }
        public DateTime CDate { get; private set; } 
        public DateTime UDate { get; private set; }
        public bool IsClosed { get; private set; }
        public DateTime? CloseDate { get; private set; }

        public static Contribute Create(
            Guid userId
            , Guid sectionId
            , string content)
        {
            var contribute = new Contribute()
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                SectionId = sectionId,
                Content = content,
                CDate = DateTime.UtcNow,
                UDate = DateTime.UtcNow,
                IsClosed = false,
                CloseDate = null
            };

            return contribute;
        }
    }
}
