using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blogging.Modules.Blog.Infrastructure.Contribute
{
    internal class ContributeConfiguration : IEntityTypeConfiguration<Domain.Contributes.Contribute>
    {
        public void Configure(EntityTypeBuilder<Domain.Contributes.Contribute> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne<Domain.Users.User>().WithMany().HasForeignKey(s => s.UserId);
            builder.HasOne<Domain.Sections.Section>().WithMany().HasForeignKey(s => s.SectionId);

        }
    }
}
