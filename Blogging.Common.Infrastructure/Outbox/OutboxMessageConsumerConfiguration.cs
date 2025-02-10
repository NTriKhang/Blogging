using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blogging.Common.Infrastructure.Outbox
{
    public sealed class OutboxMessageConsumerConfiguration : IEntityTypeConfiguration<OutboxMessageConsumer>
    {
        public void Configure(EntityTypeBuilder<OutboxMessageConsumer> builder)
        {
            builder.ToTable("OutboxMessageConsumer");
            builder.HasKey(x => new { x.Id, x.Name });
        }
    }
}
