using Blogging.Common.Application.EventBus;
using Blogging.Common.Domain;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Blogging.Common.Infrastructure.Inbox
{
    public static class IntegrationEventHandlerFactory
    {
        private static readonly ConcurrentDictionary<string, Type[]> HandlersDictionary = new();

        public static IEnumerable<IIntegrationEventHandler> GetHandlers(
            Assembly assembly
            , Type type
            , IServiceProvider serviceProvider)
        {
            Type[] handlerTypes = HandlersDictionary.GetOrAdd(
             $"{assembly.FullName}_{type.Name}",
             _ =>
             {
                 return assembly
                 .GetTypes()
                  .Where(t => t.IsAssignableTo(typeof(IIntegrationEventHandler<>).MakeGenericType(type)))
                  .ToArray();
             });

            List<IIntegrationEventHandler> handlers = [];
            foreach (Type handlerType in handlerTypes)
            {
                var integrationEventHandler = serviceProvider.GetRequiredService(handlerType);
                handlers.Add((integrationEventHandler as IIntegrationEventHandler)!);
            }
            return handlers;    
        }
    }
}
