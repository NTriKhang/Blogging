using Blogging.Common.Infrastructure.Inbox;
using Blogging.Common.Infrastructure.Outbox;
using Blogging.Modules.Blog.Application.Abtractions.Data;
using Blogging.Modules.Blog.Domain.Blogs;
using Blogging.Modules.Blog.Domain.Users;
using Blogging.Modules.Blog.Infrastructure.Blogs;
using Blogging.Modules.Blog.Infrastructure.Contribute;
using Blogging.Modules.Blog.Infrastructure.Sections;
using MassTransit.Middleware;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogging.Modules.Blog.Infrastructure.Database
{
    public class BlogDbContext(DbContextOptions<BlogDbContext> options) : DbContext(options), IUnitOfWork
    {
        internal DbSet<Domain.Users.User> Users { get; set; }
        internal DbSet<Domain.Blogs.Blog> Blogs { get; set; }
        internal DbSet<Domain.Sections.Section> Sections { get; set; }
        internal DbSet<Domain.Contributes.Contribute> Contributes { get; set; }
        internal DbSet<Domain.Contributes.ContributeContent> ContributeContents { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(Schemas.Blog);

            modelBuilder.ApplyConfiguration(new BlogConfiguration());
            modelBuilder.ApplyConfiguration(new SectionConfiguration());
            modelBuilder.ApplyConfiguration(new ContributeConfiguration());
            modelBuilder.ApplyConfiguration(new ContributeContentConfiguration());

            modelBuilder.ApplyConfiguration(new OutboxMessageConfiguration());
            modelBuilder.ApplyConfiguration(new OutboxMessageConsumerConfiguration());
            modelBuilder.ApplyConfiguration(new InboxMessageConfiguration());
            modelBuilder.ApplyConfiguration(new InboxMessageConsumerConfiguration());

            modelBuilder
                .Entity<Blog.Domain.Blogs.Blog>()
                .Property(e => e.State)
                .HasConversion<string>();
        }
    }
}
