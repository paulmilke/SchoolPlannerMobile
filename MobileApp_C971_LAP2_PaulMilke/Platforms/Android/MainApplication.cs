﻿using Android;
using Android.App;
using Android.Runtime;

[assembly: UsesPermission(Manifest.Permission.WakeLock)]

//Required so that the plugin can reschedule notifications upon a reboot
[assembly: UsesPermission(Manifest.Permission.ReceiveBootCompleted)]
[assembly: UsesPermission(Manifest.Permission.Vibrate)]
[assembly: UsesPermission("android.permission.SCHEDULE_EXACT_ALARM")]
[assembly: UsesPermission("android.permission.POST_NOTIFICATIONS")]

namespace MobileApp_C971_LAP2_PaulMilke
{
#if DEBUG
    [Application(UsesCleartextTraffic = true)]
#else
    [Application]
#endif
    public class MainApplication : MauiApplication
    {
        public MainApplication(IntPtr handle, JniHandleOwnership ownership)
            : base(handle, ownership)
        {
        }

        protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
    }
}