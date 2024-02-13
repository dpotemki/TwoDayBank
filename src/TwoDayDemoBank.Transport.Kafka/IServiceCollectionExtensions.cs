using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TwoDayDemoBank.Common.EventBus;
using TwoDayDemoBank.Common.Models;

namespace TwoDayDemoBank.Transport.Kafka
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddKafkaTransport(this IServiceCollection services, KafkaProducerConfig configuration)            
        {
            return services.AddSingleton(configuration)
                .AddSingleton<IEventProducer, EventProducer>();
        }
    }
}