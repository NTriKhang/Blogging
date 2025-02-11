using Blogging.Common.Infrastructure.Inbox;
using Blogging.Common.Infrastructure.Outbox;
using Blogging.Modules.Blog.Application.Abtractions.Data;
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
        internal DbSet<Reader> Readers { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(Schema.Blog);
            modelBuilder.ApplyConfiguration(new InboxMessageConfiguration());
        }
    }
}
