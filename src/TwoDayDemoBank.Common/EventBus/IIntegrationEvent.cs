using System;

namespace TwoDayDemoBank.Common.EventBus
{
    public interface IIntegrationEvent
    {
        Guid Id { get; }
    }
}