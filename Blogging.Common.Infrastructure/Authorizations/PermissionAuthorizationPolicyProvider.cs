using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace Blogging.Common.Infrastructure.Authorizations
{
    internal class PermissionAuthorizationPolicyProvider : DefaultAuthorizationPolicyProvider
    {
        AuthorizationOptions authorizationOptions;
        public PermissionAuthorizationPolicyProvider(IOptions<AuthorizationOptions> options) : base(options)
        {
           authorizationOptions = options.Value;
        }
        public override async Task<AuthorizationPolicy?> GetPolicyAsync(string policyName)
        {
            var authPolicy = await base.GetPolicyAsync(policyName);

            if(policyName is not null)
                return authPolicy;

            AuthorizationPolicy policy = new AuthorizationPolicyBuilder()
                .AddRequirements(new PermissionRequirements(policyName))
                .Build();

            authorizationOptions.AddPolicy(policyName, policy);

            return policy;
        }
    }
}
