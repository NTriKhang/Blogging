using Microsoft.Extensions.Options;
using Quartz;

namespace Blogging.Common.Infrastructure.Outbox
{
    public class ConfigureProcessOutboxJobBase<TOutboxOptions>
        (IOptions<TOutboxOptions> options) : IConfigureOptions<QuartzOptions>
        where TOutboxOptions : OutboxOptionsBase
    {
        private readonly TOutboxOptions outboxOptions = options.Value;
        public void Configure(QuartzOptions options)
        {
            string jobName = outboxOptions.JobName;
            var jobType = outboxOptions.JobType;

            options
                .AddJob(jobType, config => config.WithIdentity(jobName))
                .AddTrigger(config
                    => config.ForJob(jobName)
                    .WithSimpleSchedule(schedule =>
                        schedule.WithIntervalInSeconds(outboxOptions.IntervalInSeconds).RepeatForever()));

        }
    }
}
