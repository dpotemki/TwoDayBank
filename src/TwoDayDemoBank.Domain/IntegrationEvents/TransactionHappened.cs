using MediatR;
using TwoDayDemoBank.Common.EventBus;
using System;

namespace TwoDayDemoBank.Domain.IntegrationEvents
{
    public record TransactionHappened : IIntegrationEvent, INotification
    {
        public TransactionHappened(Guid id, Guid accountId)
        {
            this.Id = id;
            this.AccountId = accountId;
        }

        public Guid AccountId { get; init; }
        public Guid Id { get; }
    }
}