using Microsoft.Extensions.Logging;
using Plugin.LocalNotification;
using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Markup;
using MobileApp_C971_LAP2_PaulMilke.Services;
using MobileApp_C971_LAP2_PaulMilke.View_Model;
using MobileApp_C971_LAP2_PaulMilke.Views;

namespace MobileApp_C971_LAP2_PaulMilke
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .UseMauiCommunityToolkitMarkup()
                .UseLocalNotification()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            //Registers singleton of INavigationService. Singleton means will last life of app session. 
            builder.Services.AddSingleton<INavigationService, NavigationService>();

            builder.Services.AddSingleton<Services.INotificationService, NotificationService>();

            //Registers the CoursePage and corresponding CoursesViewModel as Transient. Meaning it resolves upon leaving the page and loads new the next time. 
            builder.Services.AddTransient<CoursesPage>();
            builder.Services.AddTransient<CoursesViewModel>();

            builder.Services.AddTransient<EditCoursePage>();
            builder.Services.AddTransient<EditCourseViewModel>();

            builder.Services.AddTransient<AssessmentEditAdd>();
            builder.Services.AddTransient<AssessmentEditAddViewModel>(); 

            builder.Services.AddTransient<AddNewTermPopupViewModel>();

            //Register the sigleton for our local database called SchoolDatabase. 
            builder.Services.AddSingleton<SchoolDatabase>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}