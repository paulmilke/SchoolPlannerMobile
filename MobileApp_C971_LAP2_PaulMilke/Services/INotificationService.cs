using Plugin.LocalNotification;


namespace MobileApp_C971_LAP2_PaulMilke.Services
{
    public interface INotificationService
    {
        Task<bool> AreNotificationsEnabledAsync();
        Task RequestNotificationPermissionAsync(); 
        Task ScheduleNotificationAsync(NotificationRequest notifcaionRequest);
    }
}
