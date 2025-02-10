using Blogging.Common.Infrastructure.Outbox;
using Blogging.Modules.User.Application.Abtractions.Data;
using Blogging.Modules.User.Domain.Users;
using Blogging.Modules.User.Infrastructure.Users;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogging.Modules.User.Infrastructure.Database
{
    public class UserDbContext(DbContextOptions<UserDbContext> options) : DbContext(options), IUnitOfWork
    {
        internal DbSet<Domain.Users.User> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(Schemas.Users);

            modelBuilder.ApplyConfiguration(new OutboxMessageConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new OutboxMessageConsumerConfiguration());  
        }
    }
}
