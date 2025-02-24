using Blogging.Modules.Blog.Domain.Blogs;
using Blogging.Modules.Blog.Domain.Sections;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Blogging.Modules.Blog.Infrastructure.Sections
{
    internal class SectionConfiguration : IEntityTypeConfiguration<Section>
    {
        public void Configure(EntityTypeBuilder<Section> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasIndex(s => new { s.BlogId, s.Order }).IsUnique();

            builder.HasOne<Domain.Blogs.Blog>().WithMany().HasForeignKey(s => s.BlogId);

            builder.Property(s => s.Order)
                .ValueGeneratedOnAdd();
        }
    }
}
