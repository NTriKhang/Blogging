using Blogging.Modules.Blog.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blogging.Modules.Blog.Infrastructure.Blogs
{
    internal class BlogConfiguration : IEntityTypeConfiguration<Blog.Domain.Blogs.Blog>
    {
        public void Configure(EntityTypeBuilder<Domain.Blogs.Blog> builder)
        {
            builder
                .HasMany(b => b.Contributors)
                .WithMany(u => u.ContributedBlogs)
                .UsingEntity(joinBuilder =>
                {
                    joinBuilder.ToTable("BlogContributor");
                });
        }
    }
}
