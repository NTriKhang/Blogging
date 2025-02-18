using Blogging.Common.Application.Messaging;
using Blogging.Modules.Blog.Application.Blogs.AddBlogContributor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogging.Modules.Blog.Application.Blogs.RemoveBlogContributor
{
    public sealed record RemoveBlogContributorCommand(Guid BlogId, Guid UserId) : ICommand;
}
