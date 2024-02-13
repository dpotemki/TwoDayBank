using EventStore.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TwoDayBank.Persistence.EventStore.Tests.Integration;
using Xunit;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TwoDayDemoBank.Persistence.EventStore.Tests.Integration
{
    public class CleanEventStoreTest : IClassFixture<EventStoreFixture>
    {
        private readonly EventStoreFixture _fixture;

        public CleanEventStoreTest(EventStoreFixture fixture)
        {
            _fixture = fixture;
        }
        [Fact]
        public async Task CheckInfrastructure()
        {
            var cl = GetEventStoreConnection(_fixture.ConnectionString);

            var weatherForecastRecordedEvent = new WeatherForecastRecorded
            {
                Date = DateTime.Now,
                Summary = "Hot "+ DateTime.Now.Second,
                TemperatureC = 30
            };
            var utf8Bytes = JsonSerializer.SerializeToUtf8Bytes(weatherForecastRecordedEvent);

            var eventData = new EventData(Uuid.NewUuid(),
                                           nameof(WeatherForecastRecorded),
                                           utf8Bytes.AsMemory());


            try { 
            var writeResult = await cl
                            .AppendToStreamAsync(WeatherForecastRecorded.StreamName,
                                                  StreamState.Any,
                                                  new[] { eventData });

            }
            catch (Exception ex)
            {
                var a = ex.Message;
            }
            //////Get result
            var streamResult = cl.ReadStreamAsync(Direction.Forwards,
                                                WeatherForecastRecorded.StreamName,
                                                StreamPosition.Start);
            await foreach (var item in streamResult)
            {
                var a = item.Event.EventType; // <-- use this to determine which class to serialise to
                var b =    JsonSerializer.Deserialize(item.Event.Data.Span,
                                typeof(WeatherForecastRecorded));

                var asd = "";
            }


        }

        public class WeatherForecastRecorded
        {
            public static string StreamName = "WeatherForecast";

            public DateTime Date { get; set; }
            public int TemperatureC { get; set; }
            public string Summary { get; set; }
        }

        private static EventStoreClient GetEventStoreConnection(string connectionString)
        {
            var settings = EventStoreClientSettings.Create(connectionString);
            return new EventStoreClient(settings);
        }
    }
}
