using System.Threading.Tasks;
using EventStore.Client;

namespace TwoDayDemoBank.Persistence.EventStore
{

    public interface IEventStoreClientWrapper
    {
        EventStoreClient GetClient();
    }
}