using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogging.Modules.User.Infrastructure.Users
{
    internal sealed class UserConfiguration : IEntityTypeConfiguration<Domain.Users.User>
    {
        public void Configure(EntityTypeBuilder<Domain.Users.User> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.UserName).IsUnique();
            builder.HasIndex(x => x.Email).IsUnique();
            builder.HasIndex(x => x.IdentityId).IsUnique();
        }
    }
}
