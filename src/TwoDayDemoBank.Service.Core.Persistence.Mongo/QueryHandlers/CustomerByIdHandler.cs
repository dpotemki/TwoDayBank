using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MongoDB.Driver;
using TwoDayDemoBank.Service.Core.Common.Queries;

namespace TwoDayDemoBank.Service.Core.Persistence.Mongo.QueryHandlers
{
    public class CustomerByIdHandler : IRequestHandler<CustomerById, CustomerDetails>
    {
        private readonly IQueryDbContext _db;
       
        public CustomerByIdHandler(IQueryDbContext db)
        {
            _db = db;
        }

        public async Task<CustomerDetails> Handle(CustomerById request, CancellationToken cancellationToken)
        {
            var cursor = await _db.CustomersDetails.FindAsync(c => c.Id == request.CustomerId,
                null, cancellationToken);
            return await cursor.FirstOrDefaultAsync(cancellationToken);
        }
    }
}