using EventStore.Client;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TwoDayDemoBank.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwoDayDemoBank.Persistence.EventStore
{
    public static class IServiceCollectionExtensionsV2
    {
        public static IServiceCollection AddEventStorePersistenceV2(this IServiceCollection services, string connectionString)
        {
            return services
                .AddSingleton<EventStoreClient>(ctx=> GetEventStoreConnection(connectionString))
                .AddSingleton<IEventStoreClientWrapper>(ctx =>
            {
                var logger = ctx.GetRequiredService<ILogger<IEventStoreClientWrapper>>();
                return new EventStoreConnectionWrapperV2(GetEventStoreConnection(connectionString), logger);
            })
                
                .AddSingleton(typeof(IAggregateRepository<,>), typeof(EventStoreAggregateRepositoryV2<,>));
        }

        private static EventStoreClient GetEventStoreConnection(string connectionString)
        {
            var settings = EventStoreClientSettings.Create(connectionString);
            return new EventStoreClient(settings);
        }
    }
}

