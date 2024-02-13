//using TwoDayDemoBank.Persistence.Tests.Models;
//using System;
//using System.Threading.Tasks;
//using Xunit;
//using FluentAssertions;
//using Microsoft.Extensions.Logging;
//using TwoDayDemoBank.Common;
//using System.ComponentModel;
//using EventStore.Client;
//using TwoDayDemoBank.Persistence.EventStore;

//namespace TwoDayBank.Persistence.EventStore.Tests.Integration
//{
//    [Trait("Category", "Integration")]
//    [Category("Integration")]
//    public class EventsRepositoryTests : IClassFixture<EventStoreFixture>
//    {
//        private readonly EventStoreFixture _fixture;

//        public EventsRepositoryTests(EventStoreFixture fixture)
//        {
//            _fixture = fixture;
//        }

//        [Fact]
//        public async Task PersistAsync_should_store_events()
//        {
//            var connStr = new Uri(_fixture.ConnectionString);
//            var logger = NSubstitute.Substitute.For<ILogger<EventStoreConnectionWrapperV2>>();
//            var cl = new EventStoreClient(EventStoreClientSettings.Create(_fixture.ConnectionString));
//            using var conn = new EventStoreConnectionWrapperV2(cl, logger);

//            var serializer = NSubstitute.Substitute.For<IEventSerializer>();

//            var sut = new EventStoreAggregateRepositoryV2<DummyAggregate, Guid>(conn, serializer);

//            var aggregate = new DummyAggregate(Guid.NewGuid());
//            aggregate.DoSomething("foo");
//            aggregate.DoSomething("bar");

//            await sut.PersistAsync(aggregate);

//            var rehydrated = await sut.RehydrateAsync(aggregate.Id);
//            rehydrated.Should().NotBeNull();
//            rehydrated.Version.Should().Be(3);
//        }

//        [Fact]
//        public async Task PersistAsync_should_clear_Aggregate_events()
//        {
//            var connStr = new Uri(_fixture.ConnectionString);
//            var logger = NSubstitute.Substitute.For<ILogger<EventStoreConnectionWrapperV2>>();
//            using var conn = new EventStoreConnectionWrapperV2(connStr, logger);

//            var serializer = NSubstitute.Substitute.For<IEventSerializer>();

//            var sut = new EventStoreAggregateRepositoryV2<DummyAggregate, Guid>(conn, serializer);

//            var aggregate = new DummyAggregate(Guid.NewGuid());
//            aggregate.DoSomething("foo");
//            aggregate.DoSomething("bar");

//            aggregate.Events.Should().NotBeEmpty();

//            await sut.PersistAsync(aggregate);

//            aggregate.Events.Should().BeEmpty();
//        }


//        [Fact]
//        public async Task RehydrateAsync_should_return_null_when_id_invalid()
//        {
//            var connStr = new Uri(_fixture.ConnectionString);

   



//        var logger = NSubstitute.Substitute.For<ILogger<EventStoreConnectionWrapperV2>>();
//            using var conn = new EventStoreConnectionWrapperV2(connStr, logger);

//            var serializer = NSubstitute.Substitute.For<IEventSerializer>();

//            var sut = new EventStoreAggregateRepositoryV2<DummyAggregate, Guid>(conn, serializer);

//            var rehydrated = await sut.RehydrateAsync(Guid.NewGuid());
//            rehydrated.Should().BeNull();
//        }

     
//    }
//}
