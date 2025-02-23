using Blogging.Common.Infrastructure.Outbox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogging.Modules.Blog.Infrastructure.Outbox
{
    internal sealed class OutboxOptions : OutboxOptionsBase
    {
        public override string JobName 
        { 
            get => typeof(ProcessOutbox).FullName!; 
        }
        public override Type JobType 
        { 
            get => typeof(ProcessOutbox); 
        }
    }
}
