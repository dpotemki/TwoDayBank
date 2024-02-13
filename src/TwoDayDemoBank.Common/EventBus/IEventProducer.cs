using System.Threading;
using System.Threading.Tasks;

namespace TwoDayDemoBank.Common.EventBus
{
    public interface IEventProducer
    {
        Task DispatchAsync(IIntegrationEvent @event, CancellationToken cancellationToken = default);
    }
}