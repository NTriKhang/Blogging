using Blogging.Common.Application.Messaging;
using Blogging.Modules.Blog.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogging.Modules.Blog.Application.Contributes.UpdateContribute
{
    public sealed record UpdateContributeCommand(
        Guid ContributeId
        , string Title
        , string Content) : ICommand;
}
