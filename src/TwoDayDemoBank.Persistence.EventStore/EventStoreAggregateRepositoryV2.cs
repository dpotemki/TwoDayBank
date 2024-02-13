using TwoDayDemoBank.Common.Models;
using TwoDayDemoBank.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using EventStore.Client;
using System.Text.Json;
using Microsoft.Extensions.Logging;
//using Newtonsoft.Json;

namespace TwoDayDemoBank.Persistence.EventStore
{
    public class EventStoreAggregateRepositoryV2<TA, TKey> : IAggregateRepository<TA, TKey>
    where TA : class, IAggregateRoot<TKey>// Assuming a parameterless constructor for TA

    {
        private readonly EventStoreClient _client;
        private readonly ILogger<EventStoreAggregateRepositoryV2<TA, TKey>> _logger;
        private readonly IEventSerializer _eventSerializer;

        public EventStoreAggregateRepositoryV2(
            EventStoreClient client, 
            ILogger<EventStoreAggregateRepositoryV2<TA, TKey>> logger,
            IEventSerializer eventSerializer)
        {
            _client = client;
            _logger = logger;
            _eventSerializer = eventSerializer;
        }
        public async Task PersistAsync(TA aggregateRoot, CancellationToken cancellationToken = default)
        {
            var events = aggregateRoot.Events.Select(e => new EventData(
                 Uuid.NewUuid(),
                 e.GetType().FullName,
                 JsonSerializer.SerializeToUtf8Bytes(e)
             )).ToArray();

            var streamName = GetStreamName(aggregateRoot.Id);

            await _client.AppendToStreamAsync(
                streamName,
                StreamState.Any,
                events,
                cancellationToken: cancellationToken);

            aggregateRoot.ClearEvents();
        }

        public async Task<TA> RehydrateAsync(TKey key, CancellationToken cancellationToken = default)
        {
            var streamName = GetStreamName(key);
            List<IDomainEvent<TKey>> events = new List<IDomainEvent<TKey>>();

            try
            {
                var result = _client.ReadStreamAsync(
                    Direction.Forwards,
                    streamName,
                    StreamPosition.Start,
                    cancellationToken: cancellationToken);

                await foreach (var @event in result)
                {
                    var eventType = Type.GetType(@event.Event.EventType);
                    var eventData = _eventSerializer.Deserialize<TKey>(@event.Event.EventType, @event.Event.Data.ToArray());
                    if (eventData is IDomainEvent<TKey> domainEvent)
                    {
                        events.Add(domainEvent);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error reading stream {StreamName}", streamName);
                return null;
            }

            var aggregate = BaseAggregateRoot<TA, TKey>.Create(events.OrderBy(e => e.AggregateVersion));
            return aggregate;
        }


     
        private static string GetStreamName(TKey key) => $"{typeof(TA).Name}-{key}";

    }
}
