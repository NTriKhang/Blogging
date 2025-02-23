using Blogging.Common.Infrastructure.Inbox;
using Blogging.Modules.Blog.Infrastructure.Outbox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogging.Modules.Blog.Infrastructure.Inbox
{
    internal class InboxOptions : InboxOptionsBase
    {
        public int IntervalInSeconds { get; set; }
        public int BatchSize { get; set; }

        public override string JobName => typeof(ProcessInbox).FullName!;

        public override Type JobType => typeof(ProcessInbox);
    }
}
