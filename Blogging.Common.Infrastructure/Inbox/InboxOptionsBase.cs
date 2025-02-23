using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogging.Common.Infrastructure.Inbox
{
    public abstract class InboxOptionsBase
    {
        public int IntervalInSeconds { get; set; }
        public int BatchSize { get; set; }
        public abstract string JobName { get; }
        public abstract Type JobType { get; }
    }
}
