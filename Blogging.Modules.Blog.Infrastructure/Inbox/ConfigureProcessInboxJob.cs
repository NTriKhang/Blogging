using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogging.Modules.Blog.Infrastructure.Inbox
{
    internal class ConfigureProcessInboxJob(IOptions<InboxOptions> inboxOption)
        : IConfigureOptions<QuartzOptions>
    {
        private readonly InboxOptions inboxOptions = inboxOption.Value;
        public void Configure(QuartzOptions options)
        {
            string jobName = typeof(ProcessInbox).FullName!;
            options
                .AddJob<ProcessInbox>(conf => conf.WithIdentity(jobName))
                .AddTrigger(conf =>
                    conf.ForJob(jobName)
                    .WithSimpleSchedule(schedule =>
                        schedule.WithIntervalInSeconds(inboxOptions.IntervalInSeconds).RepeatForever()));
        }
    }
}
