using MediatR;
using TwoDayDemoBank.Common.EventBus;
using System;

namespace TwoDayDemoBank.Domain.IntegrationEvents
{
    public record CustomerCreated : IIntegrationEvent, INotification
    {
        public CustomerCreated(Guid id, Guid customerId)
        {
            this.Id = id;
            this.CustomerId = customerId;
        }

        public Guid Id { get; }
        public Guid CustomerId { get; }
    }
}