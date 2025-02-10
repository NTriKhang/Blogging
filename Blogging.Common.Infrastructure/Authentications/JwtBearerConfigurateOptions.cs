using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogging.Common.Infrastructure.Authentications
{
    internal sealed class JwtBearerConfigurateOptions(IConfiguration conf) 
        : IConfigureNamedOptions<JwtBearerOptions>
    {
        private readonly string ConfigurationSectionName = "Authentication";
        public void Configure(JwtBearerOptions options)
        {
            conf.GetSection(ConfigurationSectionName).Bind(options);
        }

        public void Configure(string? name, JwtBearerOptions options)
        {
            Configure(options);
        }
    }
}
