using System;

namespace TwoDayDemoBank.Worker.Notifications.ApiClients.Models
{
    public record CustomerDetails
    {
        public Guid Id { get; init; }
        public string Firstname { get; init; }
        public string Lastname { get; init; }
        public string Email { get; init; }
    }
}