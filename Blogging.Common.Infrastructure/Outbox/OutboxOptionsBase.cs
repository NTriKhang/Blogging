using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogging.Common.Infrastructure.Outbox
{
    public abstract class OutboxOptionsBase
    {
        public virtual int IntervalInSeconds { get; set; }
        public virtual int BatchSize { get; set; }
        public abstract string JobName { get; }
        public abstract Type JobType { get; }
    }
}
