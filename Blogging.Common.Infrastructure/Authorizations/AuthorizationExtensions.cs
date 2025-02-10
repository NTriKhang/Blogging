using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogging.Common.Infrastructure.Authorizations
{
    internal static class AuthorizationExtensions
    {
        internal static IServiceCollection AddAuthorizationInternal(this IServiceCollection services)
        {
            services.AddTransient<IClaimsTransformation, CustomClaimsTransformation>();
            services.AddTransient<IAuthorizationHandler, PermissionAuthorizationHandler>();
            services.AddTransient<IAuthorizationPolicyProvider, PermissionAuthorizationPolicyProvider>();

            return services;
        }
    }
}
