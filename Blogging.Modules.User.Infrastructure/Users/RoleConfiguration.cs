using Blogging.Modules.User.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogging.Modules.User.Infrastructure.Users
{
    internal sealed class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("Roles");
            builder.HasKey(x => x.Name);
            builder.HasMany<User.Domain.Users.User>()
                .WithMany(x => x.Roles)
                .UsingEntity(entity =>
                {
                    entity.ToTable("User_Role");
                });
            builder.HasData(Role.Administrator
                , Role.Writer
                , Role.Reader
                , Role.Coop);
        }
    }
}
