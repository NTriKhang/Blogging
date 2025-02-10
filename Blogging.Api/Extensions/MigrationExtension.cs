using Microsoft.EntityFrameworkCore;
using Blogging.Modules.User.Infrastructure;
using Blogging.Modules.User.Infrastructure.Database;
using FluentValidation;

namespace Blogging.Api.Extensions
{
    internal static class MigrationExtension
    {
        internal static void ApplyMigrations(this IApplicationBuilder builder)
        {
            using IServiceScope scope = builder.ApplicationServices.CreateScope();
            ApplyMigration<UserDbContext>(scope);
        }
        private static void ApplyMigration<TDbContext>(IServiceScope scope) where TDbContext : DbContext
        {
            using TDbContext context = scope.ServiceProvider.GetRequiredService<TDbContext>();
            context.Database.Migrate();
        }
    }
}
