using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blogging.Common.Infrastructure.Inbox
{
    public sealed class InboxMessageConsumerConfiguration : IEntityTypeConfiguration<InboxMessageConsumer>
    {
        public void Configure(EntityTypeBuilder<InboxMessageConsumer> builder)
        {
            builder.ToTable("InboxMessageConsumer");
            builder.HasKey(x => x.Id);
        }
    }
}
