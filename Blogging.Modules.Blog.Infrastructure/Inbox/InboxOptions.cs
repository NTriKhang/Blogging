using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogging.Modules.Blog.Infrastructure.Inbox
{
    internal class InboxOptions
    {
        public int IntervalInSeconds { get; set; }
        public int BatchSize { get; set; }
    }
}
