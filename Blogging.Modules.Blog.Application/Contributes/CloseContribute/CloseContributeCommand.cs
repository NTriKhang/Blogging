using Blogging.Common.Application.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogging.Modules.Blog.Application.Contributes.CloseContribute
{
    public sealed record CloseContributeCommand(Guid Id, bool IsAccepted) : ICommand;
}
