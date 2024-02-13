using MediatR;
using TwoDayDemoBank.Common.EventBus;
using System;

namespace TwoDayDemoBank.Domain.IntegrationEvents
{
    public record AccountCreated : IIntegrationEvent, INotification
    {
        public AccountCreated(Guid id, Guid accountId)
        {
            this.Id = id;
            this.AccountId = accountId;
        }

        public Guid AccountId { get; init; }
        public Guid Id { get; }
    }
}