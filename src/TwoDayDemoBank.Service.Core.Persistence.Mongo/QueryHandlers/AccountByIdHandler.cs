using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MongoDB.Driver;
using TwoDayDemoBank.Service.Core.Common.Queries;

namespace TwoDayDemoBank.Service.Core.Persistence.Mongo.QueryHandlers
{
    public class AccountByIdHandler : IRequestHandler<AccountById, AccountDetails>
    {
        private readonly IQueryDbContext _db;

        public AccountByIdHandler(IQueryDbContext db)
        {
            _db = db;
        }

        public async Task<AccountDetails> Handle(AccountById request, CancellationToken cancellationToken)
        {
            var cursor = await _db.AccountsDetails.FindAsync(c => c.Id == request.AccountId,
                null, cancellationToken);
            return await cursor.FirstOrDefaultAsync(cancellationToken);
        }
    }
}