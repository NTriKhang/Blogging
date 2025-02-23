using Blogging.Modules.Blog.Domain.Contributes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blogging.Modules.Blog.Infrastructure.Contribute
{
    internal class ContributeContentConfiguration : IEntityTypeConfiguration<ContributeContent>
    {
        public void Configure(EntityTypeBuilder<ContributeContent> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne<Domain.Contributes.Contribute>().WithMany().HasForeignKey(x => x.ContributeId);
        }
    }
}
