using MongoDB.Driver;
using TwoDayDemoBank.Service.Core.Common.Queries;

namespace TwoDayDemoBank.Service.Core.Persistence.Mongo
{
    public interface IQueryDbContext
    {
        IMongoCollection<AccountDetails> AccountsDetails { get; }
        IMongoCollection<CustomerDetails> CustomersDetails { get; }
        IMongoCollection<CustomerArchiveItem> Customers { get; }
    }
}