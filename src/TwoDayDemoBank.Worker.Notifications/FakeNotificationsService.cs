﻿using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace TwoDayDemoBank.Worker.Notifications
{
    public class FakeNotificationsService : INotificationsService
    {
        private readonly ILogger<FakeNotificationsService> _logger;

        public FakeNotificationsService(ILogger<FakeNotificationsService> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public Task DispatchAsync(Notification notification)
        {
            if (notification == null) 
                throw new ArgumentNullException(nameof(notification));
            _logger.LogInformation("sending notification to {Recipient}", notification.Recipient);
            return Task.CompletedTask;
        }
    }
}