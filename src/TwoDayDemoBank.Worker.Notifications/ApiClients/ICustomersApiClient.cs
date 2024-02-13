using System;
using System.Threading;
using System.Threading.Tasks;
using TwoDayDemoBank.Worker.Notifications.ApiClients.Models;

namespace TwoDayDemoBank.Worker.Notifications.ApiClients
{
    public interface ICustomersApiClient {
        Task<CustomerDetails> GetCustomerAsync(Guid customerId, CancellationToken cancellationToken = default);
    }
}