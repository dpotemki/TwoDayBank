using System;
using System.Threading.Tasks;

namespace TwoDayDemoBank.Worker.Notifications
{
    public interface INotificationsFactory
    {
        Task<Notification> CreateNewAccountNotificationAsync(Guid accountId);
        Task<Notification> CreateTransactionNotificationAsync(Guid accountId);
    }
}