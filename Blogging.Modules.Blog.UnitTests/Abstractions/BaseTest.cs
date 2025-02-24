using Blogging.Common.Domain;
using Bogus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogging.Modules.Blog.UnitTests.Abstractions
{
    public abstract class BaseTest
    {
        protected static readonly Faker Faker = new();

        public static T AssertDomainEventWasPublished<T>(Entity entity)
            where T : IDomainEvent
        {
            T? domainEvent = entity.DomainEvents.OfType<T>().SingleOrDefault();

            if(domainEvent is null) 
                throw new Exception($"{typeof(T).Name} was not published");
            
            return domainEvent;
        }
    }
}
