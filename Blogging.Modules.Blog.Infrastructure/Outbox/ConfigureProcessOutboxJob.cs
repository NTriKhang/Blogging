using Blogging.Common.Infrastructure.Outbox;
using Microsoft.Extensions.Options;
using Quartz;

namespace Blogging.Modules.Blog.Infrastructure.Outbox
{
    internal sealed class ConfigureProcessOutboxJob : ConfigureProcessOutboxJobBase<OutboxOptions>
    {
        public ConfigureProcessOutboxJob(IOptions<OutboxOptions> options) : base(options)
        {

        }
    }
}
