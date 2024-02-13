//using System;
//using System.Threading.Tasks;
////using EventStore.ClientAPI;
//using Microsoft.Extensions.Logging;

//namespace TwoDayDemoBank.Persistence.EventStore
//{
//    public class EventStoreConnectionWrapper : IEventStoreClientWrapper, IDisposable
//    {
//        private readonly Lazy<Task<IEventStoreConnection>> _lazyConnection;
//        private readonly Uri _connString;
//        private readonly ILogger<EventStoreConnectionWrapper> _logger;

//        public EventStoreConnectionWrapper(Uri connString, ILogger<EventStoreConnectionWrapper> logger)
//        {
//            _connString = connString;
//            _logger = logger;

//            _lazyConnection = new Lazy<Task<IEventStoreConnection>>(() =>
//            {
//                return Task.Run(async () =>
//                {
//                    var connection = SetupConnection();

//                    await connection.ConnectAsync();

//                    return connection;
//                });
//            });
//        }

//        private async Task<IEventStoreConnection> GetEventStoreConnection(string connectionString)
//        {
//            var tcpConnection = EventStoreConnection.Create(
//                connectionString,
//                ConnectionSettings.Create()
//                    .FailOnNoServerResponse()
//                    .KeepReconnecting()
//                    .SetOperationTimeoutTo(TimeSpan.FromSeconds(5))
//                    .LimitAttemptsForOperationTo(7)
//                    .LimitRetriesForOperationTo(7)
//            );
//            await tcpConnection.ConnectAsync();
//            return tcpConnection;
//        }

//        public Task<IEventStoreConnection> GetConnectionAsync()
//        {
//            return _lazyConnection.Value;
//        }

//        public void Dispose()
//        {
//            if (!_lazyConnection.IsValueCreated)
//                return;

//            _lazyConnection.Value.Result.Dispose();
//        }
//    }
//}