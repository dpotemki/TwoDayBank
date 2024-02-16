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
            var newEvents = aggregateRoot.Events.Select(Map).ToArray();

            var streamName = GetStreamName(aggregateRoot.Id);

            await _client.AppendToStreamAsync(
                streamName,
                StreamState.Any,
                newEvents,
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

                    var appedEvent = Map(@event);
                    events.Add(appedEvent);
                }
            }
            catch (StreamNotFoundException ex)
            {
                _logger.LogInformation(ex, "StreamNotFoundException {StreamName}", streamName);
                return null;
            }

            var aggregate = BaseAggregateRoot<TA, TKey>.Create(events.OrderBy(e => e.AggregateVersion));
            return aggregate;
        }



        private static string GetStreamName(TKey key) => $"{typeof(TA).Name}-{key}";


        private IDomainEvent<TKey> Map(ResolvedEvent resolvedEvent)
        {
            var meta = System.Text.Json.JsonSerializer.Deserialize<EventMeta>(resolvedEvent.Event.Metadata.Span);
            return _eventSerializer.Deserialize<TKey>(meta.EventType, resolvedEvent.Event.Data.ToArray());
        }

        private static EventData Map(IDomainEvent<TKey> @event)
        {
            var json = System.Text.Json.JsonSerializer.Serialize((dynamic)@event);
            var data = Encoding.UTF8.GetBytes(json);

            var eventType = @event.GetType();
            var meta = new EventMeta()
            {
                EventType = eventType.AssemblyQualifiedName
            };
            var metaJson = System.Text.Json.JsonSerializer.Serialize(meta);
            var metadata = Encoding.UTF8.GetBytes(metaJson);

            Guid id = Guid.NewGuid();
            var eventPayload = new EventData(Uuid.NewUuid(), eventType.Name, data, metadata);
            return eventPayload;
        }
        internal struct EventMeta
        {
            public string EventType { get; set; }
        }
    }
}
