using Blogging.Common.Infrastructure.Inbox;
using Blogging.Common.Infrastructure.Outbox;
using Blogging.Modules.Blog.Application.Abtractions.Data;
using Blogging.Modules.Blog.Domain.Blogs;
using Blogging.Modules.Blog.Domain.Users;
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
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(Schemas.Blog);
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
