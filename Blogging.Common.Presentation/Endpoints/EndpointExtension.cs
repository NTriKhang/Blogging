using Microsoft.AspNetCore.HttpLogging;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Blogging.Common.Presentation.Endpoints
{
    public static class EndpointExtension
    {
        public static IEndpointRouteBuilder MapEndpoint(this IEndpointRouteBuilder app
            , Assembly[] assemblies)
        {
            IEndpoint[] endpoints = assemblies
                .SelectMany(x => x.GetTypes())
                .Where(type => type.IsAssignableTo(typeof(IEndpoint))
                            && type is { IsAbstract: false, IsInterface: false })
                .Select(type => (IEndpoint)Activator.CreateInstance(type)!)
                .ToArray();

            foreach(var endpoint in endpoints)
            {
                endpoint.MapEndpoint(app);
            }
            return app;
        }
    }
}
