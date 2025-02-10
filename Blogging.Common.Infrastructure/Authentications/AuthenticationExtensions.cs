using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogging.Common.Infrastructure.Authentications
{
    internal static class AuthenticationExtensions
    {
        internal static IServiceCollection AddAuthenticationInternal(this IServiceCollection services)
        {
            services.AddAuthorization();

            services.AddAuthentication().AddJwtBearer();

            services.AddHttpContextAccessor();

            return services;
        }
    }
}
