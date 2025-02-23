using Blogging.Common.Application.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogging.Modules.Blog.Application.Section.CreateSection
{
    public sealed record CreateSectionCommand(
        Guid BlogId
        , Guid UserId
        , string Title
        , string Content
        , int Order) : ICommand<Guid>;
}
