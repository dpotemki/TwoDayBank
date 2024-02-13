using System.Threading.Tasks;

namespace TwoDayDemoBank.Worker.Notifications
{
    public interface INotificationsService
    {
        Task DispatchAsync(Notification notification);
    }
}