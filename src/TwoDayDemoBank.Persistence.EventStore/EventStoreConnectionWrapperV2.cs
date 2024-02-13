using EventStore.Client;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwoDayDemoBank.Persistence.EventStore
{
    public class EventStoreConnectionWrapperV2 : IEventStoreClientWrapper, IDisposable
    {

        private readonly EventStoreClient _client;
        private readonly ILogger<IEventStoreClientWrapper> _logger;

        public EventStoreConnectionWrapperV2(EventStoreClient client, ILogger<IEventStoreClientWrapper> logger)
        {
            _client = client;
        }

        public EventStoreClient GetClient()
        {
            return _client;
        }

        public void Dispose()
        {
    
        }

    
    }
}
