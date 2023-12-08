using Plugin.LocalNotification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileApp_C971_LAP2_PaulMilke.Services
{
    class NotificationService : INotificationService
    {
        public async Task<bool> AreNotificationsEnabledAsync()
        {
            return await LocalNotificationCenter.Current.AreNotificationsEnabled();
        }

        public async Task RequestNotificationPermissionAsync()
        {
            await LocalNotificationCenter.Current.RequestNotificationPermission(); 
        }

        public async Task ScheduleNotificationAsync(Plugin.LocalNotification.NotificationRequest notifcaionRequest)
        {
            await LocalNotificationCenter.Current.Show(notifcaionRequest); 
        }
    }
}
