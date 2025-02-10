using Microsoft.Extensions.Options;
using Quartz;

namespace Blogging.Modules.User.Infrastructure.Outbox
{
    internal sealed class ConfigureProcessOutboxJob
        (IOptions<OutboxOptions> options): IConfigureOptions<QuartzOptions>
    {
        private readonly OutboxOptions outboxOptions = options.Value;
        public void Configure(QuartzOptions options)
        {
            string jobName = typeof(ProcessOutbox).FullName!;

            options
                .AddJob<ProcessOutbox>(config => config.WithIdentity(jobName))
                .AddTrigger(config 
                    => config.ForJob(jobName)
                    .WithSimpleSchedule(schedule =>
                        schedule.WithIntervalInSeconds(outboxOptions.IntervalInSeconds).RepeatForever()));

        }
    }
}
