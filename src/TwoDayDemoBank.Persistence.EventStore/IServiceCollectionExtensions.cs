//using Microsoft.Extensions.DependencyInjection;
//using Microsoft.Extensions.Logging;
//using TwoDayDemoBank.Common;
//using System;

//namespace TwoDayDemoBank.Persistence.EventStore
//{
//    public static class IServiceCollectionExtensions
//    {
//        public static IServiceCollection AddEventStorePersistence(this IServiceCollection services, string connectionString)
//        {
//            return services.AddSingleton<IEventStoreClientWrapper>(ctx =>
//                {
//                    var logger = ctx.GetRequiredService<ILogger<EventStoreConnectionWrapper>>();
//                    return new EventStoreConnectionWrapper(new Uri(connectionString), logger);
//                }).AddSingleton(typeof(IAggregateRepository<,>), typeof(EventStoreAggregateRepository<,>));
//        }
//    }
//}