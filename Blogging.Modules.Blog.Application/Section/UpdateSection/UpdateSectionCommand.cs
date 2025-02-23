using Blogging.Common.Application.Messaging;
using Blogging.Modules.Blog.Domain.Blogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogging.Modules.Blog.Application.Section.UpdateSection
{
    public sealed record UpdateSectionCommand(
        Guid Id
        , string Title
        , string Content) : ICommand;
}
