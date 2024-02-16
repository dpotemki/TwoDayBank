using Microsoft.Extensions.Logging;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using TwoDayDemoBank.Persistence.EventStore;
using EventStore.Client;

namespace TwoDayBank.Persistence.EventStore.Tests.Integration
{

    [Trait("Category", "Integration")]
    [Category("Integration")]
    public class EventStoreConnectionWrapperTests : IClassFixture<EventStoreFixture>
    {
        private readonly EventStoreFixture _fixture;

        public EventStoreConnectionWrapperTests(EventStoreFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task GetConnectionAsync_should_return_connection()
        {
            var logger = NSubstitute.Substitute.For<ILogger<EventStoreConnectionWrapperV2>>();

            var settings = EventStoreClientSettings.Create(_fixture.ConnectionString);
            var cl = new EventStoreClient(settings);
            using var sut = new EventStoreConnectionWrapperV2(
                    cl, logger);


            var conn = sut.GetClient();
            conn.Should().NotBeNull();

        }
    }
}
